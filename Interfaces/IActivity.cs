using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateActivityDTO;
using SIADAL.Models.DTOs.ReadActivityDTO;
using SIADAL.Models.DTOs.UpdateActivityDTO;

namespace SIADAL.Interfaces
{
    public interface IActivity
    {
        Task<List<ReadActivityDTO>> GetAllAsync(ActivityQueryObject query);
        Task<PaginatedResultDTO<ReadActivityDTO>> GetAllAsync(ActivityQueryObject query, int page = 1, int perPage = 10);
        Task<ReadActivityDTO?> GetByIdAsync(ulong id);
        Task<ReadActivityDTO> CreateAsync(CreateActivityDTO dto);
        Task<ReadActivityDTO?> UpdateAsync(ulong id, UpdateActivityDTO dto);
        Task<bool> DeleteAsync(ulong id);
        Task<bool> ActivityExistsAsync(ulong id);
    }
}
