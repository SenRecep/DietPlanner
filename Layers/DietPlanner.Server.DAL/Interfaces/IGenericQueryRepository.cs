using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.DAL.Interfaces
{
    public  interface IGenericQueryRepository<T> : IAsyncDisposable
        where T : class, IEntityBase, new()
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetAllWithDeletedAsync();

    }
}
