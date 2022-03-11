using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;
using Lextatico.Sly.Lexer.Fsm;
using Lextatico.Sly.Parser.Builder;
using Lextatico.Sly.Parser.Syntax.Grammar;

namespace Lextatico.Sly.Parser.LlParser
{
    public class RecursiveDescentSyntaxParser<T> : ISyntaxParser<T> where T : Token
    {
        public RecursiveDescentSyntaxParser(ParserConfiguration<T> configuration, string startingNonTerminal)
        {
            _configuration = configuration;
            StartingNonTerminal = startingNonTerminal;
            InitializeStartingTokens(_configuration, startingNonTerminal);
        }

        private readonly ParserConfiguration<T> _configuration;

        public string StartingNonTerminal { get; set; }

        public virtual void Init(ParserConfiguration<T> configuration, string root)
        {
            if (root != null) StartingNonTerminal = root;
            InitializeStartingTokens(configuration, StartingNonTerminal);
        }

        public SyntaxParseResult<T> Parse(IList<LexerToken<T>> tokens, string startingNonTerminal = null)
        {
            var start = startingNonTerminal ?? StartingNonTerminal;

            var NonTerminals = _configuration.NonTerminals;

            var errors = new List<UnexpectedTokenSyntaxError<T>>();

            var nt = NonTerminals[start];

            var rules = nt.Rules.Where(r => !tokens[0].IsEOS && r.PossibleLeadingTokens.Contains(tokens[0].Result)).ToList();

            if (!rules.Any())
            {
                errors.Add(new UnexpectedTokenSyntaxError<T>(tokens[0], nt.PossibleLeadingTokens.ToArray()));
            }

            var rs = new List<SyntaxParseResult<T>>();

            foreach (var rule in rules)
            {
                var r = Parse(tokens, rule, 0, start);
                rs.Add(r);
            }

            SyntaxParseResult<T> result = null;

            if (rs.Count > 0)
            {
                result = rs.Find(r => r.IsEnded && !r.IsError);

                if (result == null)
                {
                    var endingPositions = rs.Select(r => r.EndingPosition).ToList();

                    var lastposition = endingPositions.Max();

                    var furtherResults = rs.Where(r => r.EndingPosition == lastposition).ToList();

                    furtherResults.ForEach(r =>
                    {
                        if (r.Errors != null) errors.AddRange(r.Errors);
                    });

                    if (!errors.Any())
                    {
                        errors.Add(new UnexpectedTokenSyntaxError<T>(tokens[lastposition], tokens.LastOrDefault().Result));
                    }
                }
            }

            if (result == null)
            {
                result = new SyntaxParseResult<T>();
                // errors.Sort();

                if (errors.Count > 0)
                {
                    var lastErrorPosition = errors.Select(e => e.UnexpectedToken.PositionInTokenFlow).ToList().Max();
                    var lastErrors = errors.Where(e => e.UnexpectedToken.PositionInTokenFlow == lastErrorPosition)
                        .ToList();
                    result.Errors = lastErrors;
                }
                else
                {
                    result.Errors = errors;
                }

                result.IsError = true;
            }

            return result;
        }


