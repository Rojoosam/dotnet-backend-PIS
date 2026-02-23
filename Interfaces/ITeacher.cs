using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateTeacherDTO;
using SIADAL.Models.DTOs.ReadTeacherDTO;
using SIADAL.Models.DTOs.UpdateTeacherDTO;

namespace SIADAL.Interfaces
{
    public interface ITeacher
    {
        Task<List<ReadTeacherDTO>> GetAllAsync(TeacherQueryObject query);
        Task<PaginatedResultDTO<ReadTeacherDTO>> GetAllAsync(TeacherQueryObject query, int page = 1, int perPage = 10);
        Task<ReadTeacherDTO?> GetByIdAsync(ulong id);
        Task<ReadTeacherDTO> CreateAsync(CreateTeacherDTO dto);
        Task<ReadTeacherDTO?> UpdateAsync(ulong id, UpdateTeacherDTO dto);
        Task<bool> DeleteAsync(ulong id);
        Task<bool> TeacherExistsAsync(ulong id);
    }
}
