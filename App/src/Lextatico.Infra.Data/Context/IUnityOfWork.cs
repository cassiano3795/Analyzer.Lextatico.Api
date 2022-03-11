using System.Threading;
using System.Threading.Tasks;

namespace Lextatico.Infra.Data.Context
{
    public interface IUnityOfWork
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
    }
}
