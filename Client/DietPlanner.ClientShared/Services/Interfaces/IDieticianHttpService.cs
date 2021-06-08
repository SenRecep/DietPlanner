using System.Collections.Generic;
using System.Threading.Tasks;

using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Diet;
using DietPlanner.DTO.Other;
using DietPlanner.DTO.Person;
using DietPlanner.DTO.Report;
using DietPlanner.DTO.Response;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface IDieticianHttpService
    {
        Task<Response<IEnumerable<UserDto>>> GetAllPatientAsync();
        Task<Response<NoContent>> CreateReportAsync(ReportCreateDto reportCreateDto);
        Task<Response<TreatmentDto>> GetTreatmentAsync();
        Task<Response<UserDto>> CratePatientAsync(UserCreateDto userCreateDto);
        Task<Response<NoContent>> CreateDietAsync(DietCreateDto dietCreateDto);
    }
}
