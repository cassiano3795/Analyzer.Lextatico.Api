using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;
using Lextatico.Sly.Parser.Syntax.Grammar;

namespace Lextatico.Sly.Parser.Builder
{
    public class NonTerminal<T> where T : Token
    {
        public NonTerminal(string name, List<Rule<T>> rules)
        {
            Name = name;
            Rules = rules;
        }

        public NonTerminal(string name) : this(name, new List<Rule<T>>())
        {
        }

        public string Name { get; set; }

        public List<Rule<T>> Rules { get; set; }

        public bool IsSubRule { get; set; }

        public List<T> PossibleLeadingTokens => Rules.SelectMany(r => r.PossibleLeadingTokens).ToList();
    }
}