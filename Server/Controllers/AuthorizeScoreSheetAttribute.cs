using System;
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
            var scoreSheet = ExtractScoreSheet(context);
            if (scoreSheet == null || !quizMaster.IsAuthentic(new ScoreSheet(scoreSheet)))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            context.ActionArguments["scoreSheet"] = scoreSheet;
            await next();
        }

        private static ScoreSheetModel ExtractScoreSheet(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("ScoreSheet"))
                return null;

            try
            {
                var scoreSheetHeader = context.HttpContext.Request.Headers["ScoreSheet"];
                return System.Text.Json.JsonSerializer.Deserialize<ScoreSheetModel>(scoreSheetHeader);
            }
            catch(System.Text.Json.JsonException)
            {
                return null;
            }
        }
    }
}
