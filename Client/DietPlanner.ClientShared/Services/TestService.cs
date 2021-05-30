using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Test;

namespace DietPlanner.ClientShared.Services
{
    public class TestService : ITestService
    {
        private readonly HttpClient httpClient;

        public TestService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<TestModel> GetAsync()
        {
            TestModel data = await httpClient.GetFromJsonAsync<TestModel>("api/values");
            return data;
        }
    }
}
