using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGenericQueryRepository<T> : IGenericQueryRepository<T>
       where T : class, IEntityBase, new()
    {
        private readonly DbContext dbContext;
        private readonly DbSet<T> table;


        public EfGenericQueryRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            table = dbContext.Set<T>();
        }

        #region GetAll
        public async Task<IEnumerable<T>> GetAllAsync() => await table.Where(x => !x.IsDeleted).ToListAsync();

        public async Task<IEnumerable<T>> GetAllWithDeletedAsync() => await table.ToListAsync();
        #endregion

        #region Dispose

        public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();
        #endregion
    }
}
