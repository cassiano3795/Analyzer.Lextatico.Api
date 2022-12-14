using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analyzer.Lextatico.Application.Dtos.NonTerminalToken
{
    public class NonTerminalTokenWithRulesAndClausesDto : NonTerminalTokenDto
    {
        public IEnumerable<NonTerminalTokenRuleWithClausesDto> NonTerminalTokenRules { get; set; }
    }
}
