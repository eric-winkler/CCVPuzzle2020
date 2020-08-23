using PuzzlePortal.Shared;
using System;
using System.Linq;

namespace PuzzlePortal.Server.Domain
{
	public class ScoreSheet
	{
		public string Name { get; private set; }
		public DateTimeOffset StartingTimestamp { get; private set; }
		public DateTimeOffset? FinishedTimestamp { get; private set; }
		public Guid[] CompletedPuzzles { get; set; }
		public Guid CurrentPuzzle { get; set; }
		public string Signature { get; private set; }

		public ScoreSheet(string name, Guid firstPuzzle)
		{
			Name = name;
			StartingTimestamp = DateTimeOffset.Now;
			CurrentPuzzle = firstPuzzle;
			CompletedPuzzles = new Guid[0];
		}

		public ScoreSheet(ScoreSheetModel model)
		{
			Name = model.Name;
			StartingTimestamp = model.StartingTimestamp;
			FinishedTimestamp = model.FinishedTimestamp;
			CompletedPuzzles = model.CompletedPuzzles;
			CurrentPuzzle = model.CurrentPuzzle;
			Signature = model.Signature;
		}

		public ScoreSheet(ScoreSheet otherScoreSheet)
		{
			Name = otherScoreSheet.Name;
			StartingTimestamp = otherScoreSheet.StartingTimestamp;
			FinishedTimestamp = otherScoreSheet.FinishedTimestamp;
			CompletedPuzzles = otherScoreSheet.CompletedPuzzles;
			CurrentPuzzle = otherScoreSheet.CurrentPuzzle;
		}

		internal ScoreSheet Complete(Guid puzzleId)
		{
			if (CurrentPuzzle != puzzleId)
				throw new InvalidOperationException("Can only complete the current puzzle");

			var newScoreSheet = new ScoreSheet(this);
			newScoreSheet.CompletedPuzzles = newScoreSheet.CompletedPuzzles.Concat(new[] { puzzleId }).Distinct().ToArray();
			return newScoreSheet;
		}

		internal ScoreSheet Finish()
        {
			var newScoreSheet = new ScoreSheet(this);
			newScoreSheet.FinishedTimestamp = DateTimeOffset.Now;
			return newScoreSheet;
		}

		internal ScoreSheet ChangeToPuzzle(Guid puzzleId)
		{
			if (CompletedPuzzles.Contains(puzzleId))
				throw new InvalidOperationException("Can't change to an already completed puzzle");

			var newScoreSheet = new ScoreSheet(this);
			newScoreSheet.CurrentPuzzle = puzzleId;
			return newScoreSheet;
		}

		internal ScoreSheet Sign(string signature)
		{
			var newScoreSheet = new ScoreSheet(this);
			newScoreSheet.Signature = signature;
			return newScoreSheet;
		}

		internal ScoreSheetModel ToModel()
		{
			return new ScoreSheetModel
			{
				Name = Name,
				CompletedPuzzles = CompletedPuzzles,
				CurrentPuzzle = CurrentPuzzle,
				Signature = Signature,
				StartingTimestamp = StartingTimestamp,
				FinishedTimestamp = FinishedTimestamp
			};
		}
	}
}
