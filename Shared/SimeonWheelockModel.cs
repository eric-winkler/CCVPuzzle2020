using System.ComponentModel.DataAnnotations;

namespace PuzzlePortal.Shared
{
    public class SimeonWheelockModel
    {
        [Required]
        [Range(0.01, 2000)]
        public decimal Answer { get; set; }
        public string PriceCode { get; set; }
    }
}
