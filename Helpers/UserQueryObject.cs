using System.ComponentModel.DataAnnotations;

namespace SIADAL.Helpers
{
    public class UserQueryObject
    {
        public string? first_name { get; set; } = null!;
        public string? last_name { get; set; } = null!;
        public string? email { get; set; } = null!;
    }
}
