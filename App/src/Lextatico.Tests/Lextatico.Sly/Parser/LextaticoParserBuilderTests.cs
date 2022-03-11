using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;
using Lextatico.Sly.Parser.Builder;
using Xunit;

namespace Lextatico.Tests.Lextatico.Sly.Parser
{
    public class LextaticoParserBuilderTests
    {
        [Fact]
        public void LextaticoParserBuilder_Build()
        {
            var tokens = new List<Token>
            {
                new Token("Int", "INT", "Qualquer coisa", "int", TokenType.KeyWord),
                new Token("Ponto e vírgula", "SEMICOLON", "Qualquer coisa", ";", TokenType.SugarToken)
            };

            var productionRules = new List<string>
            {
                "program: INT SEMICOLON"
            };

            var lextaticoParserBuilder = new LextaticoParserBuilder<Token>(tokens);

            var result = lextaticoParserBuilder.BuildParser("program", ParserType.LlRecursiveDescent, productionRules);

            Assert.True(result.IsOk);
        }

        [Fact]
        public void LextaticoParserBuilder_Parse()
        {
            var tokens = new List<Token>
            {
                new Token("Int", "INT", "Qualquer coisa", "int", TokenType.KeyWord),
                new Token("Identificador 1", "ID3", "Qualquer coisa", "Qualquer coisa", TokenType.Identifier, IdentifierType.AlphaNumDash),
                new Token("Atribuição", "ASSIGN", "Qualquer coisa", "=", TokenType.SugarToken),
                new Token("Inteiro", "NINTEGER", "Qualquer coisa", "Qualquer coisa", TokenType.Integer),
                new Token("Ponto e vírgula", "SEMICOLON", "Qualquer coisa", ";", TokenType.SugarToken)
            };

            var productionRules = new List<string>
            {
                "program: INT ID3 ASSIGN NINTEGER SEMICOLON",
            };

            var lextaticoParserBuilder = new LextaticoParserBuilder<Token>(tokens);

            var buildResult = lextaticoParserBuilder.BuildParser("program", ParserType.LlRecursiveDescent, productionRules);

            var parseResult = buildResult.Result.Parse("int _abc9 = 12;", "program");

            var message = parseResult.Errors.FirstOrDefault()?.ErrorMessage ?? "";

            Assert.True(parseResult.IsOk);
        }
    }
}