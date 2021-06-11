using System;

using DietPlanner.DTO.FileModel;

using FluentValidation;

namespace DietPlanner.DTO.Validation.FluentValidation.FileModel
{
    public class FileModelCreateValidator : AbstractValidator<FileModelCreateDto>
    {
        public FileModelCreateValidator()
        {
            RuleFor(x => x.ReportId).NotEqual(new Guid()).WithMessage("Rapor Seçiniz");
            RuleFor(x => x.SectionOrder).IsInEnum();
            RuleFor(x => x.FileType).IsInEnum();
        }
    }
}
