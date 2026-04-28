using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateStudentDTO;
using SIADAL.Models.DTOs.ReadStudentDTO;
using SIADAL.Models.DTOs.UpdateStudentDTO;

namespace SIADAL.Interfaces
{
    public interface IStudent
    {
        Task<List<ReadStudentDTO>> GetAllAsync(StudentQueryObject query);
        Task<PaginatedResultDTO<ReadStudentDTO>> GetAllAsync(StudentQueryObject query, int page = 1, int perPage = 10);
        Task<ReadStudentDTO?> GetByIdAsync(int id);
        Task<ReadStudentDTO> CreateAsync(CreateStudentDTO dto);
        Task<ReadStudentDTO?> UpdateAsync(int id, UpdateStudentDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> StudentExistsAsync(int id);
    }
}
