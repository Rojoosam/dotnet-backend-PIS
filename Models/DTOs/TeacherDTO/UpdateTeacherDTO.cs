namespace SIADAL.Models.DTOs.UpdateTeacherDTO
{
    public class UpdateTeacherDTO
    {
        public string? employee_number { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public bool? is_active { get; set; }
    }
}
