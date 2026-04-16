using SIADAL.Models;
using SIADAL.Models.DTOs.CreateClassDTO;
using SIADAL.Models.DTOs.ReadClassDTO;
using SIADAL.Models.DTOs.UpdateClassDTO;

namespace SIADAL.Mappers
{
    public static class ClassMapper
    {
        public static ReadClassDTO ToDto(Class model)
        {
            return new ReadClassDTO
            {
                id = model.id,
                name = model.name,
                schedule_json = model.schedule_json,
                period_id = model.period_id,
                period_name = model.academic_period?.name ?? string.Empty,
                program_id = model.program_id,
                program_name = model.program?.name ?? string.Empty,
                teacher_id = model.teacher_id,
                teacher_name = model.teacher?.user != null
                    ? model.teacher.user.first_name + " " + model.teacher.user.last_name
                    : string.Empty
            };
        }

        public static void FromDtoToUpdate(Class model, UpdateClassDTO dto)
        {
            model.period_id = dto.period_id ?? model.period_id;
            model.program_id = dto.program_id ?? model.program_id;
            model.teacher_id = dto.teacher_id ?? model.teacher_id;
            model.name = dto.name ?? model.name;
            model.schedule_json = dto.schedule_json ?? model.schedule_json;
        }

        public static Class FromDtoToCreate(CreateClassDTO dto)
        {
            return new Class
            {
                period_id = dto.period_id,
                program_id = dto.program_id,
                teacher_id = dto.teacher_id,
                name = dto.name,
                schedule_json = dto.schedule_json
            };
        }
    }
}
