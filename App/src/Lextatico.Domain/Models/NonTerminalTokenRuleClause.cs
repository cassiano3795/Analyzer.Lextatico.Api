using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Domain.Models
{
    public class NonTerminalTokenRuleClause : Base
    {
        public string Name { get; private set; }
        public int Sequence { get; private set; }
        public bool IsTerminalToken { get; private set; }
        public Guid? TerminalTokenId { get; private set; }
        public virtual TerminalToken TerminalToken { get; private set; }
        public Guid? NonTerminalTokenId { get; private set; }
        public virtual NonTerminalToken NonTerminalToken { get; private set; }
        public Guid NonTerminalTokenRuleId { get; private set; }
        public virtual NonTerminalTokenRule NonTerminalTokenRule { get; private set; }

        public void SetNonTerminalTokenRuleId(Guid nonTerminalTokenRuleId) => NonTerminalTokenRuleId = nonTerminalTokenRuleId;
    }
}
