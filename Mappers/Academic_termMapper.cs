using SIADAL.Models;
using SIADAL.Models.DTOs.CreateAcademic_termDTO;
using SIADAL.Models.DTOs.ReadAcademic_termDTO;
using SIADAL.Models.DTOs.UpdateAcademic_termDTO;

namespace SIADAL.Mappers
{
    public static class Academic_termMapper
    {
        public static ReadAcademic_termDTO ToDto(Academic_term model)
        {
            return new ReadAcademic_termDTO
            {
                id = model.id,
                name = model.name,
                start_date = model.start_date,
                end_date = model.end_date,
                is_active = model.is_active
            };
        }

        public static void FromDtoToUpdate(Academic_term model, UpdateAcademic_termDTO dto)
        {
            model.name = dto.name ?? model.name;
            model.start_date = dto.start_date ?? model.start_date;
            model.end_date = dto.end_date ?? model.end_date;
            model.is_active = dto.is_active ?? model.is_active;
            model.updated_at = DateTime.UtcNow;
        }

        public static Academic_term FromDtoToCreate(CreateAcademic_termDTO dto)
        {
            return new Academic_term
            {
                name = dto.name,
                start_date = dto.start_date,
                end_date = dto.end_date,
                is_active = dto.is_active,  
                created_at = DateTime.UtcNow,
            };
        }
    }
}
