﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGenericRepository<T> : IGenericRepository<T>
       where T : class, IEntityBase, new()

    {
        private readonly DbContext dbContext;
        private readonly DbSet<T> table;

        private readonly IDbContextTransaction dbContextTransaction;

        public EfGenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            table = dbContext.Set<T>();
            dbContextTransaction = dbContext.Database.BeginTransaction();
        }

        #region GetAll
        public async Task<IEnumerable<T>> GetAllAsync() => await table.Where(x => !x.IsDeleted).ToListAsync();

        public async Task<IEnumerable<T>> GetAllWithDeletedAsync() => await table.ToListAsync();

        #endregion

        #region Get
        public async Task<T> GetByIdAsync(Guid id) => await table.FindAsync(id);
        #endregion

        #region CUD

        public async Task<T> AddAsync(T entity)
        {
            entity.CreatedTime = DateTime.Now;
            await table.AddAsync(entity);
            return entity;
        }

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
            bool commitState;
            try
            {
                await SaveChangesAsync();
                commitState = true;
            }
            catch (Exception)
            {
                commitState = false;
            }

            if (commitState && state)
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