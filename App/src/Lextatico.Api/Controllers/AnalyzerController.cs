using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lextatico.Api.Controllers.Base;
using Lextatico.Application.Dtos.Analyzer;
using Lextatico.Application.Dtos.Filter;
using Lextatico.Application.Services.Interfaces;
using Lextatico.Domain.Dtos.Message;
using Microsoft.AspNetCore.Mvc;

namespace Lextatico.Api.Controllers
{
    public class AnalyzerController : LextaticoController
    {
        private readonly IAnalyzerAppService _analyzerAppService;
        public AnalyzerController(IMessage message, IAnalyzerAppService analyzerAppService)
            : base(message)
        {
            _analyzerAppService = analyzerAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnalyzers([FromQuery] PaginationFilterDto pagination)
        {
            var (analyzers, total) = await _analyzerAppService
                .GetAnalyzersPaggedByLoggedUserAsync(pagination.Page, pagination.Size);

            return ReturnOk(analyzers, pagination, total);
        }

        [HttpGet, Route("{analyzerId:guid}")]
        public async Task<IActionResult> GetAnalyzer(Guid analyzerId)
        {
            var analyzer = await _analyzerAppService.GetAnalyzerByIdAsync(analyzerId);

            return ReturnOk(analyzer);
        }

        [HttpPost]
        public async Task<IActionResult> PostAnalyzer(AnalyzerWithTerminalTokensAndNonTerminalTokens analyzer)
        {
            await _analyzerAppService.CreateAnalyzerAsync(analyzer);

            return ReturnCreated();
        }

        [HttpPut, Route("{analyzerId:guid}")]
        public async Task<IActionResult> PutAnalyzer([FromRoute] Guid analyzerId, AnalyzerWithTerminalTokensAndNonTerminalTokens analyzer)
        {
            analyzer.Id = analyzerId;

            await _analyzerAppService.UpdateAnalyzerAsync(analyzer);

            return ReturnOk();
        }

        [HttpDelete, Route("{analyzerId:guid}")]
        public async Task<IActionResult> DeleteAnalyzer(Guid analyzerId)
        {
            await _analyzerAppService.DeleteAnalyzerByIdAsync(analyzerId);

            return ReturnOk();
        }

        [HttpPost, Route("deleteBulk")]
        public async Task<IActionResult> DeleteAnalyzers([FromBody] IEnumerable<Guid> analyzerIds)
        {
            await _analyzerAppService.DeleteAnalyzersByIdAsync(analyzerIds);

            return ReturnOk();
        }

        [HttpPost, Route("{analyzerId:guid}/test")]
        public async Task<IActionResult> TestAnalyzer([FromRoute] Guid analyzerId, TesteAnalyzerDto testeAnalyzer)
        {
            var result = await _analyzerAppService.TestAnalyzerByIdAsync(analyzerId, testeAnalyzer);

            return ReturnOk(result);
        }
    }
}
