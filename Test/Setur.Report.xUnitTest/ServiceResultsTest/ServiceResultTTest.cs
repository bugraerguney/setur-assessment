using Setur.Report.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Setur.Report.xUnitTest.ServiceResultsTest
{
    public class ServiceResultTTest
    {
        [Fact]
        public void Success_Should_Set_Data_And_Status()
        {
            // Arrange
            var data = "Success Data";

            // Act
            var result = ServiceResult<string>.Success(data);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFail);
            Assert.Equal(HttpStatusCode.OK, result.Status);
            Assert.Equal(data, result.Data);
            Assert.Null(result.ErrorMessage);
            Assert.Null(result.UrlAsCreated);
        }

        [Fact]
        public void SuccessAsCreated_Should_Set_Created_Status_And_Url()
        {
            // Arrange
            var data = "Created Data";
            var url = "/api/data/1";

            // Act
            var result = ServiceResult<string>.SuccessAsCreated(data, url);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFail);
            Assert.Equal(HttpStatusCode.Created, result.Status);
            Assert.Equal(data, result.Data);
            Assert.Equal(url, result.UrlAsCreated);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public void Fail_With_String_Should_Set_ErrorMessage_And_Status()
        {
            // Arrange
            var message = "An error occurred.";

            // Act
            var result = ServiceResult<string>.Fail(message);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFail);
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
            Assert.Single(result.ErrorMessage);
            Assert.Equal(message, result.ErrorMessage![0]);
            Assert.Null(result.Data);
        }

        [Fact]
        public void Fail_With_List_Should_Set_ErrorMessage_List()
        {
            // Arrange
            var errors = new List<string> { "Error1", "Error2" };

            // Act
            var result = ServiceResult<string>.Fail(errors);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFail);
            Assert.Equal(errors, result.ErrorMessage);
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
            Assert.Null(result.Data);
        }
    }
}
