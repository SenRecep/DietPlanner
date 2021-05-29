using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGenericSingleQueryRepository<T> : IGenericSingleQueryRepository<T>
       where T : class, IEntityBase, new()
    {
        private readonly DbContext dbContext;
        private readonly DbSet<T> table;


        public EfGenericSingleQueryRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            table = dbContext.Set<T>();
        }

        #region Get
        public async Task<T> GetByIdAsync(Guid id) => await table.FindAsync(id);
        #endregion

        #region Dispose

        public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();
        #endregion
    }
}
