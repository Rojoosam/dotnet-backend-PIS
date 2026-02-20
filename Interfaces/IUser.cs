using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateUserDTO;
using SIADAL.Models.DTOs.ReadUserDTO;
using SIADAL.Models.DTOs.UpdateUserDTO;

namespace SIADAL.Interfaces
{
    public interface IUser
    {
        Task<List<ReadUserDTO>> GetAllAsync(UserQueryObject query);
        Task<PaginatedResultDTO<ReadUserDTO>> GetAllAsync(UserQueryObject query, int page = 1, int perPage = 10);
        Task<ReadUserDTO?> GetByIdAsync(ulong id);
        Task<ReadUserDTO> CreateAsync(CreateUserDTO dto);
        Task<ReadUserDTO?> UpdateAsync(ulong id, UpdateUserDTO dto);
        Task<bool> DeleteAsync(ulong id);
        Task<bool> UserExistsAsync(ulong id);
    }
}
