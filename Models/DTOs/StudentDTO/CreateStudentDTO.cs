using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateStudentDTO
{
    public class CreateStudentDTO
    {
        // User fields
        [Required]
        public string first_name { get; set; } = null!;
        [Required]
        public string last_name { get; set; } = null!;
        [Required]
        public string email { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
        public bool? is_active { get; set; }

        // Student specific fields
        [Required]
        public int program_id { get; set; }
        [Required]
        public string enrollment_number { get; set; } = null!;
        [Required]
        public DateOnly birth_date { get; set; }
    }
}
