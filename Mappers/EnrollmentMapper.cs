using SIADAL.Models;
using SIADAL.Models.DTOs.CreateEnrollmentDTO;
using SIADAL.Models.DTOs.ReadEnrollmentDTO;

namespace SIADAL.Mappers
{
    public static class EnrollmentMapper
    {
        public static ReadEnrollmentDTO ToDto(Enrollment model)
        {
            return new ReadEnrollmentDTO
            {
                student_id = model.student_id,
                student_name = model.student?.user != null
                    ? model.student.user.first_name + " " + model.student.user.last_name
                    : string.Empty,
                class_id = model.class_id,
                class_name = model._class?.name ?? string.Empty,
                enrolled_at = model.enrolled_at
            };
        }

        public static Enrollment FromDtoToCreate(CreateEnrollmentDTO dto)
        {
            return new Enrollment
            {
                student_id = dto.student_id,
                class_id = dto.class_id,
                enrolled_at = DateTime.UtcNow
            };
        }
    }
}
