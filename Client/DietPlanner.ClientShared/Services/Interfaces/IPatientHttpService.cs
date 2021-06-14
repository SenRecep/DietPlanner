using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Report;
using DietPlanner.DTO.Response;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface IPatientHttpService
    {
        Task<Response<UserDto>> GetPatient(Guid id);
        Task<Response<List<ReportDto>>> GetPatientReports(Guid id);
    }
}
