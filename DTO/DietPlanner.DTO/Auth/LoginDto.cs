
using DietPlanner.DTO.Interfaces;

namespace DietPlanner.DTO.Auth
{
    public record LoginDto : IDTO {
     
        public string IdentityNumber { get; set; }
        public string Password { get; set; }
    }
}
