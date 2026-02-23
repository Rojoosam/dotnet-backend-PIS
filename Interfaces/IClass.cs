using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateClassDTO;
using SIADAL.Models.DTOs.ReadClassDTO;
using SIADAL.Models.DTOs.UpdateClassDTO;

namespace SIADAL.Interfaces
{
    public interface IClass
    {
        Task<List<ReadClassDTO>> GetAllAsync(ClassQueryObject query);
        Task<PaginatedResultDTO<ReadClassDTO>> GetAllAsync(ClassQueryObject query, int page = 1, int perPage = 10);
        Task<ReadClassDTO?> GetByIdAsync(ulong id);
        Task<ReadClassDTO> CreateAsync(CreateClassDTO dto);
        Task<ReadClassDTO?> UpdateAsync(ulong id, UpdateClassDTO dto);
        Task<bool> DeleteAsync(ulong id);
        Task<bool> ClassExistsAsync(ulong id);
    }
}
