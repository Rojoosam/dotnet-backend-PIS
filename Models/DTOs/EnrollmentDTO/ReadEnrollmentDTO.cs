namespace SIADAL.Models.DTOs.ReadEnrollmentDTO
{
    public class ReadEnrollmentDTO
    {
        public int student_id { get; set; }
        public string student_name { get; set; } = null!;
        public int class_id { get; set; }
        public string class_name { get; set; } = null!;
        public DateTime enrolled_at { get; set; }
    }
}
