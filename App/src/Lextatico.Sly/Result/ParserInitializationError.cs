using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Sly.Result
{
    public class ParserInitializationError : InitializationError
    {
        public ParserInitializationError(ErrorLevel level, string message, ErrorCodes code) : base(level, message, code)
        {
        }
    }
}