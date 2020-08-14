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

    public class ScoreSheet
    {
		public string Name { get; private set; }
		public DateTimeOffset StartingTimestamp { get; private set; }
		public Guid[] CompletedPuzzles { get; set; }
		public Guid CurrentPuzzle { get; set; }
		public string Signature { get; private set; }
		public ScoreSheet SigningTarget => new ScoreSheet(this);

		public ScoreSheet(string name, Guid firstPuzzle)
		{
			Name = name;
			StartingTimestamp = DateTimeOffset.Now;
			CurrentPuzzle = firstPuzzle;
			CompletedPuzzles = new Guid[0];
		}

		private ScoreSheet(ScoreSheet otherScoreSheet)
		{
			Name = otherScoreSheet.Name;
			StartingTimestamp = otherScoreSheet.StartingTimestamp;
			CompletedPuzzles = otherScoreSheet.CompletedPuzzles;
			CurrentPuzzle = otherScoreSheet.CurrentPuzzle;
		}

		public ScoreSheet Complete(Guid puzzleId)
		{
			if (CurrentPuzzle != puzzleId)
				throw new InvalidOperationException("Can only complete the current puzzle");

			var newScoreSheet = new ScoreSheet(this);
			newScoreSheet.CompletedPuzzles = newScoreSheet.CompletedPuzzles.Concat(new[] { puzzleId }).ToArray();
			return newScoreSheet;
		}

		public ScoreSheet ChangeToPuzzle(Guid puzzleId)
		{
			if (CompletedPuzzles.Contains(puzzleId))
				throw new InvalidOperationException("Can't change to an already completed puzzle");

			var newScoreSheet = new ScoreSheet(this);
			newScoreSheet.CurrentPuzzle = puzzleId;
			return newScoreSheet;
		}

		public ScoreSheet Sign(string signature)
        {
			var newScoreSheet = new ScoreSheet(this);
			newScoreSheet.Signature = signature;
			return newScoreSheet;
		}
	}
}
