using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateUserDTO
{
    public class CreateUserDTO
    {
        [Required]
        public string first_name { get; set; } = null!;
        [Required]
        public string last_name { get; set; } = null!;
        [Required]
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
        public bool? is_active { get; set; }
    }
}
