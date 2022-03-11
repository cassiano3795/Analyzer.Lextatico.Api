using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Application.Dtos.NonTerminalToken;
using Lextatico.Application.Dtos.TerminalToken;

namespace Lextatico.Application.Dtos.Analyzer
{
    public class AnalyzerWithTerminalTokensAndNonTerminalTokens : AnalyzerDto
    {
        public IEnumerable<TerminalTokenDto> TerminalTokens { get; set; }
        public IEnumerable<NonTerminalTokenWithRulesAndClausesDto> NonTerminalTokens { get; set; }
    }
}
