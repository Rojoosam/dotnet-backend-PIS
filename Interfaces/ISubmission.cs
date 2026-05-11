using SIADAL.Helpers;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateSubmissionDTO;
using SIADAL.Models.DTOs.ReadSubmissionDTO;
using SIADAL.Models.DTOs.UpdateSubmissionDTO;

namespace SIADAL.Interfaces
{
    public interface ISubmission
    {
        Task<List<ReadSubmissionDTO>> GetAllAsync(SubmissionQueryObject query);
        Task<PaginatedResultDTO<ReadSubmissionDTO>> GetAllAsync(SubmissionQueryObject query, int page = 1, int perPage = 10);
        Task<ReadSubmissionDTO?> GetByIdAsync(int id);
        Task<ReadSubmissionDTO> CreateAsync(CreateSubmissionDTO dto);
        Task<ReadSubmissionDTO?> UpdateAsync(int id, UpdateSubmissionDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<string?> GetFileUrlAsync(int id);
        Task<ReadSubmissionDTO?> SetFileUrlAsync(int id, string fileName);
        Task<int?> GetOwnerUserIdAsync(int id);
    }
}
