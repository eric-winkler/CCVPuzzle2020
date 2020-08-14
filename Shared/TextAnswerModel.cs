using System.ComponentModel.DataAnnotations;

namespace PuzzlePortal.Shared
{
    public class TextAnswerModel
    {
        [Required]
        [StringLength(255, ErrorMessage = "Answer is too long")]
        public string Answer { get; set; }
    }
}
