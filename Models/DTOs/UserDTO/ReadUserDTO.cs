using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.ReadUserDTO
{
    public class ReadUserDTO
    {
        public int id { get; set; }
        public string email { get; set; } = string.Empty;
        public string first_name { get; set; } = null!;
        public string last_name { get; set; } = null!;
        public bool is_active { get; set; }
        public DateTime created_at { get; set; }
    }
}
