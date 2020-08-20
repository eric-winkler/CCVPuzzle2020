using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PuzzlePortal.Server.Domain;
using PuzzlePortal.Shared;

namespace PuzzlePortal.Server.Controllers
{
    [ApiController]
    [Route("[controller]/{action}")]
    public class SimeonWheelockController : ControllerBase
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
        [AuthorizeScoreSheet]
        public ScoreSheetModel Answer(SimeonWheelockModel model)
        {
            // TODO:
            //if(EncodePrice(model.Answer) == model.PriceCode)

            throw new NotImplementedException();
            
        }

        [HttpGet]
        [AuthorizeScoreSheet]
        public SimeonWheelockModel Question()
        {
            // TODO: is this the current puzzle?

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
