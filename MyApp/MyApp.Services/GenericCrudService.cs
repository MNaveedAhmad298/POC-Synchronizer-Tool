using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public class GenericCrudService<T> where T : BaseEntity
    {
        private readonly IGenericCrudRepository<T> _repo;

        public GenericCrudService(IGenericCrudRepository<T> repo)
        {
            _repo = repo;
        }

        public Task<List<T>> GetAllAsync() => _repo.GetAllActiveAsync();

        public Task AddAsync(T entity) => _repo.AddAsync(entity);

        public Task UpdateAsync(T entity) => _repo.UpdateAsync(entity);

        public Task DeleteAsync(T entity) => _repo.SoftDeleteAsync(entity);
    }
}
