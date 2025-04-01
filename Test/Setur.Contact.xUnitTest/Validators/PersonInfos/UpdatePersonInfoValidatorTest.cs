using Setur.Contact.Application.Features.PersonInfos.Update;
using Setur.Contact.Application.Validators.PersonInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Contact.xUnitTest.Validators.PersonInfos
{
    public class UpdatePersonInfoValidatorTest
    {
        private readonly UpdatePersonInfoValidator _validator;

        public UpdatePersonInfoValidatorTest()
        {
            _validator = new UpdatePersonInfoValidator();
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Empty()
        {
            var model = new UpdatePersonInfoRequest("", "Yılmaz", "Şirket");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Too_Short()
        {
            var model = new UpdatePersonInfoRequest("Al", "Yılmaz", "Şirket");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains("en az"));
        }

        [Fact]
        public void Should_Fail_When_Name_Has_Invalid_Characters()
        {
            var model = new UpdatePersonInfoRequest("Ali123", "Yılmaz", "Şirket");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains("harf"));
        }

        [Fact]
        public void Should_Fail_When_Surname_Is_Empty()
        {
            var model = new UpdatePersonInfoRequest("Ali", "", "Şirket");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Surname");
        }

        [Fact]
        public void Should_Fail_When_Surname_Is_Too_Short()
        {
            var model = new UpdatePersonInfoRequest("Ali", "A", "Şirket");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Surname" && e.ErrorMessage.Contains("en az"));
        }

        [Fact]
        public void Should_Fail_When_Surname_Has_Invalid_Characters()
        {
            var model = new UpdatePersonInfoRequest("Ali", "Yılmaz123", "Şirket");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Surname" && e.ErrorMessage.Contains("harf"));
        }

        [Fact]
        public void Should_Fail_When_Company_Is_Empty()
        {
            var model = new UpdatePersonInfoRequest("Ali", "Yılmaz", "");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Company");
        }

        [Fact]
        public void Should_Fail_When_Company_Has_Invalid_Characters()
        {
            var model = new UpdatePersonInfoRequest("Ali", "Yılmaz", "Şirket@");
            var result = _validator.Validate(model);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Company" && e.ErrorMessage.Contains("harf, sayı"));
        }

        [Fact]
        public void Should_Pass_When_All_Fields_Are_Valid()
        {
            var model = new UpdatePersonInfoRequest("Ali", "Yılmaz", "Setur A.Ş.");
            var result = _validator.Validate(model);

            Assert.True(result.IsValid);
        }
    }
}
