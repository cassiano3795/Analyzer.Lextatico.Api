using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analyzer.Lextatico.Application.Dtos.NonTerminalToken
{
    public class NonTerminalTokenRuleWithClausesDto : NonTerminalTokenRuleDto
    {
        public IEnumerable<NonTerminalTokenRuleClauseDto> NonTerminalTokenRuleClauses { get; set; }
    }
}
