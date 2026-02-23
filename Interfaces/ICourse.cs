using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateCourseDTO;
using SIADAL.Models.DTOs.ReadCourseDTO;
using SIADAL.Models.DTOs.UpdateCourseDTO;

namespace SIADAL.Interfaces
{
    public interface ICourse
    {
        Task<List<ReadCourseDTO>> GetAllAsync(CourseQueryObject query);
        Task<PaginatedResultDTO<ReadCourseDTO>> GetAllAsync(CourseQueryObject query, int page = 1, int perPage = 10);
        Task<ReadCourseDTO?> GetByIdAsync(ulong id);
        Task<ReadCourseDTO> CreateAsync(CreateCourseDTO dto);
        Task<ReadCourseDTO?> UpdateAsync(ulong id, UpdateCourseDTO dto);
        Task<bool> DeleteAsync(ulong id);
        Task<bool> CourseExistsAsync(ulong id);
    }
}
