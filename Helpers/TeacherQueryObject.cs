namespace SIADAL.Helpers
{
    public class TeacherQueryObject
    {
        public int? employee_number { get; set; }
        public string? first_name { get; set; } = null!;

        public string? last_name { get; set; } = null!;

        public bool? is_active { get; set; }

        public string? email { get; set; } = null!;
    }
}
