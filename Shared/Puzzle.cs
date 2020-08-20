using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzlePortal.Shared
{
    public class Puzzle
    {
        public static readonly Puzzle Seafood = new Puzzle { Name = nameof(Seafood), Id = new Guid("d2f541a3-9d9a-4e85-9d98-a0049fa60acf") };
        public static readonly Puzzle SimeonWheelock = new Puzzle { Name = nameof(SimeonWheelock), Id = new Guid("1ef4df75-ecbe-4d30-b06e-dce1c7678c61") };
        
        // TODO:
        public static readonly Puzzle Skewers = new Puzzle { Name = nameof(Skewers), Id = new Guid("8cfcdc16-fc45-472b-8a9f-a10b995e4248") };
        public static readonly Puzzle CuminsLore = new Puzzle { Name = nameof(CuminsLore), Id = new Guid("fc4acc00-04c0-4fb5-8ea8-129dbe24ca1f") };
        public static readonly Puzzle Codenames = new Puzzle { Name = nameof(Codenames), Id = new Guid("8ddcc5ef-f2b7-4d88-ab02-38e311597cf5") };
        // launchdates?
        // glcodes vs gldescription?


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
