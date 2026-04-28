using SIADAL.Models;
using SIADAL.Models.DTOs.CreateAssignmentDTO;
using SIADAL.Models.DTOs.ReadAssignmentDTO;
using SIADAL.Models.DTOs.UpdateAssignmentDTO;

namespace SIADAL.Mappers
{
    public static class AssignmentMapper
    {
        public static ReadAssignmentDTO ToDto(Assignment model)
        {
            return new ReadAssignmentDTO
            {
                id = model.id,
                class_id = model.class_id,
                class_name = model._class?.name ?? string.Empty,
                name = model.name,
                duedate = model.duedate,
                points = model.points,
                details = model.details
            };
        }

        public static void FromDtoToUpdate(Assignment model, UpdateAssignmentDTO dto)
        {
            model.class_id = dto.class_id ?? model.class_id;
            model.name = dto.name ?? model.name;
            model.duedate = dto.duedate ?? model.duedate;
            model.points = dto.points ?? model.points;
            model.details = dto.details ?? model.details;
        }

        public static Assignment FromDtoToCreate(CreateAssignmentDTO dto)
        {
            return new Assignment
            {
                class_id = dto.class_id,
                name = dto.name,
                duedate = dto.duedate,
                points = dto.points,
                details = dto.details
            };
        }
    }
}
