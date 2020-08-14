using PuzzlePortal.Shared;
using System;
using System.Linq;

namespace PuzzlePortal.Server.Domain
{
    public interface IQuizMaster
    {
        ScoreSheet RegisterNewContestant(string name);
        ScoreSheet CompletePuzzle(ScoreSheet scoreSheet, Guid puzzleId);
        bool IsAuthentic(ScoreSheet scoreSheet);

    }

    public class Trebek : IQuizMaster
    {
        public string SigningKey = "todo";
        private static readonly Random Random = new Random(); // can be predicted

        public ScoreSheet RegisterNewContestant(string name)
        {
            var firstQuestion = PickOneAtRandom(PuzzleList.PuzzleIds);
            var scoreSheet = new ScoreSheet(name, firstQuestion);
            return Sign(scoreSheet);
        }

        public ScoreSheet CompletePuzzle(ScoreSheet scoreSheet, Guid puzzleId)
        {
            if (!IsAuthentic(scoreSheet))
                throw new InvalidOperationException("Score sheet is not authentic");

            if (scoreSheet.CurrentPuzzle != puzzleId)
                throw new InvalidOperationException("Can only complete the current puzzle");

            scoreSheet = scoreSheet.Complete(puzzleId);
            var remainingPuzzles = PuzzleList.PuzzleIds.Except(scoreSheet.CompletedPuzzles).ToArray();
            if (remainingPuzzles.Any())
                scoreSheet = scoreSheet.ChangeToPuzzle(PickOneAtRandom(remainingPuzzles));

            return Sign(scoreSheet);
        }

        public bool IsAuthentic(ScoreSheet scoreSheet)
        {
            var expectedSignature = scoreSheet.ComputeSignature(SigningKey);
            return scoreSheet.Signature == expectedSignature;
        }

        private ScoreSheet Sign(ScoreSheet scoreSheet)
        {
            var signature = scoreSheet.ComputeSignature(SigningKey);
            return scoreSheet.Sign(signature);
        }

        private T PickOneAtRandom<T>(T[] set)
        {
            return set[Random.Next(0, set.Length)];
        }
    }
}
