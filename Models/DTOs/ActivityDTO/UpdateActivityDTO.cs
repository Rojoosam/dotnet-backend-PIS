using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.UpdateActivityDTO
{
    public class UpdateActivityDTO
    {

        public ulong? class_id { get; set; }
        public ulong? student_id { get; set; }
        public string? name { get; set; } = null!;
        public decimal? grade { get; set; }
        public decimal? porcentage { get; set; }
        public string? status { get; set; } = null!;
    }
}
