using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.ReadClassDTO
{
    public class ReadClassDTO
    {
        public ulong id { get; set; }
        public ulong course_id { get; set; }
        public ulong teacher_id { get; set; }
        public ulong academic_term_id { get; set; }

        public List<ScheduleDTO> schedule { get; set; } = new();
        public string room { get; set; } = null!;

        public string course_name { get; set; } = null!;
        public string teacher_name { get; set; } = null!;
        public string academic_term_name { get; set; } = null!;
    }
}
