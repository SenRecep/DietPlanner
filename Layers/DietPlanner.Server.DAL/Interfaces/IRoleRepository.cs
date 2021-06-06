using System.Threading.Tasks;

using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.DAL.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByName(string name);
    }
}
