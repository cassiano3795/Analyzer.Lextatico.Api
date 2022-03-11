using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Sly.Lexer.Fsm.TransitionCheck
{
    public class TransitionMultiRange : AbstractTransitionCheck
    {
        private (char start, char end)[] ranges;

        public TransitionMultiRange(params (char start, char end)[] ranges)
        {
            this.ranges = ranges;
        }

        public override bool Match(char input)
        {
            bool match = false;
            int i = 0;
            while (!match && i < ranges.Length)
            {
                var range = ranges[i];
                match = match || input.CompareTo(range.start) >= 0 && input.CompareTo(range.end) <= 0;
                i++;
            }

            return match;
        }
    }
}