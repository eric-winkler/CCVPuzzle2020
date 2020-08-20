using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace PuzzlePortal.Server.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeCheatsAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

            if(!bool.TryParse(configuration["CheatsEnabled"], out var cheatsEnabled) || !cheatsEnabled)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
