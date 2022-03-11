using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Sly.Lexer.Fsm.TransitionCheck
{
    public class TransitionAny : AbstractTransitionCheck
    {
        public override bool Match(char input)
        {
            return true;
        }
    }
}