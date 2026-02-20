using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.UpdateStudentDTO
{
    public class UpdateStudentDTO
    {

        public string? first_name { get; set; } = null!;

        public string? last_name { get; set; } = null!;

        public bool? is_active { get; set; }

        public string? email { get; set; } = null!;
        public string? password { get; set; } = null!;
        public int? Enrollment_number { get; set; }

        public DateOnly? birth_date { get; set; }
    }
}
