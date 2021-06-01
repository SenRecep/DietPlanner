using System.Threading.Tasks;

using DietPlanner.DTO.Response;
using DietPlanner.DTO.Test;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface ITestService
    {
        public Task<Response<TestModel>> GetAsync();
    }
}
