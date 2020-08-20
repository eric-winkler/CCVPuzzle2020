using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PuzzlePortal.Server.Domain;
using PuzzlePortal.Shared;

namespace PuzzlePortal.Server.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    [AuthorizeCheats]
    public class CheatsController : ControllerBase
    {
        private readonly IQuizMaster _quizMaster;

        public CheatsController(IQuizMaster quizMaster)
        {
            _quizMaster = quizMaster;
        }

        [HttpPost]
        public ScoreSheetModel CompleteAllPuzzles(ScoreSheetModel model)
        {
            var scoreSheet = new ScoreSheet(model);
            while (Puzzle.Ids.Except(scoreSheet.CompletedPuzzles).Any())
            {
                scoreSheet = _quizMaster.CompletePuzzle(scoreSheet);
            };

            return scoreSheet.ToModel();
        }
    }
}
