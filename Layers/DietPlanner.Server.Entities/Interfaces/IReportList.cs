using System.Collections.Generic;

using DietPlanner.Server.Entities.Concrete;

namespace DietPlanner.Server.Entities.Interfaces
{
    public interface IReportList
    {
        public IEnumerable<Report> Reports { get; set; }
    }
}
