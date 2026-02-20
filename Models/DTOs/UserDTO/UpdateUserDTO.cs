using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.UpdateUserDTO
{
    public class UpdateUserDTO
    {
        public string? first_name { get; set; } = null!;

        public string? last_name { get; set; } = null!;

        public bool? is_active { get; set; }

        public string? email { get; set; } = null!;
        public string? password { get; set; } = null!;
    }
}
