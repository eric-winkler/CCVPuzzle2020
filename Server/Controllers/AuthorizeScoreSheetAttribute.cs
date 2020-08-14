using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PuzzlePortal.Server.Domain;
using PuzzlePortal.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace PuzzlePortal.Server.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeScoreSheetAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var quizMaster = context.HttpContext.RequestServices.GetService<IQuizMaster>();
            if (!(context.ActionArguments["scoreSheet"] is ScoreSheetModel scoreSheet) ||
                (scoreSheet.CompletedPuzzles?.Any() == true && !quizMaster.IsAuthentic(new ScoreSheet(scoreSheet))))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
