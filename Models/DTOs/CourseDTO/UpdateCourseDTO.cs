using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.UpdateCourseDTO
{
    public class UpdateCourseDTO
    {

        public string? name { get; set; } = null!;

        public string? code { get; set; } = null!;

        public int? credits { get; set; }

        public string? desciption { get; set; } = null!;

    }
}
