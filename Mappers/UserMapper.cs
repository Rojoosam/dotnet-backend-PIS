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
                Id = model.id,
                Email = model.email,
                First_name = model.first_name,
                Last_name = model.last_name,
            };
        }

        public static void FromDtoToUpdate(User model, UpdateUserDTO dto)
        {
            model.first_name = dto.first_name ?? model.first_name;
            model.last_name = dto.last_name ?? model.last_name;
            model.email = dto.email ?? model.email;
            model.password = BCrypt.Net.BCrypt.HashPassword(dto.password) ?? model.password;
            model.updated_at = DateTime.UtcNow;
            model.is_active = dto.is_active ?? model.is_active;
        }

        public static User FromDtoToCreate(CreateUserDTO dto)
        {
            return new User
            {
                first_name = dto.first_name,
                last_name = dto.last_name,
                email = dto.email,
                password = BCrypt.Net.BCrypt.HashPassword(dto.password),
                is_active = true,
            };
        }
    }
}
