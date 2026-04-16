using System.ComponentModel.DataAnnotations;

namespace SIADAL.Models.DTOs.CreateEnrollmentDTO
{
    public class CreateEnrollmentDTO
    {
        [Required]
        public int student_id { get; set; }
        [Required]
        public int class_id { get; set; }
    }
}
