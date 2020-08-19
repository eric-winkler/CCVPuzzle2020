using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzlePortal.Shared
{
    public class Puzzle
    {
        public static readonly Puzzle Seafood = new Puzzle { Name = nameof(Seafood), Id = new Guid("d2f541a3-9d9a-4e85-9d98-a0049fa60acf") };
        public static readonly Puzzle SimeonWheelock = new Puzzle { Name = nameof(SimeonWheelock), Id = new Guid("1ef4df75-ecbe-4d30-b06e-dce1c7678c61") };

        public static Dictionary<Guid, Puzzle> ById { get; } = new Dictionary<Guid, Puzzle>
        {
            { Seafood.Id, Seafood },
            { SimeonWheelock.Id, SimeonWheelock },
        };
        public static Guid[] Ids { get; } = ById.Keys.ToArray();


        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
