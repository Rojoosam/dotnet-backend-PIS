namespace SIADAL.Models.DTOs.ReadStudentDTO
{
    public class ReadStudentDTO
    {
        public int id { get; set; }
        public string enrollment_number { get; set; } = null!;
        public DateOnly birth_date { get; set; }

        // User info
        public int user_id { get; set; }
        public string email { get; set; } = string.Empty;
        public string first_name { get; set; } = null!;
        public string last_name { get; set; } = null!;
        public bool is_active { get; set; }

        // Program info
        public int program_id { get; set; }
        public string program_name { get; set; } = null!;
    }
}
