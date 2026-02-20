using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateStudentDTO
{
    public class CreateStudentDTO
    {
        // User fields
        public string first_name { get; set; } = null!;
        public string last_name { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
        public bool? is_active { get; set; }

        // Student specific fields
        public int Enrollment_number { get; set; }

        public DateOnly birth_date { get; set; }
    }
}
