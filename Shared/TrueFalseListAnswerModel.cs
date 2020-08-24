using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PuzzlePortal.Shared
{
    public class TrueFalseListAnswerModel
    {
        [Required]
        public IDictionary<string, bool> Answer { get; set; }

        public TrueFalseListAnswerModel()
        {
            Answer = new Dictionary<string, bool>();
        }
    }
}
