using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.ReadAcademic_termDTO
{
    public class ReadAcademic_termDTO
    {
        [Required]
        public ulong id { get; set; }

        public string name { get; set; } = null!;

        public DateOnly start_date { get; set; }

        public DateOnly end_date { get; set; }

        public bool? is_active { get; set; }
    }
}
