using System.Threading.Tasks;

using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<Role> GetRoleByName(string name);
    }
}
