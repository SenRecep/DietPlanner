using System;
using System.Threading.Tasks;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.DAL.Interfaces
{
    public interface IGenericSingleQueryRepository<T> : IAsyncDisposable
        where T : class, IEntityBase, new()
    {
        public Task<T> GetByIdAsync(Guid id);

    }
}
