using SIADAL.Models;

namespace SIADAL.Helpers
{
    public class ActivityQueryObject
    {
        public string? name { get; set; } = null!;
        public decimal? grade { get; set; }
        public decimal? porcentage { get; set; }
        public string? status { get; set; } = null!;
    }
}
