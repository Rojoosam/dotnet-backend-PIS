using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.ReadTeacherDTO
{
    public class ReadTeacherDTO
    {
        public ulong id { get; set; }
        public ulong user_id { get; set; }
        public int employee_number { get; set; }
        public string first_name { get; set; } = null!;

        public string last_name { get; set; } = null!;

        public bool is_active { get; set; }

        public string email { get; set; } = null!;
    }
}
