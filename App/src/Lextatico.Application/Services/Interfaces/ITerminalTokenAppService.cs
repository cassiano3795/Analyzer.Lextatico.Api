using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Application.Dtos.TerminalToken;

namespace Lextatico.Application.Services.Interfaces
{
    public interface ITerminalTokenAppService
    {
        Task<TerminalTokenDto> GetTerminalToken(Guid terminalTokenId);
        Task<IEnumerable<TerminalTokenDto>> GetTerminalTokens();
    }
}
