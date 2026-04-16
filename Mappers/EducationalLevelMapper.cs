using SIADAL.Models;
using SIADAL.Models.DTOs.CreateEducationalLevelDTO;
using SIADAL.Models.DTOs.ReadEducationalLevelDTO;
using SIADAL.Models.DTOs.UpdateEducationalLevelDTO;

namespace SIADAL.Mappers
{
    public static class EducationalLevelMapper
    {
        public static ReadEducationalLevelDTO ToDto(EducationalLevel model)
        {
            return new ReadEducationalLevelDTO
            {
                id = model.id,
                name = model.name
            };
        }

        public static void FromDtoToUpdate(EducationalLevel model, UpdateEducationalLevelDTO dto)
        {
            model.name = dto.name ?? model.name;
        }

        public static EducationalLevel FromDtoToCreate(CreateEducationalLevelDTO dto)
        {
            return new EducationalLevel
            {
                name = dto.name
            };
        }
    }
}
