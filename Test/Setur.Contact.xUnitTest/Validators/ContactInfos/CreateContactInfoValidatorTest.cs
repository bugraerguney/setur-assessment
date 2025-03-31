using Setur.Contact.Application.Features.ContactInfos.Create;
using Setur.Contact.Application.Validators.ContactInfos;
using Setur.Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.Validators.ContactInfos
{
    public class CreateContactInfoValidatorTest
    {
        private readonly CreateContactInfoValidator _validator;

        public CreateContactInfoValidatorTest()
        {
            _validator = new CreateContactInfoValidator();
        }

        [Fact]
        public void Should_Fail_When_PersonInfoId_Is_Empty()
        {
            var model = new CreateContactInfoRequest(Guid.Empty, InfoType.Email, "valid@example.com");

            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "PersonInfoId");
        }

        [Fact]
        public void Should_Fail_When_Content_Is_Empty()
        {
            var model = new CreateContactInfoRequest(Guid.NewGuid(), InfoType.Phone, "");

            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Content");
        }

        [Fact]
        public void Should_Fail_When_Email_Is_Invalid()
        {
            var model = new CreateContactInfoRequest(Guid.NewGuid(), InfoType.Email, "not-an-email");

            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("e-posta"));
        }

        [Fact]
        public void Should_Fail_When_Phone_Is_Invalid()
        {
            var model = new CreateContactInfoRequest(Guid.NewGuid(), InfoType.Phone, "05551234567");

            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Telefon"));
        }

        [Fact]
        public void Should_Fail_When_Location_Is_Too_Short()
        {
            var model = new CreateContactInfoRequest(Guid.NewGuid(), InfoType.Location, "A");

            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Contains("Konum"));
        }

        [Fact]
        public void Should_Pass_When_Email_Is_Valid()
        {
            var model = new CreateContactInfoRequest(Guid.NewGuid(), InfoType.Email, "valid@example.com");

            var result = _validator.Validate(model);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Pass_When_Phone_Is_Valid()
        {
            var model = new CreateContactInfoRequest(Guid.NewGuid(), InfoType.Phone, "+905551234567");

            var result = _validator.Validate(model);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Should_Pass_When_Location_Is_Valid()
        {
            var model = new CreateContactInfoRequest(Guid.NewGuid(), InfoType.Location, "Istanbul");

            var result = _validator.Validate(model);

            Assert.True(result.IsValid);
        }
    }
}
