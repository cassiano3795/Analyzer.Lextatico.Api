using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Sly.Lexer;

namespace Lextatico.Sly.Parser
{
    public class SyntaxParseResult<T> where T : Token
    {
        public bool IsError { get; set; }

        public bool IsOk => !IsError;

        public List<UnexpectedTokenSyntaxError<T>> Errors { get; set; } = new List<UnexpectedTokenSyntaxError<T>>();

        public int EndingPosition { get; set; }

        public bool IsEnded { get; set; }

        public List<T> Expecting { get; set; }

        public void AddExpectings(IEnumerable<T> expected)
        {
            if (Expecting == null)
            {
                Expecting = new List<T>();
            }
            Expecting.AddRange(expected);
        }
    }
}