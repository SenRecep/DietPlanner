using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Report;
using DietPlanner.DTO.Response;

namespace DietPlanner.ClientShared.Services
{
    public class PatientHttpService : IPatientHttpService
    {
        private readonly HttpClient httpClient;

        public PatientHttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public Task<Response<UserDto>> GetPatient(Guid id)
            => httpClient.GetFromJsonAsync<Response<UserDto>>($"/api/patient/{id}");

        public Task<Response<List<ReportDto>>> GetPatientReports(Guid id)
             => httpClient.GetFromJsonAsync<Response<List<ReportDto>>>($"/api/patient/reports/{id}");
    }
}
