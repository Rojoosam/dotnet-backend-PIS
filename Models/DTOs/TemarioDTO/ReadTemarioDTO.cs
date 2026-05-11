namespace SIADAL.Models.DTOs.TemarioDTO
{
    public class ReadTemarioDTO
    {
        public int id { get; set; }
        public int program_id { get; set; }
        public string name { get; set; } = null!;
        public bool has_pdf { get; set; }
    }
}
