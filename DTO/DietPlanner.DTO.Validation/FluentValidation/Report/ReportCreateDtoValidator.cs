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

            RuleFor(x=>x.StartTime).LessThan(DateTime.Now).WithMessage("Lütfen ileri bir tarih seçiniz");

            RuleFor(x=>x.EndTime)
                .GreaterThan(DateTime.Now).WithMessage("Lütfen ileri bir tarih seçiniz")
                .LessThanOrEqualTo(x=>x.StartTime).WithMessage("Bitiş tarihi başlangıç tarihi ile eşit veya küçük olamaz");


        }
    }
}
