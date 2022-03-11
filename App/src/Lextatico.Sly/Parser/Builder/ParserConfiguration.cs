using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;

namespace Lextatico.Sly.Parser.Builder
{
    public class ParserConfiguration<T> where T : Token
    {
        public string StartingRule { get; set; }
        public Dictionary<string, NonTerminal<T>> NonTerminals { get; set; }


        public void AddNonTerminalIfNotExists(NonTerminal<T> nonTerminal)
        {
            if (!NonTerminals.ContainsKey(nonTerminal.Name)) NonTerminals[nonTerminal.Name] = nonTerminal;
        }
    }
}