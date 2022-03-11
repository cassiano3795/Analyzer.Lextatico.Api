using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;
using Lextatico.Sly.Lexer.Fsm;
using Lextatico.Sly.Parser.Builder;

namespace Lextatico.Sly.Parser
{
    public interface ISyntaxParser<T> where T : Token
    {
        string StartingNonTerminal { get; set; }

        SyntaxParseResult<T> Parse(IList<LexerToken<T>> tokens, string startingNonTerminal = null);

        void Init(ParserConfiguration<T> configuration, string root);
    }
}