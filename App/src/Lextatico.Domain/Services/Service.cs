using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lextatico.Domain.Dtos.Message;
using Lextatico.Domain.Exceptions;
using Lextatico.Domain.Interfaces.Repositories;
using Lextatico.Domain.Interfaces.Services;
using Lextatico.Domain.Models;

namespace Lextatico.Domain.Services
{
    public class Service<T> : IService<T> where T : Base
    {
        private readonly IBaseRepository<T> _repository;

        public Service(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.SelectAllAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _repository.SelectByIdAsync(id);
        }

        public virtual async Task<bool> CreateAsync(T item)
        {
            return await _repository.InsertAsync(item);
        }

        public virtual async Task<bool> UpdateAsync(T item)
        {
            var exists = await _repository.ExistsAsync(item.Id);

            // if (!exists)
            //     return false;
            // throw new NotFoundException($"{item.Id} não encontrado.");

            return await _repository.UpdateAsync(item);
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var exists = await _repository.ExistsAsync(id);

            // if (!exists)
            //     throw new NotFoundException($"{id} não encontrado.");

            return await _repository.DeleteAsync(id);
        }

        public virtual async Task<bool> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _repository.DeleteAsync(ids);
        }
    }
}
