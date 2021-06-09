
using DietPlanner.DTO.Food;

using FluentValidation;

namespace DietPlanner.DTO.Validation.FluentValidation.Food
{
    public class FoodCreateDtoValidator : AbstractValidator<FoodCreateDto>
    {
        public FoodCreateDtoValidator()
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
