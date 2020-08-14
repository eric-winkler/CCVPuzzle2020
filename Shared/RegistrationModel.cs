using System.ComponentModel.DataAnnotations;

namespace PuzzlePortal.Shared
{
    public class RegistrationModel
    {
        [Required]
        [StringLength(255, ErrorMessage = "That's a little over the top isn't it?")]
        public string TeamName { get; set; }
    }
}
