using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lextatico.Domain.Interfaces.Repositories;
using Lextatico.Domain.Models;
using Lextatico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lextatico.Infra.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        protected readonly LextaticoContext _lextaticoContext;
        protected DbSet<T> _dataSet;

        public BaseRepository(LextaticoContext lextaticoContext)
        {
            _lextaticoContext = lextaticoContext;
            _dataSet = lextaticoContext.Set<T>();
        }

        public virtual async Task<T> SelectByIdAsync(Guid id) =>
            await _dataSet.FindAsync(id);

        public virtual async Task<IEnumerable<T>> SelectByIdAsync(IEnumerable<Guid> ids) =>
            await _dataSet.Where(x => ids.Contains(x.Id)).ToListAsync();

        public virtual async Task<IEnumerable<T>> SelectAllAsync() =>
            await _dataSet.ToListAsync();

        public virtual async Task<IEnumerable<T>> SelectAllOrderedAsync() =>
            await _dataSet.ToListAsync();

        public virtual async Task<IEnumerable<T>> SelectAllOrderByAscendingAsync<TKey>(Expression<Func<T, TKey>> expression) =>
            await _dataSet.OrderBy(expression).ToListAsync();

        public virtual async Task<IEnumerable<T>> SelectAllOrderByDescendingAsync<TKey>(Expression<Func<T, TKey>> expression) =>
            await _dataSet.OrderByDescending(expression).ToListAsync();

        public virtual async Task<bool> InsertAsync(T item)
        {
            await _dataSet.AddAsync(item);

            var result = await _lextaticoContext.SaveChangesAsync();

            return result >= 0;
        }

        public virtual async Task<bool> UpdateAsync(T item)
        {
            var itemDb = await SelectByIdAsync(item.Id);

            if (itemDb == null)
                return false;

            item.SetCreatedAt(itemDb.CreatedAt);

            _lextaticoContext.Entry(itemDb).CurrentValues.SetValues(item);

            var result = await _lextaticoContext.SaveChangesAsync();

            return result >= 0;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var itemDb = await SelectByIdAsync(id);

            if (itemDb == null)
                return false;

            _dataSet.Remove(itemDb);

            var result = await _lextaticoContext.SaveChangesAsync();

            return result >= 0;
        }

        public virtual async Task<bool> DeleteAsync(IEnumerable<Guid> ids)
        {
            var itemsDb = await SelectByIdAsync(ids);

            if (!itemsDb.Any())
                return false;

            _dataSet.RemoveRange(itemsDb);

            var result = await _lextaticoContext.SaveChangesAsync();

            return result > 0;
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await Exists(id);
        }

        private async Task<bool> Exists(Guid id)
        {
            var exist = await _dataSet.CountAsync(c => c.Id == id);

            return exist > 0;
        }
    }
}