        public virtual SyntaxParseResult<T> Parse(IList<LexerToken<T>> tokens, Rule<T> rule, int position,
            string nonTerminalName)
        {
            var currentPosition = position;
            var errors = new List<UnexpectedTokenSyntaxError<T>>();
            var isError = false;
            if (!tokens[position].IsEOS && rule.PossibleLeadingTokens.Contains(tokens[position].Result))
                if (rule.Clauses != null && rule.Clauses.Count > 0)
                {
                    foreach (var clause in rule.Clauses)
                    {
                        if (clause is TerminalClause<T>)
                        {
                            var termRes = ParseTerminal(tokens, clause as TerminalClause<T>, currentPosition);
                            if (!termRes.IsError)
                            {
                                currentPosition = termRes.EndingPosition;
                            }
                            else
                            {
                                var tok = tokens[currentPosition];
                                errors.Add(new UnexpectedTokenSyntaxError<T>(tok,
                                    ((TerminalClause<T>)clause).ExpectedToken));
                            }

                            isError = isError || termRes.IsError;
                        }
                        else if (clause is NonTerminalClause<T>)
                        {
                            var nonTerminalResult =
                                ParseNonTerminal(tokens, clause as NonTerminalClause<T>, currentPosition);
                            if (!nonTerminalResult.IsError)
                            {
                                currentPosition = nonTerminalResult.EndingPosition;
                                if (nonTerminalResult.Errors != null && nonTerminalResult.Errors.Any())
                                    errors.AddRange(nonTerminalResult.Errors);
                            }
                            else
                            {
                                errors.AddRange(nonTerminalResult.Errors);
                            }

                            isError = isError || nonTerminalResult.IsError;
                        }

                        if (isError) break;
                    }
                }

            var result = new SyntaxParseResult<T>();
            result.IsError = isError;
            result.Errors = errors;
            result.EndingPosition = currentPosition;
            if (!isError)
            {
                // TODO: REVER ESSA REGRA, NÃO PARECE ESTAR CERTA
                result.IsEnded = result.EndingPosition >= tokens.Count - 1
                    || result.EndingPosition == tokens.Count - 1 &&
                    tokens[tokens.Count - 1].IsEOS;
            }

            return result;
        }

        private SyntaxParseResult<T> ParseTerminal(IList<LexerToken<T>> tokens, TerminalClause<T> terminal, int position)
        {
            var result = new SyntaxParseResult<T>();
            result.IsError = !terminal.Check(tokens[position]);
            result.EndingPosition = !result.IsError ? position + 1 : position;
            var token = tokens[position];
            return result;
        }

        public SyntaxParseResult<T> ParseNonTerminal(IList<LexerToken<T>> tokens, NonTerminalClause<T> nonTermClause,
            int currentPosition)
        {
            var startPosition = currentPosition;
            var nt = _configuration.NonTerminals[nonTermClause.NonTerminalName];
            var errors = new List<UnexpectedTokenSyntaxError<T>>();

            var i = 0;
            var rules = nt.Rules
                .Where(r => startPosition < tokens.Count && !tokens[startPosition].IsEOS && r.PossibleLeadingTokens.Contains(tokens[startPosition].Result) || r.MayBeEmpty)
                .ToList();

            if (rules.Count == 0)
            {
                var allAcceptableTokens = new List<T>();
                nt.Rules.ForEach(r =>
                {
                    if (r != null && r.PossibleLeadingTokens != null) allAcceptableTokens.AddRange(r.PossibleLeadingTokens);
                });
                allAcceptableTokens = allAcceptableTokens.Distinct().ToList();
                return NoMatchingRuleError(tokens, currentPosition, allAcceptableTokens);
            }

            var innerRuleErrors = new List<UnexpectedTokenSyntaxError<T>>();
            var greaterIndex = 0;
            var rulesResults = new List<SyntaxParseResult<T>>();
            while (i < rules.Count)
            {
                var innerrule = rules[i];
                var innerRuleRes = Parse(tokens, innerrule, startPosition, nonTermClause.NonTerminalName);
                rulesResults.Add(innerRuleRes);

                var other = greaterIndex == 0 && innerRuleRes.EndingPosition == 0;
                if (innerRuleRes.EndingPosition > greaterIndex && innerRuleRes.Errors != null &&
                    !innerRuleRes.Errors.Any() || other)
                {
                    greaterIndex = innerRuleRes.EndingPosition;
                    //innerRuleErrors.Clear();
                    innerRuleErrors.AddRange(innerRuleRes.Errors);
                }

                innerRuleErrors.AddRange(innerRuleRes.Errors);
                i++;
            }

            errors.AddRange(innerRuleErrors);
            SyntaxParseResult<T> max = null;
            if (rulesResults.Any())
            {
                if (rulesResults.Any(x => x.IsOk))
                {
                    max = rulesResults.Where(x => x.IsOk).OrderBy(x => x.EndingPosition).Last();
                }
                else
                {
                    max = rulesResults.Where(x => !x.IsOk).OrderBy(x => x.EndingPosition).Last();
                }
            }
            else
            {
                max = new SyntaxParseResult<T>();
                max.IsError = true;
                max.IsEnded = false;
                max.EndingPosition = currentPosition;
            }

            var result = new SyntaxParseResult<T>();
            result.Errors = errors;
            result.EndingPosition = max.EndingPosition;
            result.IsError = max.IsError;
            result.IsEnded = max.IsEnded;

            if (rulesResults.Any())
            {
                var terr = rulesResults.SelectMany(x => x.Errors).ToList();
                var unexpected = terr.Cast<UnexpectedTokenSyntaxError<T>>().ToList();
                var expecting = unexpected.SelectMany(x => x.ExpectedTokens).ToList();
                result.AddExpectings(expecting);
            }

            return result;
        }

