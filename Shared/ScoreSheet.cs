using System;

namespace PuzzlePortal.Shared
{
	public class ScoreSheetModel
    {
		public string Name { get; set; }
		public DateTimeOffset StartingTimestamp { get; set; }
		public DateTimeOffset? FinishedTimestamp { get; set; }
		public Guid[] CompletedPuzzles { get; set; }
		public Guid CurrentPuzzle { get; set; }
		public string Signature { get; set; }
	}
}
