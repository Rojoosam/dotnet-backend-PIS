using SIADAL.Models;
using SIADAL.Models.DTOs.CreateProgramDTO;
using SIADAL.Models.DTOs.ReadProgramDTO;
using SIADAL.Models.DTOs.UpdateProgramDTO;

namespace SIADAL.Mappers
{
    public static class ProgramMapper
    {
        public static ReadProgramDTO ToDto(Models.Program model)
        {
            return new ReadProgramDTO
            {
                id = model.id,
                name = model.name,
                level_id = model.level_id,
                level_name = model.educational_level?.name ?? string.Empty
            };
        }

        public static void FromDtoToUpdate(Models.Program model, UpdateProgramDTO dto)
        {
            model.name = dto.name ?? model.name;
            model.level_id = dto.level_id ?? model.level_id;
        }

        public static Models.Program FromDtoToCreate(CreateProgramDTO dto)
        {
            return new Models.Program
            {
                name = dto.name,
                level_id = dto.level_id
            };
        }
    }
}
