using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Mappers;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateSubmissionDTO;
using SIADAL.Models.DTOs.ReadSubmissionDTO;
using SIADAL.Models.DTOs.UpdateSubmissionDTO;

namespace SIADAL.Repository
{
    public class SubmissionRepository : ISubmission
    {
        private readonly AppDbContext _context;

        public SubmissionRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadSubmissionDTO> BuildQuery(SubmissionQueryObject query)
        {
            var queryable = _context.Set<Submission>()
                .AsNoTracking()
                .Select(m => new ReadSubmissionDTO
                {
                    id = m.id,
                    assignment_id = m.assignment_id,
                    assignment_name = m.assignment.name,
                    student_id = m.student_id,
                    student_name = m.student.user.first_name + " " + m.student.user.last_name,
                    submitted_at = m.submitted_at,
                    grade = m.grade,
                    file_url = m.file_url
                });

            if (query.assignment_id.HasValue)
                queryable = queryable.Where(m => m.assignment_id == query.assignment_id.Value);

            if (query.student_id.HasValue)
                queryable = queryable.Where(m => m.student_id == query.student_id.Value);

            return queryable.OrderByDescending(m => m.submitted_at);
        }

        public async Task<List<ReadSubmissionDTO>> GetAllAsync(SubmissionQueryObject query)
        {
            return await BuildQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadSubmissionDTO>> GetAllAsync(SubmissionQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadSubmissionDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadSubmissionDTO?> GetByIdAsync(int id)
        {
            return await _context.Set<Submission>()
                .Where(m => m.id == id)
                .Select(m => new ReadSubmissionDTO
                {
                    id = m.id,
                    assignment_id = m.assignment_id,
                    assignment_name = m.assignment.name,
                    student_id = m.student_id,
                    student_name = m.student.user.first_name + " " + m.student.user.last_name,
                    submitted_at = m.submitted_at,
                    grade = m.grade,
                    file_url = m.file_url
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadSubmissionDTO> CreateAsync(CreateSubmissionDTO dto)
        {
            var entity = SubmissionMapper.FromDtoToCreate(dto);
            await _context.Set<Submission>().AddAsync(entity);
            await _context.SaveChangesAsync();

            var created = await _context.submissions
                .Include(s => s.assignment)
                .Include(s => s.student).ThenInclude(st => st.user)
                .FirstAsync(s => s.id == entity.id);

            return SubmissionMapper.ToDto(created);
        }

        public async Task<ReadSubmissionDTO?> UpdateAsync(int id, UpdateSubmissionDTO dto)
        {
            var entity = await _context.submissions
                .Include(s => s.assignment)
                .Include(s => s.student).ThenInclude(st => st.user)
                .FirstOrDefaultAsync(s => s.id == id);
            if (entity == null) return null;

            SubmissionMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();
            return SubmissionMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<Submission>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<Submission>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
