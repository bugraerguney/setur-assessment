using Microsoft.AspNetCore.Diagnostics;
using Setur.Contact.Application;
using System.Net;

namespace Setur.Contact.WebApi.ExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            var errorDto = ServiceResult.Fail(exception.Message, HttpStatusCode.InternalServerError);


            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(errorDto, cancellationToken: cancellationToken);

            return true;
        }
    }
}
