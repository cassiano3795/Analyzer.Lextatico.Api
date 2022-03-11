using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lextatico.Domain.Models;

namespace Lextatico.Domain.Interfaces.Repositories
{
    public interface IAnalyzerRepository : IBaseRepository<Analyzer>
    {
        Task<IEnumerable<Analyzer>> SelectAnalyzersByUserIdAsync(Guid userId);
        Task<(IEnumerable<Analyzer>, int)> SelectAnalyzersPaggedByUserIdAsync(Guid userId, int page, int size);
    }
}
