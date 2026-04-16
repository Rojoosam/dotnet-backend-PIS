using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Interfaces;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateTeacherDTO;
using SIADAL.Models.DTOs.ReadTeacherDTO;
using SIADAL.Models.DTOs.UpdateTeacherDTO;
using SIADAL.Mappers;
using SIADAL.Helpers;

namespace SIADAL.Repository
{
    public class TeacherRepository : ITeacher
    {
        private readonly AppDbContext _context;

        public TeacherRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadTeacherDTO> BuildQuery(TeacherQueryObject query)
        {
            var queryable = _context.Set<Teacher>()
                .AsNoTracking()
                .Select(m => new ReadTeacherDTO
                {
                    id = m.id,
                    user_id = m.user_id,
                    employee_number = m.employee_number,
                    first_name = m.user.first_name,
                    last_name = m.user.last_name,
                    is_active = m.user.is_active,
                    email = m.user.email
                });

            if (!string.IsNullOrEmpty(query.last_name))
                queryable = queryable.Where(m => m.last_name.Contains(query.last_name));

            if (!string.IsNullOrEmpty(query.first_name))
                queryable = queryable.Where(m => m.first_name.Contains(query.first_name));

            if (!string.IsNullOrEmpty(query.employee_number))
                queryable = queryable.Where(m => m.employee_number.Contains(query.employee_number));

            if (query.is_active.HasValue)
                queryable = queryable.Where(m => m.is_active == query.is_active.Value);

            if (!string.IsNullOrEmpty(query.email))
                queryable = queryable.Where(m => m.email.Contains(query.email));

            return queryable.OrderBy(m => m.last_name);
        }

        public async Task<List<ReadTeacherDTO>> GetAllAsync(TeacherQueryObject query)
        {
            return await BuildQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadTeacherDTO>> GetAllAsync(TeacherQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadTeacherDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadTeacherDTO?> GetByIdAsync(int id)
        {
            return await _context.Set<Teacher>()
                .Where(m => m.id == id)
                .Select(m => new ReadTeacherDTO
                {
                    id = m.id,
                    user_id = m.user_id,
                    employee_number = m.employee_number,
                    first_name = m.user.first_name,
                    last_name = m.user.last_name,
                    is_active = m.user.is_active,
                    email = m.user.email
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadTeacherDTO> CreateAsync(CreateTeacherDTO dto)
        {
            var entity = TeacherMapper.FromDtoToCreate(dto);
            await _context.Set<Teacher>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return TeacherMapper.ToDto(entity);
        }

        public async Task<ReadTeacherDTO?> UpdateAsync(int id, UpdateTeacherDTO dto)
        {
            var entity = await _context.teachers
                .Include(t => t.user)
                .FirstOrDefaultAsync(t => t.id == id);
            if (entity == null) return null;

            TeacherMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();
            return TeacherMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.teachers
                .Include(s => s.user)
                .FirstOrDefaultAsync(t => t.id == id);
            if (entity == null) return false;

            _context.Set<Teacher>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TeacherExistsAsync(int id)
        {
            return await _context.Set<Teacher>().AnyAsync(m => m.id == id);
        }
    }
}
