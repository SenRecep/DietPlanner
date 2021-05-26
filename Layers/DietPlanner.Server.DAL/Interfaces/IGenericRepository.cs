using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.DAL.Interfaces
{
    public interface IGenericRepository<T> : IUnitOfWork
        where T : class, IEntityBase, new()
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetAllWithDeletedAsync();
        public Task<IEnumerable<T>> GetAllByUserIdAsync(Guid id);

        public Task<T> GetByIdAsync(Guid id);
        public Task<T> GetByUserIdAsync(Guid id);

        public Task<T> AddAsync(T entity);
        public Task UpdateAsync(T entity);

        public Task RemoveAsync(T entity, bool hardDelete = false);

        public Task<int> SaveChangesAsync();
    }
}
