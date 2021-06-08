using System.Threading.Tasks;

using DietPlanner.DTO.Report;
using DietPlanner.DTO.Response;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface IDieticianHttpService
    {
        Task<Response<NoContent>> CreateReportAsync(ReportCreateDto reportCreateDto);
    }
}
