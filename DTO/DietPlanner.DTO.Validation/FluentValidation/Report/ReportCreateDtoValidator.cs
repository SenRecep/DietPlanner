using System;

using DietPlanner.DTO.Report;

using FluentValidation;

namespace DietPlanner.DTO.Validation.FluentValidation.Report
{
    public class ReportCreateDtoValidator : AbstractValidator<ReportCreateDto>
    {
        public ReportCreateDtoValidator()
        {
            RuleFor(x => x.DietId).NotEqual(new Guid()).WithMessage("Diyet Seçiniz");
            RuleFor(x => x.DiseaseId).NotEqual(new Guid()).WithMessage("Hastalık Seçiniz");
            RuleFor(x => x.PatientId).NotEqual(new Guid()).WithMessage("Hasta Seçiniz");
        }
    }
}
