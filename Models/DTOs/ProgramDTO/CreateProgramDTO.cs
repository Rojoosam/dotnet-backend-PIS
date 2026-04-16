using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateProgramDTO
{
    public class CreateProgramDTO
    {
        [Required]
        public int level_id { get; set; }
        [Required]
        public string name { get; set; } = null!;
    }
}
