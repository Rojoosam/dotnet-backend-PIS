using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.ReadStudentDTO
{
    public class ReadStudentDTO
    {
        [Required]
        public ulong id { get; set; }
        public int enrollment_number { get; set; }

        public DateOnly birth_date { get; set; }
        
        // User
        public ulong userId { get; set; }

        public string email { get; set; } = string.Empty;
        public string first_name { get; set; } = null!;

        public string last_name { get; set; } = null!;
        public bool is_active { get; set; }
    }
}
