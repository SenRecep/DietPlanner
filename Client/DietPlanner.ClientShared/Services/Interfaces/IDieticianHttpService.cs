using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Diet;
using DietPlanner.DTO.Disease;
using DietPlanner.DTO.FileModel;
using DietPlanner.DTO.Food;
using DietPlanner.DTO.Other;
using DietPlanner.DTO.Person;
using DietPlanner.DTO.Report;
using DietPlanner.DTO.Response;

namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface IDieticianHttpService
    {
        Task<Response<IEnumerable<UserDto>>> GetAllPatientAsync();
        Task<Response<Guid>> CreateReportAsync(ReportCreateDto reportCreateDto);
        Task<Response<TreatmentDto>> GetTreatmentAsync();
        Task<Response<UserDto>> CratePatientAsync(UserCreateDto userCreateDto);
        Task<Response<NoContent>> CreateDietAsync(DietCreateDto dietCreateDto);

        Task<Response<NoContent>> CreateFoodAsync(FoodCreateDto foodCreateDto);
        Task<Response<List<DietFoodCreateDto>>> GetAllFoodAsync();

        Task<Response<NoContent>> CreateDiseaseAsync(DiseaseCreateDto diseaseCreateDto);

        Task<Response<IEnumerable<DiseaseDto>>> GetAllDieasesAsync();

        Task<Response<Guid>> CreteFileModelAsync(FileModelCreateDto fileModelCreateDto);


    }
}
