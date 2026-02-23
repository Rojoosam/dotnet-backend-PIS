namespace SIADAL.Models.DTOs.ClassDTO
{
    public class ReadClassRawDTO
    {
        public ulong id { get; set; }
        public ulong course_id { get; set; }
        public ulong teacher_id { get; set; }
        public ulong academic_term_id { get; set; }
        public string schedule { get; set; } = null!;
        public string room { get; set; } = null!;
        public string academic_term_name { get; set; } = null!;
        public string course_name { get; set; } = null!;
        public string teacher_name { get; set; } = null!;
    }
}
