using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lextatico.Application.Dtos.TerminalToken;
using Lextatico.Application.Services.Interfaces;
using Lextatico.Domain.Interfaces.Services;

namespace Lextatico.Application.Services
{
    public class TerminalTokenAppService : ITerminalTokenAppService
    {
        private readonly IMapper _mapper;
        private readonly ITerminalTokenService _terminalTokenService;

        public TerminalTokenAppService(IMapper mapper, ITerminalTokenService terminalTokenService)
        {
            _mapper = mapper;
            _terminalTokenService = terminalTokenService;
        }

        public async Task<TerminalTokenDto> GetTerminalToken(Guid terminalTokenId)
        {
            var terminalToken = _mapper.Map<TerminalTokenDto>(await _terminalTokenService.GetByIdAsync(terminalTokenId));

            return terminalToken;
        }

        public async Task<IEnumerable<TerminalTokenDto>> GetTerminalTokens()
        {
            var terminalTokens = _mapper.Map<IEnumerable<TerminalTokenDto>>(await _terminalTokenService.GetAllAsync());

            return terminalTokens;
        }
    }
}
