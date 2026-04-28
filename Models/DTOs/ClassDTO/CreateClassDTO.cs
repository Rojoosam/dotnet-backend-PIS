using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateClassDTO
{
    public class CreateClassDTO
    {
        [Required]
        public int period_id { get; set; }
        [Required]
        public int program_id { get; set; }
        [Required]
        public int teacher_id { get; set; }
        [Required]
        public string name { get; set; } = null!;
        public string? schedule_json { get; set; }
    }
}
