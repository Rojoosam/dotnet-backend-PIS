using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.UpdateAcademic_termDTO
{
    public class UpdateAcademic_termDTO
    {

        public string? name { get; set; } = null!;

        public DateOnly? start_date { get; set; }

        public DateOnly? end_date { get; set; }

        public bool? is_active { get; set; }
    }
}
