using System.Threading.Tasks;

using DietPlanner.DTO.Response;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface ITestService
    {
        public Task<Response<string>> GetAsync();
    }
}
