namespace SIADAL.Models.DTOs.ReadSubmissionDTO
{
    public class ReadSubmissionDTO
    {
        public int id { get; set; }
        public int assignment_id { get; set; }
        public string assignment_name { get; set; } = null!;
        public int student_id { get; set; }
        public string student_name { get; set; } = null!;
        public DateTime submitted_at { get; set; }
        public int? grade { get; set; }
        public string? file_url { get; set; }
    }
}
