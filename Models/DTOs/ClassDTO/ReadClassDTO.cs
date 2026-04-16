namespace SIADAL.Models.DTOs.ReadClassDTO
{
    public class ReadClassDTO
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public string? schedule_json { get; set; }

        // Period info
        public int period_id { get; set; }
        public string period_name { get; set; } = null!;

        // Program info
        public int program_id { get; set; }
        public string program_name { get; set; } = null!;

        // Teacher info
        public int teacher_id { get; set; }
        public string teacher_name { get; set; } = null!;
    }
}
