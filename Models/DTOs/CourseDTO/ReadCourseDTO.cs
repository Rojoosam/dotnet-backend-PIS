using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.ReadCourseDTO
{
    public class ReadCourseDTO
    {
        [Required]
        public ulong id { get; set; }

        public string name { get; set; } = null!;

        public string code { get; set; } = null!;

        public int credits { get; set; }

        public string desciption { get; set; } = null!;

    }
}
