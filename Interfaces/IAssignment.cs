using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateAssignmentDTO;
using SIADAL.Models.DTOs.ReadAssignmentDTO;
using SIADAL.Models.DTOs.UpdateAssignmentDTO;

namespace SIADAL.Interfaces
{
    public interface IAssignment
    {
        Task<List<ReadAssignmentDTO>> GetAllAsync(AssignmentQueryObject query);
        Task<PaginatedResultDTO<ReadAssignmentDTO>> GetAllAsync(AssignmentQueryObject query, int page = 1, int perPage = 10);
        Task<ReadAssignmentDTO?> GetByIdAsync(int id);
        Task<ReadAssignmentDTO> CreateAsync(CreateAssignmentDTO dto);
        Task<ReadAssignmentDTO?> UpdateAsync(int id, UpdateAssignmentDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
