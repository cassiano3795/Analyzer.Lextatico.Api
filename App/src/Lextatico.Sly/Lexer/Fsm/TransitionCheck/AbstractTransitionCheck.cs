using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Sly.Lexer.Fsm.TransitionCheck
{
    public abstract class AbstractTransitionCheck
    {
        public abstract bool Match(char input);

        public bool Check(char input, ReadOnlyMemory<char> value)
        {
            var match = true;
            if (match) match = Match(input);
            return match;
        }
    }
}