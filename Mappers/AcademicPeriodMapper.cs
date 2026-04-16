using SIADAL.Models;
using SIADAL.Models.DTOs.CreateAcademicPeriodDTO;
using SIADAL.Models.DTOs.ReadAcademicPeriodDTO;
using SIADAL.Models.DTOs.UpdateAcademicPeriodDTO;

namespace SIADAL.Mappers
{
    public static class AcademicPeriodMapper
    {
        public static ReadAcademicPeriodDTO ToDto(AcademicPeriod model)
        {
            return new ReadAcademicPeriodDTO
            {
                id = model.id,
                name = model.name,
                start_date = model.start_date,
                end_date = model.end_date,
                is_active = model.is_active
            };
        }

        public static void FromDtoToUpdate(AcademicPeriod model, UpdateAcademicPeriodDTO dto)
        {
            model.name = dto.name ?? model.name;
            model.start_date = dto.start_date ?? model.start_date;
            model.end_date = dto.end_date ?? model.end_date;
            model.is_active = dto.is_active ?? model.is_active;
        }

        public static AcademicPeriod FromDtoToCreate(CreateAcademicPeriodDTO dto)
        {
            return new AcademicPeriod
            {
                name = dto.name,
                start_date = dto.start_date,
                end_date = dto.end_date,
                is_active = dto.is_active ?? false,
            };
        }
    }
}
