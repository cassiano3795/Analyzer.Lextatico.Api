using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;
using Lextatico.Sly.Parser.LlParser;
using Lextatico.Sly.Parser.Syntax.Grammar;
using Lextatico.Sly.Result;

namespace Lextatico.Sly.Parser.Builder
{
    public delegate BuildResult<Parser<T>> ParserChecker<T>(BuildResult<Parser<T>> result,
        NonTerminal<T> nonterminal) where T : Token;

    public class LextaticoParserBuilder<T> : ILextaticoParserBuilder<T> where T : Token
    {
        public LextaticoParserBuilder(IList<T> tokens)
        {
            _tokens = tokens;
        }

        private readonly IList<T> _tokens;

        public BuildResult<Parser<T>> BuildParser(string rootRule, ParserType parserType, IEnumerable<string> productionRules)
        {
            var result = new BuildResult<Parser<T>>();

            var lexerResult = BuildLexer();

            if (lexerResult.IsError)
            {
                foreach (var lexerResultError in lexerResult.Errors)
                {
                    result.AddError(lexerResultError);
                }
                return result;
            }

            Parser<T> parser;

            switch (parserType)
            {
                case ParserType.LlRecursiveDescent:
                    {
                        var configuration = ExtractParserConfiguration(productionRules);

                        var (foundRecursion, recursions) = LeftRecursionChecker<T>.CheckLeftRecursion(configuration);
                        if (foundRecursion)
                        {
                            var recs = string.Join("\n", recursions.Select(x => string.Join(" > ", x)));

                            result.AddError(new ParserInitializationError(ErrorLevel.Fatal,
                                string.Format("left recursion detected : {0}", recs),
                                ErrorCodes.ParserLeftRecursive));

                            return result;

                        }

                        configuration.StartingRule = rootRule;

                        var syntaxParser = BuildSyntaxParser(configuration, parserType, rootRule);

                        parser = new Parser<T>(syntaxParser);

                        parser.Lexer = lexerResult.Result;

                        parser.Configuration = configuration;

                        result.Result = parser;

                        break;
                    }
                case ParserType.EbnfLlRecursiveDescente:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(parserType));
            }

            if (!result.IsError)
            {
                var expressionResult = parser.BuildExpressionParser(result, rootRule);

                if (expressionResult.IsError) result.AddErrors(expressionResult.Errors);

                result.Result.Configuration = expressionResult.Result;

                result = CheckParser(result);

                if (result.IsError)
                {
                    result.Result = null;
                }
            }
            else
            {
                result.Result = null;
            }

            return result;
        }

        private BuildResult<ILexer<T>> BuildLexer()
        {
            var lexerBuilder = new LextaticoLexerBuilder<T>(_tokens);

            var lexerResult = lexerBuilder.Build();

            return lexerResult;
        }

        private ISyntaxParser<T> BuildSyntaxParser(ParserConfiguration<T> configuration,
            ParserType parserType, string rootRule)
        {
            ISyntaxParser<T> parser;
            switch (parserType)
            {
                case ParserType.LlRecursiveDescent:
                    {
                        parser = new RecursiveDescentSyntaxParser<T>(configuration, rootRule);
                        break;
                    }
                default:
                    {
                        parser = null;
                        break;
                    }
            }

            return parser;
        }

        public ParserConfiguration<T> ExtractParserConfiguration(IEnumerable<string> productionRules)
        {
            var conf = new ParserConfiguration<T>();

            var nonTerminals = new Dictionary<string, NonTerminal<T>>();

            foreach (var productionRule in productionRules)
            {
                var ntAndRule = ExtractNTAndRule(productionRule);

                var rule = BuildNonTerminal(ntAndRule);

                rule.NonTerminalName = ntAndRule.Item1;

                NonTerminal<T> nonT;

                if (!nonTerminals.ContainsKey(ntAndRule.Item1))
                    nonT = new NonTerminal<T>(ntAndRule.Item1);
                else
                    nonT = nonTerminals[ntAndRule.Item1];

                nonT.Rules.Add(rule);

                nonTerminals[ntAndRule.Item1] = nonT;
            }

            conf.NonTerminals = nonTerminals;

            return conf;
        }

