using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.DAL.Interfaces
{
    public interface IPatientRepository
    {
        Task<(bool State, IEnumerable<string> Errors)> CheckReportDateByUserIdAsync(Guid userId,DateTimeRange range);
    }
}
