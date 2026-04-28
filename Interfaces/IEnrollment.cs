using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateEnrollmentDTO;
using SIADAL.Models.DTOs.ReadEnrollmentDTO;

namespace SIADAL.Interfaces
{
    public interface IEnrollment
    {
        Task<List<ReadEnrollmentDTO>> GetAllAsync(EnrollmentQueryObject query);
        Task<PaginatedResultDTO<ReadEnrollmentDTO>> GetAllAsync(EnrollmentQueryObject query, int page = 1, int perPage = 10);
        Task<ReadEnrollmentDTO?> GetByIdAsync(int studentId, int classId);
        Task<ReadEnrollmentDTO> CreateAsync(CreateEnrollmentDTO dto);
        Task<bool> DeleteAsync(int studentId, int classId);
    }
}
