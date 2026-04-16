namespace SIADAL.Models.DTOs.ReadAcademicPeriodDTO
{
    public class ReadAcademicPeriodDTO
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public DateOnly start_date { get; set; }
        public DateOnly end_date { get; set; }
        public bool is_active { get; set; }
    }
}
