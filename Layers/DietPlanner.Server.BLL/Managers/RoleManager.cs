using System.Threading.Tasks;

using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.BLL.Managers
{
    public class RoleManager : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleManager(IRoleRepository roleRepository) => this.roleRepository = roleRepository;
        public async Task<Role> GetRoleByName(string name) => await roleRepository.GetRoleByName(name);
    }
}
