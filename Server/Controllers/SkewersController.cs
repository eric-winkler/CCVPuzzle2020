using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PuzzlePortal.Shared;
using PuzzlePortal.Server.Domain;

namespace PuzzlePortal.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}")]
    [AuthorizeScoreSheet(currentPuzzle: Puzzle.GuidStrings.Skewers)]
    public class SkewersController : QuestionControllerBase
    {
        private static readonly string[] RealSkus = new[] { "Dragon", "Golden Cane", "Rattle", "Handcuffs", "Batteries", "Astroturf", "Cut Padlock", "Broom Head", "Kitchen Sink" };
        private static readonly string[] FakeSkus = new[] { "USB Cable", "Lightsabre", "Armour", "Saucepan", "Time Machine" };
        private readonly IQuizMaster _quizMaster;

        public SkewersController(IQuizMaster quizMaster)
        {
            _quizMaster = quizMaster;
        }

        [HttpPost]
        public ActionResult<ScoreSheetModel> Answer(TrueFalseListAnswerModel model)
        {
            if(!model.Answer.Any()
                || !model.Answer.Where(a => a.Value).Select(a => a.Key).All(m => RealSkus.Contains(m))
                || !model.Answer.Where(a => !a.Value).Select(a => a.Key).All(m => FakeSkus.Contains(m)))
            {
                return BadRequest();
            }

            return _quizMaster.CompletePuzzle(ScoreSheet).ToModel();
        }

        [HttpGet]
        public IEnumerable<string> Question()
        {
            return RealSkus.Concat(FakeSkus)
                .OrderBy(i => Guid.NewGuid())
                .Take(6)
                .ToArray();
        }
    }
}
