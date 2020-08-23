using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PuzzlePortal.Server.Domain;
using PuzzlePortal.Shared;

namespace PuzzlePortal.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}")]
    [AuthorizeScoreSheet(currentPuzzle: Puzzle.GuidStrings.SimeonWheelock)]
    public class SimeonWheelockController : QuestionControllerBase
    {
        private readonly IQuizMaster _quizMaster;

        private const string Keyword = "BLACKSMITH";
        private const int MinPrice = 12;
        private const int MaxPrice = 999;

        private readonly Random _random = new Random();

        public SimeonWheelockController(IQuizMaster quizMaster)
        {
            _quizMaster = quizMaster;
        }

        [HttpPost]
        public ActionResult<ScoreSheetModel> Answer(SimeonWheelockModel model)
        {
            if(EncodePrice(model.Answer) != model.PriceCode)
            {
                return BadRequest();
            }

            return _quizMaster.CompletePuzzle(ScoreSheet).ToModel();
        }

        [HttpGet]
        public SimeonWheelockModel Question()
        {
            return new SimeonWheelockModel
            {
                PriceCode = EncodePrice(_random.Next(MinPrice * 100, MaxPrice * 100) / 100m)
            };
        }

        private static string EncodePrice(decimal price)
        {
            var keyword = Keyword.ToArray();
            var priceChars = price.ToString("C")
            .Where(c => c == '.' || (c >= '0' && c <= '9'))
            .Select(c => c == '.' ? '.' : keyword[(c - '0' + 9) % 10])
            .ToArray();

            return new string(priceChars);
        }
    }
}
