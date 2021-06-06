using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Server.Entities.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfPersonRepository : IPersonRepository
    {
        private readonly DietPlannerDbContext context;

        public EfPersonRepository(DietPlannerDbContext context)
        {
            this.context = context;
        }
        public async Task<IPerson> LoginAsync(string identityNumber, string password)
        {
            IPerson found;
            found = await context.Patients.Include(x => x.Role).FirstOrDefaultAsync(person => person.IdentityNumber.Equals(identityNumber));
            if (found is not null)
                return found;

            found = await context.Dieticians.Include(x => x.Role).FirstOrDefaultAsync(person => person.IdentityNumber.Equals(identityNumber));
            if (found is not null)
                return found;

            found = await context.Admins.Include(x => x.Role).FirstOrDefaultAsync(person => person.IdentityNumber.Equals(identityNumber));
            if (found is not null)
                if (BCrypt.Net.BCrypt.Verify(password, found.Password))
                    return found;

            return null;
        }
    }
}
