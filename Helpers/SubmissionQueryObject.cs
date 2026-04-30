namespace SIADAL.Helpers
{
    public class SubmissionQueryObject
    {
        public int? assignment_id { get; set; }
        public int? student_id { get; set; }
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 10;
        public bool Paginated { get; set; } = true;
    }
}
