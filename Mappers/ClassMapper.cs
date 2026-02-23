using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateClassDTO;
using SIADAL.Models.DTOs.ReadClassDTO;
using SIADAL.Models.DTOs.UpdateClassDTO;
using System.Text.Json;

namespace SIADAL.Mappers
{
    public static class ClassMapper
    {
        public static ReadClassDTO ToDto(Class model)
        {
            return new ReadClassDTO
            {
                id = model.id,
                course_id = model.course_id,
                teacher_id = model.teacher_id,
                academic_term_id = model.academic_term_id,
                room = model.room,
                academic_term_name = model.academic_term.name,
                course_name = model.course.name,
                teacher_name = model.teacher.user.first_name + " " + model.teacher.user.last_name,
                schedule = JsonSerializer.Deserialize<List<ScheduleDTO>>(model.schedule)!
            };
        }

        public static void FromDtoToUpdate(Class model, UpdateClassDTO dto)
        {
            model.course_id = dto.course_id ?? model.course_id;
            model.teacher_id = dto.teacher_id ?? model.teacher_id;
            model.academic_term.id = dto.academic_term_id ?? model.academic_term_id;
            model.schedule = JsonSerializer.Serialize(dto.schedule) ?? model.schedule;
            model.room = dto.room ?? model.room;
            model.updated_at = DateTime.UtcNow;
        }

        public static Class FromDtoToCreate(CreateClassDTO dto)
        {
            return new Class
            {
                course_id = dto.course_id,
                teacher_id = dto.teacher_id,
                academic_term_id = dto.academic_term_id,
                schedule = JsonSerializer.Serialize(dto.schedule),
                room = dto.room,
                created_at = DateTime.UtcNow
            };
        }
    }
}
