using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateTeacherDTO
{
    public class CreateTeacherDTO
    {
        public int employee_number { get; set; }
        public string first_name { get; set; } = null!;

        public string last_name { get; set; } = null!;

        public bool? is_active { get; set; }

        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}
