using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Sly.Result
{
    public class LexerInitializationError : InitializationError
    {
        public LexerInitializationError(ErrorLevel level, string message, ErrorCodes code) : base(level, message, code)
        {
        }
    }
}