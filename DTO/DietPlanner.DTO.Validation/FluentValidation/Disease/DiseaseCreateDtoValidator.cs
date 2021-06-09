
using DietPlanner.DTO.Disease;

using FluentValidation;

namespace DietPlanner.DTO.Validation.FluentValidation.Disease
{
    public class DiseaseCreateDtoValidator : AbstractValidator<DiseaseCreateDto>
    {
        public DiseaseCreateDtoValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad alanı boş geçilemez")
            .MaximumLength(30).WithMessage("Ad alanı en fazla 30 karakter olabilir");
        }
    }
}
