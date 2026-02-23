using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Interfaces;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateCourseDTO;
using SIADAL.Models.DTOs.ReadCourseDTO;
using SIADAL.Models.DTOs.UpdateCourseDTO;
using SIADAL.Mappers;
using SIADAL.Helpers;

namespace SIADAL.Repository
{
    public class CourseRepository : ICourse
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadCourseDTO> BuildCourseQuery(CourseQueryObject query)
        {
            var queryable = _context.Set<Course>()
                .AsNoTracking()
                .Select(m => new ReadCourseDTO
                {
                    id = m.id,
                    name = m.name,
                    code = m.code,
                    credits = m.credits,
                    desciption = m.desciption

                });

            if (!string.IsNullOrEmpty(query.name))
                queryable = queryable.Where(m => m.name.Contains(query.name));

            if (!string.IsNullOrEmpty(query.code))
                queryable = queryable.Where(m => m.code.Contains(query.code));

            if (query.credits.HasValue)
                queryable = queryable.Where(m => m.credits == query.credits.Value);

            if (!string.IsNullOrEmpty(query.desciption))
                queryable = queryable.Where(m => m.desciption.Contains(query.desciption));

            return queryable.OrderBy(m => m.name);
        }

        public async Task<List<ReadCourseDTO>> GetAllAsync(CourseQueryObject query)
        {
            return await BuildCourseQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadCourseDTO>> GetAllAsync(CourseQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildCourseQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadCourseDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadCourseDTO?> GetByIdAsync(ulong id)
        {
            return await _context.Set<Course>()
                .Where(m => m.id == id)
                .Select(m => new ReadCourseDTO
                {
                    id = m.id,
                    name = m.name,
                    code = m.code,
                    credits = m.credits,
                    desciption = m.desciption
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadCourseDTO> CreateAsync(CreateCourseDTO dto)
        {
            var entity = CourseMapper.FromDtoToCreate(dto);
            await _context.Set<Course>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return CourseMapper.ToDto(entity);
        }

        public async Task<ReadCourseDTO?> UpdateAsync(ulong id, UpdateCourseDTO dto)
        {
            var entity = await _context.Set<Course>().FindAsync(id);
            if (entity == null) return null;

            CourseMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();
            return CourseMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(ulong id)
        {
            var entity = await _context.Set<Course>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<Course>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CourseExistsAsync(ulong id)
        {
            return await _context.Set<Course>().AnyAsync(m => m.id == id);
        }
    }
}
