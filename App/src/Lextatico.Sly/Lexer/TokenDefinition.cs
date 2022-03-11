using System.Text.RegularExpressions;

namespace Lextatico.Sly.Lexer
{
    public class TokenDefinition
    {
        public TokenDefinition(Token token, string regex, bool isIgnored = false, bool isEndOfLine = false)
        {
            Token = token;
            Regex = new Regex(regex, RegexOptions.Compiled);
            IsIgnored = isIgnored;
            IsEndOfLine = isEndOfLine;
        }

        public bool IsIgnored { get; }

        public bool IsEndOfLine { get; }

        public Regex Regex { get; }

        public Token Token { get; }
    }
}