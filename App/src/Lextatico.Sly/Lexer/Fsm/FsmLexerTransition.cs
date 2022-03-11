using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer.Fsm.TransitionCheck;

namespace Lextatico.Sly.Lexer
{
    public class FsmLexerTransition
    {
        public FsmLexerTransition(AbstractTransitionCheck check, int fromNode, int toNode)
        {
            FromNode = fromNode;
            ToNode = toNode;
            Check = check;
        }

        public AbstractTransitionCheck Check { get; set; }
        public int FromNode { get; set; }
        public int ToNode { get; set; }

        internal bool Match(char token, ReadOnlyMemory<char> value)
        {
            return Check.Check(token, value);
        }

        internal bool Match(char token)
        {
            return Check.Match(token);
        }
    }
}