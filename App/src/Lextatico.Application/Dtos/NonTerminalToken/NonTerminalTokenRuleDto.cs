using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Application.Dtos.NonTerminalToken
{
    public class NonTerminalTokenRuleDto : BaseDto
    {
        public string Name { get; set; }
        public int Sequence { get; set; }
    }
}
