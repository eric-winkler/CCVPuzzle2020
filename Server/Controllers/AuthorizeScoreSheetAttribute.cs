using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PuzzlePortal.Server.Domain;
using PuzzlePortal.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace PuzzlePortal.Server.Controllers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeScoreSheetAttribute : Attribute, IAsyncActionFilter
    {
        private readonly Guid? _currentPuzzle;

        public AuthorizeScoreSheetAttribute()
        {
        }

        public AuthorizeScoreSheetAttribute(string currentPuzzle)
        {
            _currentPuzzle = new Guid(currentPuzzle);
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var quizMaster = context.HttpContext.RequestServices.GetService<IQuizMaster>();
            var scoreSheet = ExtractScoreSheet(context);
            if (scoreSheet == null
                || (_currentPuzzle != null && _currentPuzzle != scoreSheet.CurrentPuzzle)
                || !quizMaster.IsAuthentic(new ScoreSheet(scoreSheet)))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (context.Controller is QuestionControllerBase questionController)
            {
                questionController.ScoreSheet = new ScoreSheet(scoreSheet);
            }

            await next();
        }

        private static ScoreSheetModel ExtractScoreSheet(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("ScoreSheet"))
                return null;

            try
            {
                var scoreSheetHeader = context.HttpContext.Request.Headers["ScoreSheet"];
                return JsonSerializer.Deserialize<ScoreSheetModel>(scoreSheetHeader);
            }
            catch(JsonException)
            {
                return null;
            }
        }
    }
}
