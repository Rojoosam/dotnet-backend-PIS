using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.UpdateClassDTO
{
    public class UpdateClassDTO
    {

        public ulong? course_id { get; set; }

        public ulong? teacher_id { get; set; }
        public ulong? academic_term_id { get; set; }

        public List<ScheduleDTO>? schedule { get; set; } = new();

        public string? room { get; set; } = null!;
    }
}
