using SIADAL.Models;
using SIADAL.Models.DTOs.CreateRoleDTO;
using SIADAL.Models.DTOs.ReadRoleDTO;
using SIADAL.Models.DTOs.UpdateRoleDTO;

namespace SIADAL.Mappers
{
    public static class RoleMapper
    {
        public static ReadRoleDTO ToDto(Role model)
        {
            return new ReadRoleDTO
            {
                id          = model.id,
                name        = model.name,
                created_at  = model.created_at
            };
        }

        public static void FromDtoToUpdate(Role model, UpdateRoleDTO dto)
        {
            model.name          = dto.name ?? model.name;
            model.updated_at    = DateTime.UtcNow;
        }

        public static Role FromDtoToCreate(CreateRoleDTO dto)
        {
            return new Role
            {
                name        = dto.name,
                created_at  = DateTime.UtcNow
            };
        }
    }
}
