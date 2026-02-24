using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Interfaces;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateActivityDTO;
using SIADAL.Models.DTOs.ReadActivityDTO;
using SIADAL.Models.DTOs.UpdateActivityDTO;
using SIADAL.Mappers;
using SIADAL.Helpers;

namespace SIADAL.Repository
{
    public class ActivityRepository : IActivity
    {
        private readonly AppDbContext _context;

        public ActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadActivityDTO> BuildActivityQuery(ActivityQueryObject query)
        {
            var queryable = _context.Set<Activity>()
                .AsNoTracking()
                .Include(a => a.student)
                .Include(a => a._class)
                .Select(m => new ReadActivityDTO
                {
                    id          = m.id,
                    class_id    = m.class_id,
                    student_id  = m.student_id,
                    name        = m.name,
                    grade       = m.grade,
                    porcentage  = m.porcentage,
                    status      = m.status
                });

            if (!string.IsNullOrEmpty(query.name))
                queryable = queryable.Where(m => m.name.Contains(query.name));

            if (query.grade.HasValue)
                queryable = queryable.Where(m => m.grade == query.grade.Value);
            
            if (query.porcentage.HasValue)
                queryable = queryable.Where(m => m.porcentage == query.porcentage.Value);

            if (!string.IsNullOrEmpty(query.status))
                queryable = queryable.Where(m => m.status.Contains(query.status));

            return queryable.OrderBy(m => m.name);
        }

        public async Task<List<ReadActivityDTO>> GetAllAsync(ActivityQueryObject query)
        {
            return await BuildActivityQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadActivityDTO>> GetAllAsync(ActivityQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildActivityQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadActivityDTO>
            {
                Items       = items,
                CurrentPage = page,
                PerPage     = perPage,
                TotalItems  = totalItems,
                TotalPages  = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadActivityDTO?> GetByIdAsync(ulong id)
        {
            return await _context.Set<Activity>()
                .Include(a => a.student)
                .Include(a => a._class)
                .Where(m => m.id == id)
                .Select(m => new ReadActivityDTO
                {
                    id          = m.id,
                    class_id    = m.class_id,
                    student_id  = m.student_id,
                    name        = m.name,
                    grade       = m.grade,
                    porcentage  = m.porcentage,
                    status      = m.status
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadActivityDTO> CreateAsync(CreateActivityDTO dto)
        {
            var entity = ActivityMapper.FromDtoToCreate(dto);
            await _context.Set<Activity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return ActivityMapper.ToDto(entity);
        }

        public async Task<ReadActivityDTO?> UpdateAsync(ulong id, UpdateActivityDTO dto)
        {
            var entity = await _context.Set<Activity>().FindAsync(id);
            if (entity == null) return null;

            ActivityMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();
            return ActivityMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(ulong id)
        {
            var entity = await _context.Set<Activity>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<Activity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActivityExistsAsync(ulong id)
        {
            return await _context.Set<Activity>().AnyAsync(m => m.id == id);
        }
    }
}
