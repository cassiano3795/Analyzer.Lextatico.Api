using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analyzer.Lextatico.Application.Dtos.NonTerminalToken
{
    public class NonTerminalTokenDetailWithRulesDto : NonTerminalTokenDto
    {
        public IEnumerable<NonTerminalTokenRuleDto> NonTerminalTokenRules { get; set; }
    }
}
