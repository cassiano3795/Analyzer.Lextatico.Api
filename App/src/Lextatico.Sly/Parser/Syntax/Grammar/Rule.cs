using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;

namespace Lextatico.Sly.Parser.Syntax.Grammar
{
    public class Rule<T> where T : Token
    {
        public Rule()
        {
            Clauses = new List<IClause<T>>();
        }

        public bool IsByPassRule { get; set; } = false;

        public bool IsExpressionRule { get; set; }

        // public Affix ExpressionAffix { get; set; }

        public string RuleString { get; set; }

        public string Key
        {
            get
            {
                var key = string.Join("_", Clauses.Select(c => c.ToString()));

                if (Clauses.Count == 1)
                    key += "_";

                return IsExpressionRule ? key.Replace(" | ", "_") : key;
            }
        }

        public List<IClause<T>> Clauses { get; set; }

        public List<T> PossibleLeadingTokens { get; set; }

        public string NonTerminalName { get; set; }

        public bool MayBeEmpty => Clauses == null
                                  || Clauses.Count == 0
                                  || Clauses.Count == 1 && Clauses[0].MayBeEmpty();

    }
}