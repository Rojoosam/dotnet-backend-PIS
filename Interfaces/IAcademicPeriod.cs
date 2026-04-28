using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateAcademicPeriodDTO;
using SIADAL.Models.DTOs.ReadAcademicPeriodDTO;
using SIADAL.Models.DTOs.UpdateAcademicPeriodDTO;

namespace SIADAL.Interfaces
{
    public interface IAcademicPeriod
    {
        Task<List<ReadAcademicPeriodDTO>> GetAllAsync(AcademicPeriodQueryObject query);
        Task<PaginatedResultDTO<ReadAcademicPeriodDTO>> GetAllAsync(AcademicPeriodQueryObject query, int page = 1, int perPage = 10);
        Task<ReadAcademicPeriodDTO?> GetByIdAsync(int id);
        Task<ReadAcademicPeriodDTO> CreateAsync(CreateAcademicPeriodDTO dto);
        Task<ReadAcademicPeriodDTO?> UpdateAsync(int id, UpdateAcademicPeriodDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
