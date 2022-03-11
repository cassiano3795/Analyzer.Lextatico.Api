using System;
using System.Collections.Generic;

namespace Lextatico.Domain.Models
{
    public class TerminalToken : Base
    {
        public TerminalToken()
        {
        }

        public string Name { get; private set; }
        public string ViewName { get; private set; }
        public string Resume { get; private set; }
        public string Lexeme { get; private set; }
        public string TokenType { get; private set; }
        public string IdentifierType { get; private set; }
        public virtual ICollection<AnalyzerTerminalToken> AnalyzerTokens { get; } = new List<AnalyzerTerminalToken>();
        public virtual ICollection<NonTerminalTokenRuleClause> NonTerminalTokenRuleClauses { get; } = new List<NonTerminalTokenRuleClause>();
    }
}
