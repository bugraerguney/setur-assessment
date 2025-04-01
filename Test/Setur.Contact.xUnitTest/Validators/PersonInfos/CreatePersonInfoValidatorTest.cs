using Setur.Contact.Application.Features.PersonInfos.Create;
using Setur.Contact.Application.Validators.PersonInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.Validators.PersonInfos
{
    public class CreatePersonInfoValidatorTest
    {
        private readonly CreatePersonInfoValidator _validator;

        public CreatePersonInfoValidatorTest()
        {
            _validator = new CreatePersonInfoValidator();
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Empty()
        {
            var model = new CreatePersonInfoRequest("", "Yılmaz", "Şirket");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Too_Short()
        {
            var model = new CreatePersonInfoRequest("Al", "Yılmaz", "Şirket");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains("en az"));
        }

        [Fact]
        public void Should_Fail_When_Name_Has_Invalid_Characters()
        {
            var model = new CreatePersonInfoRequest("Ali123", "Yılmaz", "Şirket");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains("harf"));
        }

        [Fact]
        public void Should_Fail_When_Surname_Is_Empty()
        {
            var model = new CreatePersonInfoRequest("Ali", "", "Şirket");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Surname");
        }

        [Fact]
        public void Should_Fail_When_Surname_Is_Too_Short()
        {
            var model = new CreatePersonInfoRequest("Ali", "A", "Şirket");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Surname" && e.ErrorMessage.Contains("en az"));
        }

        [Fact]
        public void Should_Fail_When_Surname_Has_Invalid_Characters()
        {
            var model = new CreatePersonInfoRequest("Ali", "Yılmaz123", "Şirket");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Surname" && e.ErrorMessage.Contains("harf"));
        }

        [Fact]
        public void Should_Fail_When_Company_Is_Empty()
        {
            var model = new CreatePersonInfoRequest("Ali", "Yılmaz", "");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Company");
        }

        [Fact]
        public void Should_Fail_When_Company_Has_Invalid_Characters()
        {
            var model = new CreatePersonInfoRequest("Ali", "Yılmaz", "Şirket@");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Company" && e.ErrorMessage.Contains("harf, sayı"));
        }

        [Fact]
        public void Should_Pass_When_All_Fields_Are_Valid()
        {
            var model = new CreatePersonInfoRequest("Ali", "Yılmaz", "Setur A.Ş.");
            var result = _validator.Validate(model);

            Assert.True(result.IsValid);
        }
    }
}
