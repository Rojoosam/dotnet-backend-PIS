using SIADAL.Models;
using SIADAL.Models.DTOs.CreateActivityDTO;
using SIADAL.Models.DTOs.ReadActivityDTO;
using SIADAL.Models.DTOs.UpdateActivityDTO;

namespace SIADAL.Mappers
{
    public static class ActivityMapper
    {
        public static ReadActivityDTO ToDto(Activity model)
        {
            return new ReadActivityDTO
            {
                id          = model.id,
                class_id    = model.class_id,
                student_id  = model.student_id,
                name        = model.name,
                grade       = model.grade,
                porcentage  = model.porcentage,
                status      = model.status
            };
        }

        public static void FromDtoToUpdate(Activity model, UpdateActivityDTO dto)
        {
            model.class_id      = dto.class_id     ?? model.class_id;
            model.student_id    = dto.student_id   ?? model.student_id;
            model.name          = dto.name         ?? model.name;
            model.grade         = dto.grade        ?? model.grade;
            model.porcentage    = dto.porcentage   ?? model.porcentage;
            model.status        = dto.status       ?? model.status;
            model.updated_at    = DateTime.UtcNow;
        }

        public static Activity FromDtoToCreate(CreateActivityDTO dto)
        {
            return new Activity
            {
                class_id        = dto.class_id,
                student_id      = dto.student_id,
                name            = dto.name,
                grade           = dto.grade,
                porcentage      = dto.porcentage,
                status          = dto.status,
                created_at      = DateTime.UtcNow
            };
        }
    }
}
