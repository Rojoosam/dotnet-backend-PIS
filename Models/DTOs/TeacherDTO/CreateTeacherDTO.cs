using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateTeacherDTO
{
    public class CreateTeacherDTO
    {
        [Required]
        public string employee_number { get; set; } = null!;
        [Required]
        public string first_name { get; set; } = null!;
        [Required]
        public string last_name { get; set; } = null!;
        [Required]
        public string email { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
        public bool? is_active { get; set; }
    }
}
