using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lextatico.Domain.Models;

namespace Lextatico.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : Base
    {
        Task<IEnumerable<T>> SelectAllAsync();
        Task<IEnumerable<T>> SelectAllOrderByAscendingAsync<TKey>(Expression<Func<T, TKey>> expression);
        Task<IEnumerable<T>> SelectAllOrderByDescendingAsync<TKey>(Expression<Func<T, TKey>> expression);
        Task<bool> InsertAsync(T item);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(Guid id);
        Task<T> SelectByIdAsync(Guid id);
        Task<bool> Exists(Guid id);
    }
}
