using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Application.Dtos.TerminalToken;

namespace Lextatico.Application.Dtos.NonTerminalToken
{
    public class NonTerminalTokenRuleClauseDto : BaseDto
    {
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool IsTerminalToken { get; set; }
        public Guid? TerminalTokenId { get; set; }
        public TerminalTokenDto TerminalToken { get; set; }
        public Guid? NonTerminalTokenId { get; set; }
        public NonTerminalTokenDto NonTerminalToken { get; set; }
        public Guid NonTerminalTokenRuleId { get; set; }
    }
}
