using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lextatico.Application.Dtos.TerminalToken
{
    public class TerminalTokenDto : BaseDto
    {
        public string Name { get; set; }
        public string ViewName { get; set; }
        public string Resume { get; set; }
        public string Lexeme { get; set; }
        public TokenType TokenType { get; set; }
        public string TokenTypeString => TokenType.ToString();
        public IdentifierType IdentifierType { get; set; }
        public string IdentifierTypeString => IdentifierType.ToString();
    }
}
