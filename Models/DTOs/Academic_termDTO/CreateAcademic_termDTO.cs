using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateAcademic_termDTO
{
    public class CreateAcademic_termDTO
    {
        public string name { get; set; } = null!;

        public DateOnly start_date { get; set; }

        public DateOnly end_date { get; set; }

        public bool? is_active { get; set; }
    }
}
