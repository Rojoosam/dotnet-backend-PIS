using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateEducationalLevelDTO
{
    public class CreateEducationalLevelDTO
    {
        [Required]
        public string name { get; set; } = null!;
    }
}
