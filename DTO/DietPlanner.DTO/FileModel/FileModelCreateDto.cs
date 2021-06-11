using System;

namespace DietPlanner.DTO.FileModel
{
    public class FileModelCreateDto
    {
        public Guid ReportId { get; set; }
        public FileType FileType { get; set; }
        public SectionOrder SectionOrder { get; set; }
    }
}
