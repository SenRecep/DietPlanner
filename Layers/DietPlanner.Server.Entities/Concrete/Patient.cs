using System.Collections.Generic;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.Entities.Concrete
{
    public class Patient : Person, IPatient
    {
        public IEnumerable<Report> Reports { get; set; }
    }
}
