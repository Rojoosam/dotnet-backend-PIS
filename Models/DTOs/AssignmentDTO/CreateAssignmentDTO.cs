using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateAssignmentDTO
{
    public class CreateAssignmentDTO
    {
        [Required]
        public int class_id { get; set; }
        [Required]
        public string name { get; set; } = null!;
        public DateTime? duedate { get; set; }
        public int points { get; set; } = 0;
        public string? details { get; set; }
    }
}
