using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Application.Dtos.NonTerminalToken
{
    public class NonTerminalTokenDto : BaseDto
    {
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsStart { get; set; }
    }
}
