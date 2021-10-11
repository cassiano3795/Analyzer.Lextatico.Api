using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Domain.Models
{
    public class NonTerminalTokenRule : Base
    {
        public string Name { get; set; }
        public int Sequence { get; set; }
        public Guid IdNonTerminalToken { get; set; }
        public virtual NonTerminalToken NonTerminalToken { get; set; }
        public virtual ICollection<NonTerminalTokenRuleClause> NonTerminalTokenRuleClauses { get; set; }
    }
}