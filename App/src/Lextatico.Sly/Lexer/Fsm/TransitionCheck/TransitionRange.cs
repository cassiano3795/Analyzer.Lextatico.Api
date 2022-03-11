using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Sly.Lexer.Fsm.TransitionCheck
{
    public class TransitionRange : AbstractTransitionCheck
    {
        private readonly char _rangeEnd;
        private readonly char _rangeStart;

        public TransitionRange(char start, char end)
        {
            _rangeStart = start;
            _rangeEnd = end;
        }
        public override bool Match(char input)
            => input.CompareTo(_rangeStart) >= 0 && input.CompareTo(_rangeEnd) <= 0;
    }
}