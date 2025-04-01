using Setur.Report.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ServiceResultsTest
{
    public class ServiceResultTest
    {
        [Fact]
        public void Success_Should_Set_Status_And_IsSuccess()
        {
            var result = ServiceResult.Success(HttpStatusCode.Created);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFail);
            Assert.Equal(HttpStatusCode.Created, result.Status);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public void Fail_With_List_Should_Set_Errors_And_IsFail()
        {
            var errors = new List<string> { "Hata1", "Hata2" };
            var result = ServiceResult.Fail(errors, HttpStatusCode.NotFound);

            Assert.True(result.IsFail);
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.Status);
            Assert.Equal(2, result.ErrorMessage?.Count);
        }

        [Fact]
        public void Fail_With_String_Should_Set_Single_Error_And_Default_Status()
        {
            var result = ServiceResult.Fail("Tek hata");

            Assert.True(result.IsFail);
            Assert.Single(result.ErrorMessage!);
            Assert.Equal("Tek hata", result.ErrorMessage[0]);
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
        }
    }
}
