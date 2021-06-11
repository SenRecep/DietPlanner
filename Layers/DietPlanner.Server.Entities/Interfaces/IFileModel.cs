
using System;

using DietPlanner.Server.Entities.Concrete;
using DietPlanner.Server.Entities.Enums;

namespace DietPlanner.Server.Entities.Interfaces
{
    public interface IFileModel:IEntityBase
    {
         FileType Type { get; set; }
         string ContentType { get; set; }
         string OriginalFileName { get; set; }
         string FileName { get; set; }

         Guid ReportId { get; set; }
         Report Report { get; set; }
    }
}
