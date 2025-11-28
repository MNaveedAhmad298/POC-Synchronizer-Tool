using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Data.Repositories
{
    public class GenericCrudRepository<T> : IGenericCrudRepository<T> where T : BaseEntity
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory; 

        public GenericCrudRepository(IDbContextFactory<AppDbContext> contextFactory) 
        {
            _contextFactory = contextFactory; 
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            using var context = _contextFactory.CreateDbContext(); 
            return await context.Set<T>().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id && !e.IsDeleted);
        }

        public async Task<List<T>> GetAllActiveAsync()
        {
            using var context = _contextFactory.CreateDbContext(); 
            return await context.Set<T>().Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            using var context = _contextFactory.CreateDbContext(); 
            entity.IsSynced = false;
            entity.IsDeleted = false;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync(); 
        }

        public async Task UpdateAsync(T entity)
        {
            using var context = _contextFactory.CreateDbContext(); 
            entity.IsSynced = false;
            entity.UpdatedAt = DateTime.UtcNow;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync(); 
        }

        public async Task SoftDeleteAsync(T entity)
        {
            using var context = _contextFactory.CreateDbContext(); 
            entity.IsDeleted = true;
            entity.IsSynced = false;
            entity.UpdatedAt = DateTime.UtcNow;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync(); 
        }
    }
}
