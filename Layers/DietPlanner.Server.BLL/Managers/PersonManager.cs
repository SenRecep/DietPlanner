using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using DietPlanner.DTO.Auth;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.DAL.Interfaces;

namespace DietPlanner.Server.BLL.Managers
{
    public class PersonManager:IPersonService
    {
        private readonly IPersonRepository personRepository;
        private readonly IMapper mapper;

        public PersonManager(IPersonRepository personRepository,IMapper mapper)
        {
            this.personRepository = personRepository;
            this.mapper = mapper;
        }

        public async Task<UserDto> LoginAsync(LoginDto dto)
        {
            var found = await personRepository.LoginAsync(dto.IdentityNumber,dto.Password);
            //if (found is null)
            //    return null;
            return mapper.Map<UserDto>(found);
        }
    }
}
