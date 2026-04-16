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
        Task<ReadUserDTO?> GetByIdAsync(int id);
        Task<ReadUserDTO> CreateAsync(CreateUserDTO dto);
        Task<ReadUserDTO?> UpdateAsync(int id, UpdateUserDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UserExistsAsync(int id);
    }
}
