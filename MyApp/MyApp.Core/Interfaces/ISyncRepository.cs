using MyApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core.Interfaces
{
    public interface ISyncRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetUnsyncedAsync();
        Task MarkAsSyncedAsync(List<T> entities);
    }
}
