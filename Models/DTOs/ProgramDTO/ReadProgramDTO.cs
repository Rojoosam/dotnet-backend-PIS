namespace SIADAL.Models.DTOs.ReadProgramDTO
{
    public class ReadProgramDTO
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public int level_id { get; set; }
        public string level_name { get; set; } = null!;

        public string? temario { get; set; }
    }
}