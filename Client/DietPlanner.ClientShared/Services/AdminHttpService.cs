using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Response;

namespace DietPlanner.ClientShared.Services
{
    public class AdminHttpService : IAdminHttpService
    {
        private readonly HttpClient httpClient;

        public AdminHttpService(HttpClient httpClient) => this.httpClient = httpClient;

        public async Task<Response<IEnumerable<UserDto>>> GetAllDietician() => await httpClient.GetFromJsonAsync<Response<IEnumerable<UserDto>>>("api/admin/GetAllDietician");
    }
}
