using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Application.Dtos.Analyzer;

namespace Lextatico.Application.Services.Interfaces
{
    public interface IAnalyzerAppService : IAppService
    {
        Task<AnalyzerDto> GetAnalyzerByIdAsync(Guid analyzerId);
        Task<IEnumerable<AnalyzerDto>> GetAnalyzersByLoggedUserAsync();
        Task<(IEnumerable<AnalyzerDto>, int)> GetAnalyzersPaggedByLoggedUserAsync(int page, int size);
        Task<bool> CreateAnalyzerAsync(AnalyzerWithTerminalTokensAndNonTerminalTokens analyzer);
        Task<bool> UpdateAnalyzerAsync(AnalyzerWithTerminalTokensAndNonTerminalTokens analyzer);
        Task<bool> DeleteAnalyzerByIdAsync(Guid analyzerId);
    }
}
