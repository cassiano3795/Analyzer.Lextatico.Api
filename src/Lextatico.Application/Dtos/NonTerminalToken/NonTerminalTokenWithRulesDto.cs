using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Application.Dtos.NonTerminalToken
{
    public class NonTerminalTokenDetailWithRulesDto : NonTerminalTokenDetailDto
    {
        public IEnumerable<NonTerminalTokenRuleDto> NonTerminalTokenRules { get; set; }
    }
}