using SIADAL.Models;
using SIADAL.Models.DTOs.CreateUserDTO;
using SIADAL.Models.DTOs.ReadUserDTO;
using SIADAL.Models.DTOs.UpdateUserDTO;

namespace SIADAL.Mappers
{
    public static class UserMapper
    {
        public static ReadUserDTO ToDto(User model)
        {
            return new ReadUserDTO
            {
                id = model.id,
                email = model.email,
                first_name = model.first_name,
                last_name = model.last_name,
                is_active = model.is_active,
                created_at = model.created_at
            };
        }

        public static void FromDtoToUpdate(User model, UpdateUserDTO dto)
        {
            model.first_name = dto.first_name ?? model.first_name;
            model.last_name = dto.last_name ?? model.last_name;
            model.email = dto.email ?? model.email;
            model.is_active = dto.is_active ?? model.is_active;

            if (!string.IsNullOrEmpty(dto.password))
                model.password_hash = BCrypt.Net.BCrypt.HashPassword(dto.password);
        }

        public static User FromDtoToCreate(CreateUserDTO dto)
        {
            return new User
            {
                first_name = dto.first_name,
                last_name = dto.last_name,
                email = dto.email,
                password_hash = BCrypt.Net.BCrypt.HashPassword(dto.password),
                is_active = dto.is_active ?? true,
                created_at = DateTime.UtcNow,
            };
        }
    }
}
