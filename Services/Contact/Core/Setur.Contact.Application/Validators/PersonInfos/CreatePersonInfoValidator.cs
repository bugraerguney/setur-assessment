using FluentValidation;
using Setur.Contact.Application.Features.PersonInfos.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Validators.PersonInfos
{
    public class CreatePersonInfoValidator : AbstractValidator<CreatePersonInfoRequest>
    {
        public CreatePersonInfoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad alanı boş olamaz.")
                .MinimumLength(3).WithMessage("Ad en az 3 karakter olmalıdır.")
                .MaximumLength(64).WithMessage("Ad en fazla 64 karakter olabilir.")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]*$").WithMessage("Ad yalnızca harflerden oluşmalıdır.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz.")
                .MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalıdır.")
                .MaximumLength(64).WithMessage("Soyad en fazla 64 karakter olabilir.")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]*$").WithMessage("Soyad yalnızca harflerden oluşmalıdır.");

            RuleFor(x => x.Company)
                .NotEmpty().WithMessage("Şirket adı boş olamaz.")
                .MinimumLength(2).WithMessage("Şirket adı en az 2 karakter olmalıdır.")
                .MaximumLength(64).WithMessage("Şirket adı en fazla 64 karakter olabilir.")
                .Matches(@"^[a-zA-Z0-9ğüşıöçĞÜŞİÖÇ\s\.\-&]*$").WithMessage("Şirket adı yalnızca harf, sayı ve - . & karakterlerini içerebilir.");
        }
    }
}
