using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Response;
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

        public async Task<Response<TestModel>> GetAsync()
        {
            httpClient.DefaultRequestHeaders.Add("userId",$"{Guid.NewGuid()}");
            Response<TestModel> response = await httpClient.GetFromJsonAsync<Response<TestModel>>("api/values");
            return response;
        }
    }
}
