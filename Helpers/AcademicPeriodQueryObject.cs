namespace SIADAL.Helpers
{
    public class AcademicPeriodQueryObject
    {
        public string? name { get; set; }
        public DateOnly? start_date { get; set; }
        public DateOnly? end_date { get; set; }
        public bool? is_active { get; set; }
    }
}
