namespace SIADAL.Models;

public class Temario
{
    public int id { get; set; }
    public int program_id { get; set; }
    public string name { get; set; } = null!;
    public string? pdf_file_name { get; set; }

    public virtual Models.Program program { get; set; } = null!;
}
