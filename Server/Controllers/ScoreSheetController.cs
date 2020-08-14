using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PuzzlePortal.Server.Domain;
using PuzzlePortal.Shared;

namespace PuzzlePortal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreSheetController : ControllerBase
    {
        private readonly IQuizMaster _quizMaster;

        public ScoreSheetController(IQuizMaster quizMaster)
        {
            _quizMaster = quizMaster;
        }

        [HttpPost]
        [AuthorizeScoreSheet]
        public ScoreSheetModel Post(ScoreSheetModel scoreSheet)
        {
            if (scoreSheet.CompletedPuzzles?.Any() == true)
            {
                return scoreSheet;
            }
            else
            {
                return CreateNewScoreSheet(scoreSheet);
            }
        }

        private ScoreSheetModel CreateNewScoreSheet(ScoreSheetModel scoreSheet)
        {
            var newScoreSheet = _quizMaster.RegisterNewContestant(scoreSheet.Name);
            return newScoreSheet.ToModel();
        }
    }
}
