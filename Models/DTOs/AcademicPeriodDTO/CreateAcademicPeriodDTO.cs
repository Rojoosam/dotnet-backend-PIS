using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateAcademicPeriodDTO
{
    public class CreateAcademicPeriodDTO
    {
        [Required]
        public string name { get; set; } = null!;
        [Required]
        public DateOnly start_date { get; set; }
        [Required]
        public DateOnly end_date { get; set; }
        public bool? is_active { get; set; }
    }
}
