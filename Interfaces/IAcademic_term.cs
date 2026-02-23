using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateAcademic_termDTO;
using SIADAL.Models.DTOs.ReadAcademic_termDTO;
using SIADAL.Models.DTOs.UpdateAcademic_termDTO;

namespace SIADAL.Interfaces
{
    public interface IAcademic_term
    {
        Task<List<ReadAcademic_termDTO>> GetAllAsync(Academic_termQueryObject query);
        Task<PaginatedResultDTO<ReadAcademic_termDTO>> GetAllAsync(Academic_termQueryObject query, int page = 1, int perPage = 10);
        Task<ReadAcademic_termDTO?> GetByIdAsync(ulong id);
        Task<ReadAcademic_termDTO> CreateAsync(CreateAcademic_termDTO dto);
        Task<ReadAcademic_termDTO?> UpdateAsync(ulong id, UpdateAcademic_termDTO dto);
        Task<bool> DeleteAsync(ulong id);
        Task<bool> Academic_termExistsAsync(ulong id);
    }
}
