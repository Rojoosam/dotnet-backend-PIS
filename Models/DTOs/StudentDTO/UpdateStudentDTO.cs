namespace SIADAL.Models.DTOs.UpdateStudentDTO
{
    public class UpdateStudentDTO
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public bool? is_active { get; set; }
        public int? program_id { get; set; }
        public string? enrollment_number { get; set; }
        public DateOnly? birth_date { get; set; }
    }
}
