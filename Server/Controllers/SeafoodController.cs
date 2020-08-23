using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.IO;
using PuzzlePortal.Shared;
using PuzzlePortal.Server.Domain;

namespace PuzzlePortal.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/{action}")]
    [AuthorizeScoreSheet(currentPuzzle: Puzzle.GuidStrings.Seafood)]
    public class SeafoodController : QuestionControllerBase
    {
        private static readonly string[] Sealife = LoadSealife();
        private readonly IQuizMaster _quizMaster;

        private static string[] LoadSealife()
        {
            using var stream = typeof(SeafoodController).GetTypeInfo().Assembly.GetManifestResourceStream("PuzzlePortal.Server.Controllers.Sealife.txt");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        public SeafoodController(IQuizMaster quizMaster)
        {
            _quizMaster = quizMaster;
        }

        [HttpPost]
        public ActionResult<ScoreSheetModel> Answer(TextAnswerModel model)
        {
            if(model.Answer.ToLower() != "calamari")
            {
                return BadRequest();
            }

            return _quizMaster.CompletePuzzle(ScoreSheet).ToModel();
        }

        [HttpGet]
        public IEnumerable<string> Question()
        {
            var rng = new Random();
            return Enumerable.Range(1, 25).Select(i => Sealife[rng.Next(Sealife.Length)])
                .ToArray();
        }
    }
}
