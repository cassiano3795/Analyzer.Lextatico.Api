using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Sly.Lexer.Fsm.TransitionCheck
{
    public class TransitionAnyExcept : AbstractTransitionCheck
    {
        private readonly List<char> TokenExceptions;

        public TransitionAnyExcept(params char[] tokens)
        {
            TokenExceptions = new List<char>();
            TokenExceptions.AddRange(tokens);
        }

        public override bool Match(char input)
        {
            return !TokenExceptions.Contains(input);
        }
    }
}