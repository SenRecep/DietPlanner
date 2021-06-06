using System.Threading.Tasks;

using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Concrete;

using Microsoft.EntityFrameworkCore;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfRoleRepository : IRoleRepository
    {
        private readonly DietPlannerDbContext dbContext;

        public EfRoleRepository(DietPlannerDbContext dbContext) => this.dbContext = dbContext;
        public async Task<Role> GetRoleByName(string name) => await dbContext.Roles.FirstOrDefaultAsync(x => x.Name.Equals(name));
    }
}
