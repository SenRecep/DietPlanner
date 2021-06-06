using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using DietPlanner.DTO.Auth;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Enums;
using DietPlanner.Server.Entities.Interfaces;
using DietPlanner.Shared.ExtensionMethods;

namespace DietPlanner.Server.BLL.Managers
{
    public class PersonManager:IPersonService
    {
        private readonly IPersonRepository personRepository;
        private readonly IPersonSingleQueryRepository personSingleQueryRepository;
        private readonly IMapper mapper;

        public PersonManager(IPersonRepository personRepository,IPersonSingleQueryRepository personSingleQueryRepository,IMapper mapper)
        {
            this.personRepository = personRepository;
            this.personSingleQueryRepository = personSingleQueryRepository;
            this.mapper = mapper;
        }

        public async Task<UserDto> GetPersonByIdentityNumber(PersonType personType, string identityNumber)
        {
            IPerson person = await personSingleQueryRepository.GetPersonByIdentityNumber(personType,identityNumber);
            if (person.IsNotNull() && person.IsDeleted)
                person = null;
            return mapper.Map<UserDto>(person);
        }

        public async Task<UserDto> LoginAsync(LoginDto dto)
        {
            var found = await personRepository.LoginAsync(dto.IdentityNumber,dto.Password);
            return mapper.Map<UserDto>(found);
        }
    }
}
