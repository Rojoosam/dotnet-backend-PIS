namespace SIADAL.Models.DTOs.UpdateAssignmentDTO
{
    public class UpdateAssignmentDTO
    {
        public int? class_id { get; set; }
        public string? name { get; set; }
        public DateTime? duedate { get; set; }
        public int? points { get; set; }
        public string? details { get; set; }
    }
}
