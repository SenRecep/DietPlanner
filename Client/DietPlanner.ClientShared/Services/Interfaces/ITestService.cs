using System.Threading.Tasks;

using DietPlanner.DTO.Test;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface ITestService
    {
        public Task<TestModel> GetAsync();
    }
}
