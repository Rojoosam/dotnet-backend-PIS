namespace SIADAL.Models.DTOs.UpdateClassDTO
{
    public class UpdateClassDTO
    {
        public int? period_id { get; set; }
        public int? program_id { get; set; }
        public int? teacher_id { get; set; }
        public string? name { get; set; }
        public string? schedule_json { get; set; }
    }
}
