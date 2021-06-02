
using AutoMapper;

using DietPlanner.DTO.Auth;
using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.BLL.Mapping.AutoMapper
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<IPerson, UserDto>();
        }
    }
}
