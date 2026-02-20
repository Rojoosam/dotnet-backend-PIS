using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.ReadUserDTO
{
    public class ReadUserDTO
    {
        [Required]
        public ulong Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string First_name { get; set; } = null!;

        public string Last_name { get; set; } = null!;

    }
}
