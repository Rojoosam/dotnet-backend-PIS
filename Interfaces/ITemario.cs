using SIADAL.Models.DTOs.TemarioDTO;

namespace SIADAL.Interfaces
{
    public interface ITemario
    {
        Task<List<ReadTemarioDTO>> GetByProgramIdAsync(int programId);
        Task<ReadTemarioDTO?> GetByIdAsync(int id);
        Task<ReadTemarioDTO> CreateAsync(CreateTemarioDTO dto);
        Task<ReadTemarioDTO?> SetPdfAsync(int id, string fileName);
        Task<string?> GetPdfFileNameAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
