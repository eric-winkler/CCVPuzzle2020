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
    [AuthorizeScoreSheet(currentPuzzle: Puzzle.GuidStrings.CuminsLore)]
    public class CuminsLoreController : QuestionControllerBase
    {
        private static readonly string[] RealStories = new[]
        {
            "We have a taxidermied three headed duckling for sale at a store",
            "Someone posted a gimp cage for sale on WIWO",
            "Someone once loaned on 50kgs of frozen prawns",
            "Someone loaned their fish tank, with fish in it",
            "Usher nearly bought a bass guitar from the Freo store",
            "We had a 21ct diamond worth $800k for sale on Webshop"
        };
        private static readonly string[] FakeStories = new[]
        {
            "None of these are true",
            "A store kept 50kgs of frozen prawns in a fish tank",
            "Taxidermied animals and gimp cages are often bought together",
            "Usher sold us a 21ct diamond worth $800k"
        };
        private readonly IQuizMaster _quizMaster;

        public CuminsLoreController(IQuizMaster quizMaster)
        {
            _quizMaster = quizMaster;
        }

        [HttpPost]
        public ActionResult<ScoreSheetModel> Answer(TrueFalseListAnswerModel model)
        {
            if(!model.Answer.Any()
                || !model.Answer.Where(a => a.Value).Select(a => a.Key).All(m => RealStories.Contains(m))
                || !model.Answer.Where(a => !a.Value).Select(a => a.Key).All(m => FakeStories.Contains(m)))
            {
                return BadRequest();
            }

            return _quizMaster.CompletePuzzle(ScoreSheet).ToModel();
        }

        [HttpGet]
        public IEnumerable<string> Question()
        {
            return RealStories.Concat(FakeStories)
                .OrderBy(i => Guid.NewGuid())
                .Take(5)
                .ToArray();
        }
    }
}
