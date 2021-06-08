
using DietPlanner.DTO.Auth;

using FluentValidation;

namespace DietPlanner.DTO.Validation.FluentValidation.Auth
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.IdentityNumber)
               .NotEmpty().WithMessage("Kimlik numarası boş geçilemez")
               .Matches("^[1-9]{1}[0-9]{9}[02468]{1}$").WithMessage("Kimlik numarası formatı hatalı");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola boş geçilemez")
                .Matches("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,35}$").WithMessage("Parolanız yeterince güçlü değil");
        }
    }
}
