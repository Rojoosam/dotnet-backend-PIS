namespace SIADAL.Models.DTOs.ReadAssignmentDTO
{
    public class ReadAssignmentDTO
    {
        public int id { get; set; }
        public int class_id { get; set; }
        public string class_name { get; set; } = null!;
        public string name { get; set; } = null!;
        public DateTime? duedate { get; set; }
        public int points { get; set; }
        public string? details { get; set; }
    }
}
