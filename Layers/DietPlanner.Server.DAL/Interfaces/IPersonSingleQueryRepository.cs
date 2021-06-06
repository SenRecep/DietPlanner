using System.Threading.Tasks;

using DietPlanner.Server.Entities.Enums;
using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.DAL.Interfaces
{
    public interface IPersonSingleQueryRepository
    {
        Task<IPerson> GetPersonByIdentityNumber(PersonType personType, string identityNumber);
    }
}
