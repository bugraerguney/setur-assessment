using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Setur.Contact.Application.Contracts.Persistance;
using Setur.Contact.Application;

namespace Setur.Contact.WebApi.Filters
{
    public class NotFoundFilter<T, TId>(IGenericRepository<T, TId> genericRepository) : Attribute, IAsyncActionFilter where T : class where TId : struct
    {


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var idValue = context.ActionArguments.Values.FirstOrDefault();



            if (idValue is not TId id)
            {
                await next();

                return;
            }



            if (await genericRepository.AnyAsync(id))
            {
                await next();
                return;
            }

            var entityName = typeof(T).Name;

            var actionName = context.ActionDescriptor.RouteValues["action"];

            var result = ServiceResult.Fail($"Data bulunamamıştır.({entityName})({actionName})");
            context.Result = new NotFoundObjectResult(result);


        }
    }
}
