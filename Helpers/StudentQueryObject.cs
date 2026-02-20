namespace SIADAL.Helpers
{
    public class StudentQueryObject
    {
        public string? first_name { get; set; } = null!;
        public string? last_name { get; set; } = null!;
        public bool? is_active { get; set; }
        public string? email { get; set; } = null!;
        public int? enrollment_number { get; set; }
        public DateOnly? birth_date { get; set; }
    }
}
