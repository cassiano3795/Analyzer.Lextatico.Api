using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;

namespace Lextatico.Sly.Parser.Syntax.Grammar
{
    public class NonTerminalClause<T> : IClause<T> where T : Token
    {
        public NonTerminalClause(string name)
        {
            NonTerminalName = name;
        }

        public string NonTerminalName { get; set; }

        public bool IsGroup { get; set; } = false;

        public bool MayBeEmpty() => false;
    }
}