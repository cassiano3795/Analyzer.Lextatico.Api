using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;
using Lextatico.Sly.Lexer.Fsm;
using Lextatico.Sly.Parser.Builder;
using Lextatico.Sly.Result;

namespace Lextatico.Sly.Parser
{
    public class Parser<T> where T : Token
    {
        public Parser(ISyntaxParser<T> syntaxParser)
        {
            SyntaxParser = syntaxParser;
        }

        public ILexer<T> Lexer { get; set; }
        public ISyntaxParser<T> SyntaxParser { get; set; }
        public ParserConfiguration<T> Configuration { get; set; }

        public virtual BuildResult<ParserConfiguration<T>> BuildExpressionParser(
            BuildResult<Parser<T>> result, string startingRule = null)
        {
            var exprResult = new BuildResult<ParserConfiguration<T>>(Configuration);

            SyntaxParser.Init(exprResult.Result, startingRule);

            if (startingRule != null)
            {
                Configuration.StartingRule = startingRule;
                SyntaxParser.StartingNonTerminal = startingRule;
            }

            if (exprResult.IsError)
                result.AddErrors(exprResult.Errors);
            else
                result.Result.Configuration = Configuration;
            return exprResult;
        }

        public ParseResult<T> Parse(string source, string startingNonTerminal = null)
        {
            ParseResult<T> result;
            var lexingResult = Lexer.Tokenize(source);
            if (lexingResult.IsError)
            {
                result = new ParseResult<T>();
                result.IsError = true;
                result.Errors.Add(lexingResult.Error);
                return result;
            }

            var tokens = lexingResult.Tokens;
            var position = 0;
            var tokensWithoutComments = new List<LexerToken<T>>();
            for (var i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                // if (!token.IsComment || token.Notignored)
                // {
                token.PositionInTokenFlow = position;
                tokensWithoutComments.Add(token);
                position++;
                // }
            }

            result = Parse(tokensWithoutComments, startingNonTerminal);


            return result;
        }

        public ParseResult<T> Parse(IList<LexerToken<T>> tokens, string startingNonTerminal = null)
        {
            var result = new ParseResult<T>();

            var syntaxResult = SyntaxParser.Parse(tokens, startingNonTerminal);

            if (!syntaxResult.IsError)
            {
                result.IsError = false;
            }
            else
            {
                var unexpectedTokens = syntaxResult.Errors.Cast<UnexpectedTokenSyntaxError<T>>().ToList();
                var byEnding = unexpectedTokens.GroupBy(x => x.UnexpectedToken.Position).OrderBy(x => x.Key);
                var errors = new List<ParseError>();
                foreach (var expecting in byEnding)
                {
                    var expectingTokens = expecting.SelectMany(x => x.ExpectedTokens).Distinct();
                    var expected = new UnexpectedTokenSyntaxError<T>(expecting.First().UnexpectedToken, expectingTokens.ToArray());
                    errors.Add(expected);
                }

                result.Errors.AddRange(errors);
                result.IsError = true;
            }

            return result;
        }
    }
}