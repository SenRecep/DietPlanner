using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Concrete;

using Microsoft.EntityFrameworkCore;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Repositories
{
    public class EfPatientRepository : IPatientRepository
    {
        private readonly DietPlannerDbContext dbContext;

        public EfPatientRepository(DietPlannerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<(bool State, IEnumerable<string> Errors)> CheckReportDateByUserIdAsync(Guid userId, DateTimeRange range)
        {
            var errors = new List<string>();
            var user = await dbContext.Patients.Include(x => x.Reports).FirstOrDefaultAsync(x => x.Id == userId);
            if (user is not null && user.Reports is not null)
            {
                foreach (var item in user.Reports)
                    if ((item.StartTime <= range.Start && item.EndTime > range.Start) ||
                        (item.StartTime <= range.End && item.EndTime > range.End))
                        errors.Add($"Diyet başlangıç tarihi: {item.StartTime} bitiş tarihi: {item.EndTime}");
                return (errors.Count==0, errors);
            }
            return (true, new List<string>());
        }
    }
}
