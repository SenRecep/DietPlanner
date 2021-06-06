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
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş geçilemez")
                .MaximumLength(40).WithMessage("Ad 40 karakterden fazla olamaz");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş geçilemez")
                .MaximumLength(40).WithMessage("Soyad 40 karakterden fazla olamaz");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres boş geçilemez")
                .MaximumLength(200).WithMessage("Adres 200 karakterden fazla olamaz");

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email boş geçilemez")
                .EmailAddress().WithMessage("Email formatı hatalı")
                .MaximumLength(50).WithMessage("Soyad 50 karakterden fazla olamaz");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş geçilemez")
                .Matches("^(05)[0-9][0-9][1-9]([0-9]){6}$").WithMessage("Telefon numarası formatı hatalı");

            RuleFor(x => x.IdentityNumber)
              .NotEmpty().WithMessage("Kimlik numarası boş geçilemez")
              .Matches("^[1-9]{1}[0-9]{9}[02468]{1}$").WithMessage("Kimlik numarası formatı hatalı");
        }
    }
}
