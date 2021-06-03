using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.Server.BLL.StringInfos;
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Shared.StringInfo;

using Microsoft.EntityFrameworkCore;

namespace DietPlanner.Server.Seed
{
    public class UserRoleSeed
    {
        private readonly DietPlannerDbContext dbContext;

        public UserRoleSeed(DietPlannerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task RoleSeedAsync()
        {
            if (dbContext.Roles.Any())
                return;
            await dbContext.Roles.AddAsync(new Role() { Name=RoleInfo.Admin});
            await dbContext.Roles.AddAsync(new Role() { Name=RoleInfo.Patient});
            await dbContext.Roles.AddAsync(new Role() { Name=RoleInfo.Dietician});
            await dbContext.SaveChangesAsync();
        }

        public async Task UserSeedAsync()
        {
            if (dbContext.Admins.Any())
                return;
            var adminRole = await dbContext.Roles.FirstOrDefaultAsync(x=>x.Name.Equals(RoleInfo.Admin));
            await dbContext.Admins.AddAsync(new Admin()
            {
                Address = "Istanbul",
                CreateUserId = Guid.Parse(UserStringInfo.SystemUserId),
                CreatedTime=DateTime.Now,
                Email="admin@dietplanner.com",
                FirstName="Admin",
                LastName="1",
                IdentityNumber="11111111112",
                Password="Password12*",
                PhoneNumber="05319649002",
                Role= adminRole
            }) ;
            await dbContext.SaveChangesAsync();
        }

        public async Task SeedAsync()
        {
            await RoleSeedAsync();
            await UserSeedAsync();
        }
    }
}
