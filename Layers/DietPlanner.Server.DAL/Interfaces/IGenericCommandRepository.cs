using System.Threading.Tasks;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.DAL.Interfaces
{
    public interface IGenericCommandRepository<T> : IUnitOfWork
        where T : class, IEntityBase, new()
    {
        public Task<T> AddAsync(T entity);
        public Task UpdateAsync(T entity);

        public Task RemoveAsync(T entity, bool hardDelete = false);

        public Task<int> SaveChangesAsync();
    }
}
