using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using DietPlanner.ClientShared.Services.Interfaces;
using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Diet;
using DietPlanner.DTO.Disease;
using DietPlanner.DTO.FileModel;
using DietPlanner.DTO.Food;
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

        public async Task<Response<NoContent>> CreateDietAsync(DietCreateDto dietCreateDto)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/diet", dietCreateDto);
            return await response.Content.ReadFromJsonAsync<Response<NoContent>>();
        }

        public async Task<Response<NoContent>> CreateDiseaseAsync(DiseaseCreateDto diseaseCreateDto)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/disease", diseaseCreateDto);
            return await response.Content.ReadFromJsonAsync<Response<NoContent>>();
        }

        public async Task<Response<NoContent>> CreateFoodAsync(FoodCreateDto foodCreateDto)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/food", foodCreateDto);
            return await response.Content.ReadFromJsonAsync<Response<NoContent>>();
        }

        public async Task<Response<Guid>> CreateReportAsync(ReportCreateDto reportCreateDto)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/report", reportCreateDto);
            return await response.Content.ReadFromJsonAsync<Response<Guid>>();
        }

        public async Task<Response<Guid>> CreteFileModelAsync(FileModelCreateDto fileModelCreateDto)
        {
            var response = await httpClient.PostAsJsonAsync("api/filemodel", fileModelCreateDto);
            return await response.Content.ReadFromJsonAsync<Response<Guid>>();
        }

        public async Task<Response<IEnumerable<DiseaseDto>>> GetAllDieasesAsync()
        => await httpClient.GetFromJsonAsync<Response<IEnumerable<DiseaseDto>>>("api/disease");
        public async Task<Response<List<DietFoodCreateDto>>> GetAllFoodAsync() 
            => await httpClient.GetFromJsonAsync<Response<List<DietFoodCreateDto>>>("api/food");

        public async Task<Response<IEnumerable<UserDto>>> GetAllPatientAsync()
             => await httpClient.GetFromJsonAsync<Response<IEnumerable<UserDto>>>("api/dietician/getallpatient");

        public async Task<Response<TreatmentDto>> GetTreatmentAsync() =>
            await httpClient.GetFromJsonAsync<Response<TreatmentDto>>("api/treatment");
    }
}
