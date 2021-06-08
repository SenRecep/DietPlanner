using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Report;
using DietPlanner.DTO.Response;

namespace DietPlanner.ClientShared.Services
{
    public class DieticianHttpService : IDieticianHttpService
    {
        private readonly HttpClient httpClient;

        public DieticianHttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Response<NoContent>> CreateReportAsync(ReportCreateDto reportCreateDto)
        {
            var response = await httpClient.PostAsJsonAsync("api/report", reportCreateDto);
            return await response.Content.ReadFromJsonAsync<Response<NoContent>>();
        }
    }
}
