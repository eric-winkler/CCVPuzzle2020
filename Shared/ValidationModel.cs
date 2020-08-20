using System.ComponentModel.DataAnnotations;

namespace PuzzlePortal.Shared
{
    public class ValidationModel
    {
        [Required]
        public string Code { get; set; }
    }
}
