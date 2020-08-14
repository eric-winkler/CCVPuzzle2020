using System;
using System.Linq;

namespace PuzzlePortal.Shared
{
	public class PuzzleList
    {
		public static Guid[] PuzzleIds => new[] { // TODO: add actual quizes
			new Guid("c4e7444e-5ce2-49d5-a37a-8814cefa4aa3"),
			new Guid("18e2f10c-ab94-4a75-be9c-ec0a46525608"),
			new Guid("9a54205e-b556-4ec4-9a5b-6f4b2126091b"),
			new Guid("39b9dcc2-0e2e-4f3c-83f2-1b4a14ea2617"),
			new Guid("7acb258a-cc91-455a-b0a3-cde72f04fd05"),
			new Guid("21e8da3d-470b-4120-bc15-532b1521e428"),
		};

	}

	public class ScoreSheetModel
    {
		public string Name { get; set; }
		public DateTimeOffset StartingTimestamp { get; set; }
		public Guid[] CompletedPuzzles { get; set; }
		public Guid CurrentPuzzle { get; set; }
		public string Signature { get; set; }
	}
}
