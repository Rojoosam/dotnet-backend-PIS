using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Interfaces;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateStudentDTO;
using SIADAL.Models.DTOs.ReadStudentDTO;
using SIADAL.Models.DTOs.UpdateStudentDTO;
using SIADAL.Mappers;
using SIADAL.Helpers;

namespace SIADAL.Repository
{
    public class StudentRepository : IStudent
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadStudentDTO> BuildQuery(StudentQueryObject query)
        {
            var queryable = _context.Set<Student>()
                .AsNoTracking()
                .Select(m => new ReadStudentDTO
                {
                    id = m.id,
                    enrollment_number = m.enrollment_number,
                    birth_date = m.birth_date,
                    user_id = m.user.id,
                    email = m.user.email,
                    first_name = m.user.first_name,
                    last_name = m.user.last_name,
                    is_active = m.user.is_active,
                    program_id = m.program_id,
                    program_name = m.program.name
                });

            if (!string.IsNullOrEmpty(query.email))
                queryable = queryable.Where(m => m.email.Contains(query.email));

            if (!string.IsNullOrEmpty(query.enrollment_number))
                queryable = queryable.Where(m => m.enrollment_number.Contains(query.enrollment_number));

            if (!string.IsNullOrEmpty(query.first_name))
                queryable = queryable.Where(m => m.first_name.Contains(query.first_name));

            if (!string.IsNullOrEmpty(query.last_name))
                queryable = queryable.Where(m => m.last_name.Contains(query.last_name));

            if (query.is_active.HasValue)
                queryable = queryable.Where(m => m.is_active == query.is_active.Value);

            if (query.program_id.HasValue)
                queryable = queryable.Where(m => m.program_id == query.program_id.Value);

            return queryable.OrderBy(m => m.last_name);
        }

        public async Task<List<ReadStudentDTO>> GetAllAsync(StudentQueryObject query)
        {
            return await BuildQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadStudentDTO>> GetAllAsync(StudentQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadStudentDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadStudentDTO?> GetByIdAsync(int id)
        {
            return await _context.Set<Student>()
                .Where(m => m.id == id)
                .Select(m => new ReadStudentDTO
                {
                    id = m.id,
                    user_id = m.user.id,
                    email = m.user.email,
                    first_name = m.user.first_name,
                    last_name = m.user.last_name,
                    enrollment_number = m.enrollment_number,
                    birth_date = m.birth_date,
                    is_active = m.user.is_active,
                    program_id = m.program_id,
                    program_name = m.program.name
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadStudentDTO> CreateAsync(CreateStudentDTO dto)
        {
            var entity = StudentMapper.FromDtoToCreate(dto);
            await _context.Set<Student>().AddAsync(entity);
            await _context.SaveChangesAsync();

            // Reload with program
            await _context.Entry(entity).Reference(s => s.program).LoadAsync();
            return StudentMapper.ToDto(entity);
        }

        public async Task<ReadStudentDTO?> UpdateAsync(int id, UpdateStudentDTO dto)
        {
            var entity = await _context.students
                .Include(s => s.user)
                .Include(s => s.program)
                .FirstOrDefaultAsync(s => s.id == id);

            if (entity == null) return null;

            StudentMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();

            if (dto.program_id.HasValue)
                await _context.Entry(entity).Reference(s => s.program).LoadAsync();

            return StudentMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.students
                .Include(s => s.user)
                .FirstOrDefaultAsync(s => s.id == id);
            if (entity == null) return false;

            _context.Set<Student>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> StudentExistsAsync(int id)
        {
            return await _context.Set<Student>().AnyAsync(m => m.id == id);
        }
    }
}
