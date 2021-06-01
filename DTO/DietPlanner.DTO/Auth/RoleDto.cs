
using DietPlanner.DTO.Interfaces;

namespace DietPlanner.DTO.Auth
{
    public record RoleDto: IDTO
    {
        public string Name { get; set; }
    }
}
