using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer.Fsm;

namespace Lextatico.Sly.Lexer
{
    public class LexerResult<T>
        where T : Token
    {
        public LexerResult()
        { }

        public LexerResult(List<LexerToken<T>> tokens)
        {
            Tokens = tokens;
        }

        public LexerResult(LexicalError error)
        {
            Error = error;
        }

        public bool IsError => Error != null;

        public bool IsOk => !IsError;

        public LexicalError Error { get; internal set; }

        public IList<LexerToken<T>> Tokens { get; set; }


    }
}