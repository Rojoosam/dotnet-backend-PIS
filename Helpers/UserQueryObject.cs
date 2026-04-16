namespace SIADAL.Helpers
{
    public class UserQueryObject
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public bool? is_active { get; set; }
        public int page { get; set; } = 1;
        public int per_page { get; set; } = 10;
    }
}