        private Tuple<string, string> ExtractNTAndRule(string ruleString)
        {
            Tuple<string, string> result = null;

            if (ruleString != null)
            {
                var nt = "";

                var rule = "";

                var i = ruleString.IndexOf(":");

                if (i > 0)
                {
                    nt = ruleString.Substring(0, i).Trim();
                    rule = ruleString.Substring(i + 1);
                    result = new Tuple<string, string>(nt, rule);
                }
            }

            return result;
        }

        private Rule<T> BuildNonTerminal(Tuple<string, string> ntAndRule)
        {
            var rule = new Rule<T>();

            rule.RuleString = $"{ntAndRule.Item1} : {ntAndRule.Item2}";

            var clauses = new List<IClause<T>>();

            var ruleString = ntAndRule.Item2;

            var clausesString = ruleString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in clausesString)
            {
                IClause<T> clause = null;

                var isTerminal = false;

                try
                {
                    if (_tokens.Any(a => a.ViewName == item))
                    {
                        isTerminal = true;
                    }
                }
                catch (ArgumentException)
                {
                    isTerminal = false;
                }

                if (isTerminal)
                {
                    var token = (T)_tokens.FirstOrDefault(f => f.ViewName == item);

                    clause = new TerminalClause<T>(token);
                }
                else
                {
                    clause = new NonTerminalClause<T>(item);
                }

                if (clause != null) clauses.Add(clause);
            }

            rule.Clauses = clauses;

            return rule;
        }

        private BuildResult<Parser<T>> CheckParser(BuildResult<Parser<T>> result)
        {
            var checkers = new List<ParserChecker<T>>();
            checkers.Add(CheckUnreachable);
            checkers.Add(CheckNotFound);

            if (result.Result != null && !result.IsError)
                foreach (var checker in checkers)
                    if (checker != null)
                        result.Result.Configuration.NonTerminals.Values.ToList()
                            .ForEach(nt => result = checker(result, nt));
            return result;
        }

        private BuildResult<Parser<T>> CheckUnreachable(BuildResult<Parser<T>> result,
            NonTerminal<T> nonTerminal)
        {
            var conf = result.Result.Configuration;
            var found = false;
            if (nonTerminal.Name != conf.StartingRule)
            {
                foreach (var nt in result.Result.Configuration.NonTerminals.Values.ToList())
                    if (nt.Name != nonTerminal.Name)
                    {
                        found = NonTerminalReferences(nt, nonTerminal.Name);
                        if (found) break;
                    }

                if (!found)
                    result.AddError(new ParserInitializationError(ErrorLevel.Warn,
                        string.Format("non terminal [{0}] is never used", nonTerminal.Name),
                        ErrorCodes.NotAnError));
            }

            return result;
        }


        private static bool NonTerminalReferences(NonTerminal<T> nonTerminal, string referenceName)
        {
            var found = false;
            var iRule = 0;
            while (iRule < nonTerminal.Rules.Count && !found)
            {
                var rule = nonTerminal.Rules[iRule];
                var iClause = 0;
                while (iClause < rule.Clauses.Count && !found)
                {
                    var clause = rule.Clauses[iClause];
                    if (clause is NonTerminalClause<T> ntClause)
                    {
                        found = ntClause.NonTerminalName == referenceName;
                    }

                    iClause++;
                }
                iRule++;
            }

            return found;
        }


        private BuildResult<Parser<T>> CheckNotFound(BuildResult<Parser<T>> result,
            NonTerminal<T> nonTerminal)
        {
            var conf = result.Result.Configuration;
            foreach (var rule in nonTerminal.Rules)
                foreach (var clause in rule.Clauses)
                    if (clause is NonTerminalClause<T> ntClause)
                        if (!conf.NonTerminals.ContainsKey(ntClause.NonTerminalName))
                            result.AddError(new ParserInitializationError(ErrorLevel.Error,
                                string.Format("{0} references from {1} does not exist", ntClause.NonTerminalName, rule.RuleString),
                                ErrorCodes.ParserReferenceNotFound));
            return result;
        }
    }
}