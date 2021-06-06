using System;
using System.Linq;
using System.Threading.Tasks;

using DietPlanner.Server.BLL.StringInfos;
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Shared.DesignPatterns.FluentFactory;
using DietPlanner.Shared.StringInfo;

using Microsoft.AspNetCore.Identity;
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
            await dbContext.Roles.AddAsync(new() { Name = RoleInfo.Admin });
            await dbContext.Roles.AddAsync(new() { Name = RoleInfo.Patient });
            await dbContext.Roles.AddAsync(new() { Name = RoleInfo.Dietician });
            await dbContext.SaveChangesAsync();
        }

        public async Task UserSeedAsync()
        {
            if (dbContext.Admins.Any())
                return;
            Role adminRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Name.Equals(RoleInfo.Admin));

            await dbContext.Admins.AddAsync(FluentFactory<Admin>.Init()
                .GiveAValue(x => x.Address, "Istanbul")
                .GiveAValue(x => x.CreateUserId, Guid.Parse(UserStringInfo.SystemUserId))
                .GiveAValue(x => x.CreatedTime, DateTime.Now)
                .GiveAValue(x => x.Email, "admin@dietplanner.com")
                .GiveAValue(x => x.FirstName, "Admin")
                .GiveAValue(x => x.LastName, "1")
                .GiveAValue(x => x.IdentityNumber, "11111111112")
                .GiveAValue(x => x.Password, "Password12*")
                .GiveAValue(x => x.PhoneNumber, "05319649002")
                .GiveAValue(x => x.Role, adminRole)
                .Use(admin => admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password))
                .Take());
            await dbContext.SaveChangesAsync();
        }

        public async Task SeedAsync()
        {
            await RoleSeedAsync();
            await UserSeedAsync();
        }
    }
}
