using Microsoft.AspNetCore.Mvc;
using PuzzlePortal.Server.Domain;

namespace PuzzlePortal.Server.Controllers
{
    public class QuestionControllerBase : ControllerBase
    {
        public ScoreSheet ScoreSheet { get; set; }
    }
}
