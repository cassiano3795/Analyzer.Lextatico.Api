using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Domain.Models
{
    public class NonTerminalTokenRule : Base
    {
        public string Name { get; private set; }
        public int Sequence { get; private set; }
        public Guid NonTerminalTokenId { get; private set; }
        public virtual NonTerminalToken NonTerminalToken { get; private set; }
        public virtual ICollection<NonTerminalTokenRuleClause> NonTerminalTokenRuleClauses { get; } = new List<NonTerminalTokenRuleClause>();

        public void SetNonTerminalTokenId(Guid nonTerminalTokenId) => NonTerminalTokenId = nonTerminalTokenId;
    }
}
