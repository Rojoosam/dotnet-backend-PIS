namespace SIADAL.Helpers
{
    public class Academic_termQueryObject
    {
        public string? name { get; set; } = null!;

        public DateOnly? start_date { get; set; }

        public DateOnly? end_date { get; set; }

        public bool? is_active { get; set; }

    }
}
