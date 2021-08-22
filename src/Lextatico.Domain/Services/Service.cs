using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lextatico.Domain.Interfaces.Repositories;
using Lextatico.Domain.Interfaces.Services;
using Lextatico.Domain.Models;

namespace Lextatico.Domain.Services
{
    public class Service<T> : IService<T> where T : BaseModel
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _repository.SelectAllAsync();
        }

        public virtual async Task<T> GetAsync(Guid id)
        {
            return await _repository.SelectAsync(id);
        }

        public virtual async Task<T> PostAsync(T item)
        {
            return await _repository.InsertAsync(item);
        }

        public virtual async Task<T> PutAsync(T item)
        {
            return await _repository.UpdateAsync(item);
        }
    }
}
