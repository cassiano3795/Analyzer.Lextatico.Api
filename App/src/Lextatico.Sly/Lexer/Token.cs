using System;
using System.ComponentModel;
using System.Globalization;

namespace Lextatico.Sly.Lexer
{
    public class Token
    {
        public Token()
        {
        }

        public Token(string name, string viewName, string resume, string lexeme, TokenType tokenType, IdentifierType? identifierType = null)
        {
            Name = name;
            ViewName = viewName;
            Resume = resume;
            Lexeme = lexeme;
            TokenType = tokenType;
            IdentifierType = identifierType;
        }

        public string Name { get; }

        public string ViewName { get; }

        public string Resume { get; }

        public string Lexeme { get; }

        public TokenType TokenType { get; }

        public IdentifierType? IdentifierType { get; }

        public override string ToString()
        {
            return $"({Name} - '{ViewName}')";
        }
    }

    public enum TokenType
    {
        [Description("Quando nenhum TokenType é definido. Ex: node \"start\"")]
        Default,
        Identifier,
        String,
        Char,
        Integer,
        Float,
        KeyWord,
        SugarToken
    }

    public enum IdentifierType
    {
        Alpha,
        AlphaNum,
        AlphaNumDash
    }
}
