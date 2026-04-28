using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateSubmissionDTO
{
    public class CreateSubmissionDTO
    {
        [Required]
        public int assignment_id { get; set; }
        [Required]
        public int student_id { get; set; }
        public int? grade { get; set; }
        public string? file_url { get; set; }
    }
}
