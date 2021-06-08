using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Other;
using DietPlanner.DTO.Person;
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

        public async Task<Response<UserDto>> CratePatientAsync(UserCreateDto userCreateDto)
        {
            var response = await httpClient.PostAsJsonAsync("api/dietician/CreatePatient", userCreateDto);
            return await response.Content.ReadFromJsonAsync<Response<UserDto>>();
        }

        public async Task<Response<NoContent>> CreateReportAsync(ReportCreateDto reportCreateDto)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/report", reportCreateDto);
            return await response.Content.ReadFromJsonAsync<Response<NoContent>>();
        }

        public async Task<Response<IEnumerable<UserDto>>> GetAllPatientAsync()
             => await httpClient.GetFromJsonAsync<Response<IEnumerable<UserDto>>>("api/dietician/getallpatient");

        public async Task<Response<TreatmentDto>> GetTreatmentAsync() =>
            await httpClient.GetFromJsonAsync<Response<TreatmentDto>>("api/treatment");
    }
}
