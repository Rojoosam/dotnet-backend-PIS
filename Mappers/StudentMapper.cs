using SIADAL.Models;
using SIADAL.Models.DTOs.CreateStudentDTO;
using SIADAL.Models.DTOs.ReadStudentDTO;
using SIADAL.Models.DTOs.UpdateStudentDTO;

namespace SIADAL.Mappers
{
    public static class StudentMapper
    {
        public static ReadStudentDTO ToDto(Student model)
        {
            return new ReadStudentDTO
            {
                id = model.id,
                enrollment_number = model.enrollment_number,
                birth_date = model.birth_date,
                userId = model.user_id,
                email = model.user.email,
                first_name = model.user.first_name,
                last_name = model.user.last_name
            };
        }

        public static void FromDtoToUpdate(Student model, UpdateStudentDTO dto)
        {
            model.birth_date = dto.birth_date ?? model.birth_date;
            model.enrollment_number = dto.Enrollment_number ?? model.enrollment_number;
            model.user.first_name = dto.first_name ?? model.user.first_name;
            model.user.last_name = dto.last_name ?? model.user.last_name;
            model.user.email = dto.email ?? model.user.email;
            model.updated_at = DateTime.UtcNow;

            if (dto.password != null)
                model.user.password = BCrypt.Net.BCrypt.HashPassword(dto.password) ?? model.user.password;

        }

        public static Student FromDtoToCreate(CreateStudentDTO dto)
        {
            return new Student
            {
               birth_date = dto.birth_date,
               enrollment_number = dto.Enrollment_number,

                user = new User
                {
                    first_name = dto.first_name,
                    last_name = dto.last_name,
                    email = dto.email,
                    password = BCrypt.Net.BCrypt.HashPassword(dto.password),
                    is_active = dto.is_active ?? true,
                    created_at = DateTime.UtcNow
                },
            };
        }
    }
}
