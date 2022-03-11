using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;
using Lextatico.Sly.Lexer.Fsm;

namespace Lextatico.Sly.Parser.Syntax.Grammar
{
    public class TerminalClause<T> : IClause<T> where T : Token
    {
        public TerminalClause(T token)
        {
            ExpectedToken = token;
        }

        public TerminalClause(T token, bool discard) : this(token)
        {
            Discarded = discard;
        }

        public T ExpectedToken { get; set; }

        public bool Discarded { get; set; }

        public virtual bool MayBeEmpty() => false;

        public virtual bool Check(LexerToken<T> nextToken) => nextToken.Result?.Equals(ExpectedToken) ?? false;
    }
}