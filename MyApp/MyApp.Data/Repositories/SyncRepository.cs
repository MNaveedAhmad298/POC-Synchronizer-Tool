using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;

namespace MyApp.Data.Repositories
{
    public class SyncRepository<T> : ISyncRepository<T> where T : BaseEntity
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public SyncRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<T>> GetUnsyncedAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await context.Set<T>()
                .AsNoTracking()
                .Where(e => !e.IsSynced)
                .ToListAsync();
        }

        public async Task MarkAsSyncedAsync(List<T> entities)
        {
            using var context = _contextFactory.CreateDbContext();

            foreach (var entity in entities)
            {
                entity.IsSynced = true;
                context.Update(entity);
            }

            await context.SaveChangesAsync();
        }
    }
}
