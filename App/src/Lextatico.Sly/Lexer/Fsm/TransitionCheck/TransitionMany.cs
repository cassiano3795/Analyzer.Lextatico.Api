using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Sly.Lexer.Fsm.TransitionCheck
{
    public class TransitionMany : AbstractTransitionCheck
    {
        public TransitionMany(char[] transitionTokens)
        {
            _transitionTokens = transitionTokens;
        }

        private readonly char[] _transitionTokens;

        public override bool Match(char input) => _transitionTokens.Contains(input);
    }
}