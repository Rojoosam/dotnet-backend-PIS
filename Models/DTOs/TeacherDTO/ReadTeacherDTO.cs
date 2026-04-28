namespace SIADAL.Models.DTOs.ReadTeacherDTO
{
    public class ReadTeacherDTO
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string employee_number { get; set; } = null!;
        public string first_name { get; set; } = null!;
        public string last_name { get; set; } = null!;
        public bool is_active { get; set; }
        public string email { get; set; } = null!;
    }
}
