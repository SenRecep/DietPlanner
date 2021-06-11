
using System;

using DietPlanner.Server.Entities.Enums;
using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.Entities.Concrete
{
    public class FileModel : EntityBase, IFileModel
    {
        public FileType Type { get; set; }
        public string ContentType { get; set; }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }

        public Guid  ReportId { get; set; }
        public Report  Report { get; set; }
    }
}
