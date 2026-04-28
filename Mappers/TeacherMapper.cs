using SIADAL.Models;
using SIADAL.Models.DTOs.CreateTeacherDTO;
using SIADAL.Models.DTOs.ReadTeacherDTO;
using SIADAL.Models.DTOs.UpdateTeacherDTO;

namespace SIADAL.Mappers
{
    public static class TeacherMapper
    {
        public static ReadTeacherDTO ToDto(Teacher model)
        {
            return new ReadTeacherDTO
            {
                id = model.id,
                user_id = model.user_id,
                employee_number = model.employee_number,
                first_name = model.user.first_name,
                last_name = model.user.last_name,
                is_active = model.user.is_active,
                email = model.user.email
            };
        }

        public static void FromDtoToUpdate(Teacher model, UpdateTeacherDTO dto)
        {
            model.employee_number = dto.employee_number ?? model.employee_number;
            model.user.first_name = dto.first_name ?? model.user.first_name;
            model.user.last_name = dto.last_name ?? model.user.last_name;
            model.user.is_active = dto.is_active ?? model.user.is_active;
            model.user.email = dto.email ?? model.user.email;
        }

        public static Teacher FromDtoToCreate(CreateTeacherDTO dto)
        {
            return new Teacher
            {
                employee_number = dto.employee_number,
                user = new User
                {
                    first_name = dto.first_name,
                    last_name = dto.last_name,
                    email = dto.email,
                    password_hash = BCrypt.Net.BCrypt.HashPassword(dto.password),
                    is_active = dto.is_active ?? true,
                    created_at = DateTime.UtcNow
                },
            };
        }
    }
}
