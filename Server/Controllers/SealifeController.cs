using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.IO;

namespace PuzzlePortal.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SealifeController : ControllerBase
    {
        private static readonly string[] Sealife = LoadSealife();

        private static string[] LoadSealife()
        {
            using var stream = typeof(SealifeController).GetTypeInfo().Assembly.GetManifestResourceStream("PuzzlePortal.Server.Controllers.Sealife.txt");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        private readonly ILogger<SealifeController> logger;

        public SealifeController(ILogger<SealifeController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(i => Sealife[rng.Next(Sealife.Length)])
                .ToArray();
        }
    }
}
