using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;
using Xunit;

namespace Lextatico.Tests.Lextatico.Sly.Lexer
{
    public class LextaticoLexerBuilderTests
    {
        [Fact]
        public void LextaticoLexerBuilder_Build()
        {
            var tokens = new List<Token>
            {
                new Token("Int", "INT", "Qualquer coisa", "int", TokenType.KeyWord),
                new Token("Identificador", "ID2", "Qualquer coisa", "id", TokenType.Identifier),
                new Token("Ponto e vírgula", "SEMICOLON", "Qualquer coisa", ";", TokenType.SugarToken)
            };

            var lexerBuilder = new LextaticoLexerBuilder<Token>(tokens);

            var result = lexerBuilder.Build();

            Assert.True(result.IsOk);
        }

        [Fact]
        public void LextaticoLexerBuilder_Tokenize()
        {
            var tokens = new List<Token>
            {
                new Token("Int", "INT", "Qualquer coisa", "int", TokenType.KeyWord),
                new Token("Identificador", "ID2", "Qualquer coisa", "id", TokenType.Identifier),
                new Token("Ponto e vírgula", "SEMICOLON", "Qualquer coisa", ";", TokenType.SugarToken)
            };

            var lexerBuilder = new LextaticoLexerBuilder<Token>(tokens);

            var result = lexerBuilder.Build();

            var tokenResult = result.Result.Tokenize("int abc;");
        }
    }
}