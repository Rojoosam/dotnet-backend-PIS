using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateRoleDTO;
using SIADAL.Models.DTOs.ReadRoleDTO;
using SIADAL.Models.DTOs.UpdateRoleDTO;

namespace SIADAL.Interfaces
{
    public interface IRole
    {
        Task<List<ReadRoleDTO>> GetAllAsync(RoleQueryObject query);
        Task<PaginatedResultDTO<ReadRoleDTO>> GetAllAsync(RoleQueryObject query, int page = 1, int perPage = 10);
        Task<ReadRoleDTO?> GetByIdAsync(ulong id);
        Task<ReadRoleDTO> CreateAsync(CreateRoleDTO dto);
        Task<ReadRoleDTO?> UpdateAsync(ulong id, UpdateRoleDTO dto);
        Task<bool> DeleteAsync(ulong id);
        Task<bool> RoleExistsAsync(ulong id);
    }
}
