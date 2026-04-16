using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateProgramDTO;
using SIADAL.Models.DTOs.ReadProgramDTO;
using SIADAL.Models.DTOs.UpdateProgramDTO;

namespace SIADAL.Interfaces
{
    public interface IProgram
    {
        Task<List<ReadProgramDTO>> GetAllAsync(ProgramQueryObject query);
        Task<PaginatedResultDTO<ReadProgramDTO>> GetAllAsync(ProgramQueryObject query, int page = 1, int perPage = 10);
        Task<ReadProgramDTO?> GetByIdAsync(int id);
        Task<ReadProgramDTO> CreateAsync(CreateProgramDTO dto);
        Task<ReadProgramDTO?> UpdateAsync(int id, UpdateProgramDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
