using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Domain.Dtos.Message;
using Lextatico.Domain.Interfaces.Repositories;
using Lextatico.Domain.Interfaces.Services;
using Lextatico.Domain.Models;

namespace Lextatico.Domain.Services
{
    public class TerminalTokenService : Service<TerminalToken>, ITerminalTokenService
    {
        public TerminalTokenService(ITerminalTokenRepository terminalTokenRepository, IMessage message) 
            : base(terminalTokenRepository)
        {
        }
    }
}