using System.Threading.Tasks;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.DAL.Interfaces
{
    public interface IPersonRepository
    {
        Task<IPerson> LoginAsync(string identityNumber, string password);
    }
}
