using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Mappers;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateClassDTO;
using SIADAL.Models.DTOs.ReadClassDTO;
using SIADAL.Models.DTOs.UpdateClassDTO;

namespace SIADAL.Repository
{
    public class ClassRepository : IClass
    {
        private readonly AppDbContext _context;

        public ClassRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadClassDTO> BuildQuery(ClassQueryObject query)
        {
            var queryable = _context.Set<Class>()
                .AsNoTracking()
                .Select(m => new ReadClassDTO
                {
                    id = m.id,
                    name = m.name,
                    schedule_json = m.schedule_json,
                    period_id = m.period_id,
                    period_name = m.academic_periods.name,
                    program_id = m.program_id,
                    program_name = m.program.name,
                    teacher_id = m.teacher_id,
                    teacher_name = m.teacher.user.first_name + " " + m.teacher.user.last_name
                });

            if (!string.IsNullOrEmpty(query.name))
                queryable = queryable.Where(m => m.name.Contains(query.name));

            if (query.period_id.HasValue)
                queryable = queryable.Where(m => m.period_id == query.period_id.Value);

            if (query.program_id.HasValue)
                queryable = queryable.Where(m => m.program_id == query.program_id.Value);

            if (query.teacher_id.HasValue)
                queryable = queryable.Where(m => m.teacher_id == query.teacher_id.Value);

            return queryable.OrderBy(m => m.name);
        }

        public async Task<List<ReadClassDTO>> GetAllAsync(ClassQueryObject query)
        {
            return await BuildQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadClassDTO>> GetAllAsync(ClassQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadClassDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadClassDTO?> GetByIdAsync(int id)
        {
            return await _context.Set<Class>()
                .AsNoTracking()
                .Where(m => m.id == id)
                .Select(m => new ReadClassDTO
                {
                    id = m.id,
                    name = m.name,
                    schedule_json = m.schedule_json,
                    period_id = m.period_id,
                    period_name = m.academic_periods.name,
                    program_id = m.program_id,
                    program_name = m.program.name,
                    teacher_id = m.teacher_id,
                    teacher_name = m.teacher.user.first_name + " " + m.teacher.user.last_name
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadClassDTO> CreateAsync(CreateClassDTO dto)
        {
            var entity = ClassMapper.FromDtoToCreate(dto);
            await _context.Set<Class>().AddAsync(entity);
            await _context.SaveChangesAsync();

            var created = await _context.classes
                .Include(c => c.academic_periods)
                .Include(c => c.program)
                .Include(c => c.teacher)
                    .ThenInclude(t => t.user)
                .FirstAsync(c => c.id == entity.id);

            return ClassMapper.ToDto(created);
        }

        public async Task<ReadClassDTO?> UpdateAsync(int id, UpdateClassDTO dto)
        {
            var entity = await _context.classes
                .Include(c => c.academic_periods)
                .Include(c => c.program)
                .Include(c => c.teacher)
                    .ThenInclude(t => t.user)
                .FirstOrDefaultAsync(c => c.id == id);
            if (entity == null) return null;

            ClassMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();
            return ClassMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.classes.FindAsync(id);
            if (entity == null) return false;

            _context.classes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClassExistsAsync(int id)
        {
            return await _context.Set<Class>().AnyAsync(m => m.id == id);
        }
    }
}
