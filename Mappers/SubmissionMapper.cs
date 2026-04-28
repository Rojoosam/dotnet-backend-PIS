using SIADAL.Models;
using SIADAL.Models.DTOs.CreateSubmissionDTO;
using SIADAL.Models.DTOs.ReadSubmissionDTO;
using SIADAL.Models.DTOs.UpdateSubmissionDTO;

namespace SIADAL.Mappers
{
    public static class SubmissionMapper
    {
        public static ReadSubmissionDTO ToDto(Submission model)
        {
            return new ReadSubmissionDTO
            {
                id = model.id,
                assignment_id = model.assignment_id,
                assignment_name = model.assignment?.name ?? string.Empty,
                student_id = model.student_id,
                student_name = model.student?.user != null
                    ? model.student.user.first_name + " " + model.student.user.last_name
                    : string.Empty,
                submitted_at = model.submitted_at,
                grade = model.grade,
                file_url = model.file_url
            };
        }

        public static void FromDtoToUpdate(Submission model, UpdateSubmissionDTO dto)
        {
            model.grade = dto.grade ?? model.grade;
            model.file_url = dto.file_url ?? model.file_url;
        }

        public static Submission FromDtoToCreate(CreateSubmissionDTO dto)
        {
            return new Submission
            {
                assignment_id = dto.assignment_id,
                student_id = dto.student_id,
                grade = dto.grade,
                file_url = dto.file_url,
                submitted_at = DateTime.UtcNow
            };
        }
    }
}
