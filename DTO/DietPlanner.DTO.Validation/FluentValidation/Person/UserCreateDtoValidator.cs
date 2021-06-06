using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.DTO.Person;

using FluentValidation;

namespace DietPlanner.DTO.Validation.FluentValidation.Person
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad boş geçilemez");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad boş geçilemez");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Adres boş geçilemez");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş geçilemez");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş geçilemez")
                .Matches("^(05)[0-9][0-9][1-9]([0-9]){6}$").WithMessage("Telefon numarası formatı hatalı");

            RuleFor(x => x.IdentityNumber)
              .NotEmpty().WithMessage("Kimlik numarası boş geçilemez")
              .Matches("^[1-9]{1}[0-9]{9}[02468]{1}$").WithMessage("Kimlik numarası formatı hatalı");
        }
    }
}
