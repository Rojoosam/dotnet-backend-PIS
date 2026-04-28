using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Mappers;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateEnrollmentDTO;
using SIADAL.Models.DTOs.ReadEnrollmentDTO;

namespace SIADAL.Repository
{
    public class EnrollmentRepository : IEnrollment
    {
        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadEnrollmentDTO> BuildQuery(EnrollmentQueryObject query)
        {
            var queryable = _context.Set<Enrollment>()
                .AsNoTracking()
                .Select(m => new ReadEnrollmentDTO
                {
                    student_id = m.student_id,
                    student_name = m.student.user.first_name + " " + m.student.user.last_name,
                    class_id = m.class_id,
                    class_name = m._class.name,
                    enrolled_at = m.enrolled_at
                });

            if (query.student_id.HasValue)
                queryable = queryable.Where(m => m.student_id == query.student_id.Value);

            if (query.class_id.HasValue)
                queryable = queryable.Where(m => m.class_id == query.class_id.Value);

            return queryable.OrderByDescending(m => m.enrolled_at);
        }

        public async Task<List<ReadEnrollmentDTO>> GetAllAsync(EnrollmentQueryObject query)
        {
            return await BuildQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadEnrollmentDTO>> GetAllAsync(EnrollmentQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadEnrollmentDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadEnrollmentDTO?> GetByIdAsync(int studentId, int classId)
        {
            return await _context.Set<Enrollment>()
                .Where(m => m.student_id == studentId && m.class_id == classId)
                .Select(m => new ReadEnrollmentDTO
                {
                    student_id = m.student_id,
                    student_name = m.student.user.first_name + " " + m.student.user.last_name,
                    class_id = m.class_id,
                    class_name = m._class.name,
                    enrolled_at = m.enrolled_at
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadEnrollmentDTO> CreateAsync(CreateEnrollmentDTO dto)
        {
            var entity = EnrollmentMapper.FromDtoToCreate(dto);
            await _context.Set<Enrollment>().AddAsync(entity);
            await _context.SaveChangesAsync();

            // Reload with navigation properties
            var created = await _context.enrollments
                .Include(e => e.student).ThenInclude(s => s.user)
                .Include(e => e._class)
                .FirstAsync(e => e.student_id == entity.student_id && e.class_id == entity.class_id);

            return EnrollmentMapper.ToDto(created);
        }

        public async Task<bool> DeleteAsync(int studentId, int classId)
        {
            var entity = await _context.Set<Enrollment>()
                .FirstOrDefaultAsync(e => e.student_id == studentId && e.class_id == classId);
            if (entity == null) return false;

            _context.Set<Enrollment>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
