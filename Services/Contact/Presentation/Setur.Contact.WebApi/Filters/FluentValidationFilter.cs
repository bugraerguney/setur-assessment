using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Setur.Contact.Application;

namespace Setur.Contact.WebApi.Filters
{
    public class FluentValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (!context.ModelState.IsValid)
            {

                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                var result = ServiceResult.Fail(errors);

                context.Result = new BadRequestObjectResult(result);
                return;
            }

            await next();


        }
    }
}
