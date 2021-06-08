
using System.Linq;

using DietPlanner.DTO.Diet;

using FluentValidation;

namespace DietPlanner.DTO.Validation.FluentValidation.Diet
{
    public class DietCreateDtoValidator : AbstractValidator<DietCreateDto>
    {
        public DietCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad alanı boş geçilemez")
                .MaximumLength(30).WithMessage("Ad alanı en fazla 30 karakter olabilir");
            RuleFor(x => x.Description)
              .NotEmpty().WithMessage("Açıklama alanı boş geçilemez")
              .MaximumLength(500).WithMessage("Açıklama alanı en fazla 30 karakter olabilir");
        }
    }
}
