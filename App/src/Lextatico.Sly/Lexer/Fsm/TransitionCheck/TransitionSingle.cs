using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Sly.Lexer.Fsm.TransitionCheck
{
    public class TransitionSingle : AbstractTransitionCheck
    {
        public TransitionSingle(char transitionToken)
        {
            _transitionToken = transitionToken;
        }

        private readonly char _transitionToken;

        public override bool Match(char input) => input.Equals(_transitionToken);
    }
}