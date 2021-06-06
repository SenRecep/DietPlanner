using System.Collections.Generic;

namespace DietPlanner.ClientShared.Models
{
    public class NavLinks
    {
        public List<NavbarModel> Dietician { get; set; }
        public List<NavbarModel> Admin { get; set; }
        public List<NavbarModel> Patient { get; set; }
    }
}
