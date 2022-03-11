using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Api.Controllers.Base;
using Lextatico.Application.Services.Interfaces;
using Lextatico.Domain.Dtos.Message;
using Microsoft.AspNetCore.Mvc;

namespace Lextatico.Api.Controllers
{
    public class TerminalTokenController : LextaticoController
    {
        private readonly ITerminalTokenAppService _terminalTokenAppService;

        public TerminalTokenController(IMessage message, ITerminalTokenAppService terminalTokenAppService)
            : base(message)
        {
            _terminalTokenAppService = terminalTokenAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTerminalTokens()
        {
            var terminalTokens = await _terminalTokenAppService.GetTerminalTokens();

            return ReturnOk(terminalTokens);
        }

        [HttpGet, Route("{terminalTokenId:guid}")]
        public async Task<IActionResult> GetTerminalToken(Guid terminalTokenId)
        {
            var terminalToken = await _terminalTokenAppService.GetTerminalToken(terminalTokenId);

            return ReturnOk(terminalToken);
        }
    }
}