        private void InitializeStartingTokens(ParserConfiguration<T> configuration, string root)
        {
            var nts = configuration.NonTerminals;

            InitStartingTokensForNonTerminal(nts, root);

            foreach (var nt in nts.Values)
            {
                foreach (var rule in nt.Rules)
                {
                    if (rule.PossibleLeadingTokens == null || rule.PossibleLeadingTokens.Count == 0)
                        InitStartingTokensForRule(nts, rule);
                }
            }
        }

        private void InitStartingTokensForNonTerminal(Dictionary<string, NonTerminal<T>> nonTerminals,
            string name)
        {
            if (nonTerminals.ContainsKey(name))
            {
                var nt = nonTerminals[name];

                nt.Rules.ForEach(r => InitStartingTokensForRule(nonTerminals, r));
            }
        }

        private void InitStartingTokensForRule(Dictionary<string, NonTerminal<T>> nonTerminals,
            Rule<T> rule)
        {
            if (rule.PossibleLeadingTokens == null || rule.PossibleLeadingTokens.Count == 0)
            {
                rule.PossibleLeadingTokens = new List<T>();

                if (rule.Clauses.Count > 0)
                {
                    var first = rule.Clauses[0];

                    if (first is TerminalClause<T> term)
                    {
                        rule.PossibleLeadingTokens.Add(term.ExpectedToken);

                        rule.PossibleLeadingTokens = rule.PossibleLeadingTokens.Distinct().ToList();
                    }
                    else if (first is NonTerminalClause<T> nonterm)
                    {
                        InitStartingTokensForNonTerminal(nonTerminals, nonterm.NonTerminalName);

                        if (nonTerminals.ContainsKey(nonterm.NonTerminalName))
                        {
                            var firstNonTerminal = nonTerminals[nonterm.NonTerminalName];

                            firstNonTerminal.Rules.ForEach(r =>
                            {
                                rule.PossibleLeadingTokens.AddRange(r.PossibleLeadingTokens);
                            });

                            rule.PossibleLeadingTokens = rule.PossibleLeadingTokens.Distinct().ToList();
                        }
                    }
                }
            }
        }

        private SyntaxParseResult<T> NoMatchingRuleError(IList<LexerToken<T>> tokens, int currentPosition, List<T> allAcceptableTokens)
        {
            var noRuleErrors = new List<UnexpectedTokenSyntaxError<T>>();

            if (currentPosition < tokens.Count)
            {
                noRuleErrors.Add(new UnexpectedTokenSyntaxError<T>(tokens[currentPosition],
                    allAcceptableTokens.ToArray<T>()));
            }
            else
            {
                noRuleErrors.Add(new UnexpectedTokenSyntaxError<T>(new LexerToken<T>() {IsEOS = true},
                    allAcceptableTokens.ToArray<T>()));
            }

            var error = new SyntaxParseResult<T>();
            error.IsError = true;
            error.IsEnded = false;
            error.Errors = noRuleErrors;
            error.EndingPosition = currentPosition;

            return error;
        }
    }
}
