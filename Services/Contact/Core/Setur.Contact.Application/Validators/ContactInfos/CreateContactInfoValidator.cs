using FluentValidation;
using Setur.Contact.Application.Features.ContactInfos.Create;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.Application.Validators.ContactInfos
{
    public class CreateContactInfoValidator: AbstractValidator<CreateContactInfoRequest>
    {
        public CreateContactInfoValidator()
        {
            RuleFor(x => x.PersonInfoId)
                .NotEmpty().WithMessage("Kişi ID'si boş olamaz.");

            RuleFor(x => x.InfoType)
                .IsInEnum().WithMessage("Geçersiz bilgi türü.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("İçerik boş olamaz.")
                .MaximumLength(256).WithMessage("İçerik en fazla 64 karakter olabilir.");

             RuleFor(x => x.Content)
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .When(x => x.InfoType == InfoType.Email);

             RuleFor(x => x.Content)
                .Matches(@"^\+90\d{10}$").WithMessage("Telefon numarası '+90' ile başlamalı ve 13 haneli olmalıdır.")
                .When(x => x.InfoType == InfoType.Phone);

             RuleFor(x => x.Content)
                .MinimumLength(2).WithMessage("Konum bilgisi en az 2 karakter olmalıdır.")
                .When(x => x.InfoType == InfoType.Location);
        }
    }
}
