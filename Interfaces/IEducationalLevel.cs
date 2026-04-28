using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateEducationalLevelDTO;
using SIADAL.Models.DTOs.ReadEducationalLevelDTO;
using SIADAL.Models.DTOs.UpdateEducationalLevelDTO;

namespace SIADAL.Interfaces
{
    public interface IEducationalLevel
    {
        Task<List<ReadEducationalLevelDTO>> GetAllAsync(EducationalLevelQueryObject query);
        Task<PaginatedResultDTO<ReadEducationalLevelDTO>> GetAllAsync(EducationalLevelQueryObject query, int page = 1, int perPage = 10);
        Task<ReadEducationalLevelDTO?> GetByIdAsync(int id);
        Task<ReadEducationalLevelDTO> CreateAsync(CreateEducationalLevelDTO dto);
        Task<ReadEducationalLevelDTO?> UpdateAsync(int id, UpdateEducationalLevelDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
