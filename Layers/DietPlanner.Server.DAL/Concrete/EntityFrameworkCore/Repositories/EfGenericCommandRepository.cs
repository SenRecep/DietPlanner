using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGenericCommandRepository<T> : IGenericCommandRepository<T>
       where T : class, IEntityBase, new()
    {
        private readonly DbContext dbContext;
        private readonly DbSet<T> table;

        private readonly IDbContextTransaction dbContextTransaction;

        public EfGenericCommandRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            table = dbContext.Set<T>();
            dbContextTransaction = dbContext.Database.BeginTransaction();
        }

        #region CRUD

        public async Task<T> AddAsync(T entity)
        {
            entity.CreatedTime = DateTime.Now;
            await table.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities) => await table.AddRangeAsync(entities);

        public async Task UpdateAsync(T entity)
        {
            entity.UpdateTime = DateTime.Now;
            await Task.FromResult(table.Update(entity));
        }

        public async Task RemoveAsync(T entity, bool hardDelete = false)
        {
            if (hardDelete)
                await Task.FromResult(table.Remove(entity));
            else
            {
                entity.IsDeleted = true;
                await UpdateAsync(entity);
            }
        }



        #endregion

        #region Commit
        public async Task<bool> Commit(bool state = true)
        {
            if (!state)
            {
                await dbContextTransaction.RollbackAsync();
                await DisposeAsync();
                return state;
            }

            bool commitState;
            try
            {
                await SaveChangesAsync();
                commitState = true;
            }
            catch 
            {
                commitState = false;
            }

            if (commitState )
                await dbContextTransaction.CommitAsync();
            else
                await dbContextTransaction.RollbackAsync();

            await DisposeAsync();
            return commitState;
        }
        #endregion

        #region Dispose

        public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();
        #endregion

        #region Save

        public async Task<int> SaveChangesAsync() => await dbContext.SaveChangesAsync();



        #endregion

    }
}
