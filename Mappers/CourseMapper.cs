using SIADAL.Models;
using SIADAL.Models.DTOs.CreateCourseDTO;
using SIADAL.Models.DTOs.ReadCourseDTO;
using SIADAL.Models.DTOs.UpdateCourseDTO;
using System.Xml.Linq;

namespace SIADAL.Mappers
{
    public static class CourseMapper
    {
        public static ReadCourseDTO ToDto(Course model)
        {
            return new ReadCourseDTO
            {
                id = model.id,
                name = model.name,
                code = model.code,
                credits = model.credits,
                desciption = model.desciption
            };
        }

        public static void FromDtoToUpdate(Course model, UpdateCourseDTO dto)
        {
            model.name = dto.name ?? model.name;
            model.code = dto.code ?? model.code;
            model.credits = dto.credits ?? model.credits;
            model.desciption = dto.desciption ?? model.desciption;
            model.updated_at = DateTime.UtcNow;
        }

        public static Course FromDtoToCreate(CreateCourseDTO dto)
        {
            return new Course
            {
                name = dto.name,
                code = dto.code,
                credits = dto.credits,
                desciption = dto.desciption ?? string.Empty,
                created_at = DateTime.UtcNow
            };
        }
    }
}
