using System.Threading.Tasks;

using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Enums;
using DietPlanner.Server.Entities.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfPersonSingleQueryRepository : IPersonSingleQueryRepository
    {
        private readonly DietPlannerDbContext dbContext;

        public EfPersonSingleQueryRepository(DietPlannerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IPerson> GetPersonByIdentityNumber(PersonType personType, string identityNumber) => personType switch
        {
            PersonType.Dietician => await dbContext.Dieticians.FirstOrDefaultAsync(x => x.IdentityNumber.Equals(identityNumber)),
            PersonType.Patient => await dbContext.Patients.FirstOrDefaultAsync(x => x.IdentityNumber.Equals(identityNumber)),
            PersonType.Admin => await dbContext.Admins.FirstOrDefaultAsync(x => x.IdentityNumber.Equals(identityNumber)),
            _ => throw new System.NotImplementedException()
        };
    }
}
