
using AutoMapper;

using DietPlanner.DTO.Auth;
using DietPlanner.DTO.Person;
using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.BLL.Mapping.AutoMapper
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<IPerson, UserDto>();
            CreateMap<UserCreateDto, Dietician>();
            CreateMap<UserCreateDto, Patient>();
        }
    }
}
