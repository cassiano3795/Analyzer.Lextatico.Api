using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Sly.Lexer
{
    public class FsmLexerNode<T>
        where T : struct
    {
        public FsmLexerNode()
        {
        }

        public FsmLexerNode(T value)
        {
            Value = value;
        }

        public int Id { get; set; } = 0;
        public T Value { get; set; }
        public Token Token { get; set; }
        public bool IsEnd { get; set; } = false;
        public bool IsStart { get; set; } = false;
        public string Mark { get; internal set; }
        public bool IsLineEnding { get; set; }
    }
}