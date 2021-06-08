
using AutoMapper;

using DietPlanner.DTO.Diet;
using DietPlanner.DTO.Disease;
using DietPlanner.DTO.Patient;
using DietPlanner.DTO.Report;
using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.BLL.Mapping.AutoMapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Diet, DietDto>();
            CreateMap<Disease, DiseaseDto>();
            CreateMap<Patient, PatientDto>();
            CreateMap<ReportCreateDto, Report>();
        }
    }
}
