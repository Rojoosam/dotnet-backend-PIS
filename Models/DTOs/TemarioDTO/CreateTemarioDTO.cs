using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.TemarioDTO
{
    public class CreateTemarioDTO
    {
        [Required]
        public int program_id { get; set; }
        [Required]
        public string name { get; set; } = null!;
    }
}
