using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Mappers;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.ClassDTO;
using SIADAL.Models.DTOs.CreateClassDTO;
using SIADAL.Models.DTOs.ReadClassDTO;
using SIADAL.Models.DTOs.UpdateClassDTO;
using System.Text.Json;

namespace SIADAL.Repository
{
    public class ClassRepository : IClass
    {
        private readonly AppDbContext _context;

        public ClassRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadClassRawDTO> BuildClassQuery(ClassQueryObject query)
        {
            var queryable = _context.Set<Class>()
                .AsNoTracking()
                .Select(m => new ReadClassRawDTO
                {
                    id = m.id,
                    course_id = m.course_id,
                    teacher_id = m.teacher_id,
                    academic_term_id = m.academic_term_id,
                    schedule = m.schedule,
                    room = m.room,
                    academic_term_name = m.academic_term.name,
                    course_name = m.course.name,
                    teacher_name = m.teacher.user.first_name + " " + m.teacher.user.last_name
                });

            if (!string.IsNullOrEmpty(query.schedule))
                queryable = queryable.Where(m => m.schedule.Contains(query.schedule));

            if (!string.IsNullOrEmpty(query.room))
                queryable = queryable.Where(m => m.room.Contains(query.room));

            return queryable.OrderBy(m => m.schedule);
        }

        public async Task<List<ReadClassDTO>> GetAllAsync(ClassQueryObject query)
        {
            var rawData = await BuildClassQuery(query).ToListAsync();

            return rawData.Select(m => new ReadClassDTO
            {
                id = m.id,
                course_id = m.course_id,
                teacher_id = m.teacher_id,
                academic_term_id = m.academic_term_id,
                room = m.room,
                academic_term_name = m.academic_term_name,
                course_name = m.course_name,
                teacher_name = m.teacher_name,
                schedule = JsonSerializer.Deserialize<List<ScheduleDTO>>(m.schedule)!
            }).ToList();
        }

        public async Task<PaginatedResultDTO<ReadClassDTO>> GetAllAsync(ClassQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildClassQuery(query);

            var totalItems = await queryable.CountAsync();

            var rawItems = await queryable
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToListAsync();

            var items = rawItems.Select(m => new ReadClassDTO
            {
                id = m.id,
                course_id = m.course_id,
                teacher_id = m.teacher_id,
                academic_term_id = m.academic_term_id,
                room = m.room,
                academic_term_name = m.academic_term_name,
                course_name = m.course_name,
                teacher_name = m.teacher_name,
                schedule = JsonSerializer.Deserialize<List<ScheduleDTO>>(m.schedule)!
            }).ToList();

            return new PaginatedResultDTO<ReadClassDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadClassDTO?> GetByIdAsync(ulong id)
        {
            var raw = await _context.Set<Class>()
                .AsNoTracking()
                .Where(m => m.id == id)
                .Select(m => new ReadClassRawDTO
                {
                    id = m.id,
                    course_id = m.course_id,
                    teacher_id = m.teacher_id,
                    academic_term_id = m.academic_term_id,
                    schedule = m.schedule,
                    room = m.room,
                    academic_term_name = m.academic_term.name,
                    course_name = m.course.name,
                    teacher_name = m.teacher.user.first_name + " " + m.teacher.user.last_name
                })
                .FirstOrDefaultAsync();

            if (raw == null) return null;

            return new ReadClassDTO
            {
                id = raw.id,
                course_id = raw.course_id,
                teacher_id = raw.teacher_id,
                academic_term_id = raw.academic_term_id,
                room = raw.room,
                academic_term_name = raw.academic_term_name,
                course_name = raw.course_name,
                teacher_name = raw.teacher_name,
                schedule = JsonSerializer.Deserialize<List<ScheduleDTO>>(raw.schedule)!
            };
        }

        public async Task<ReadClassDTO> CreateAsync(CreateClassDTO dto)
        {
            var entity = ClassMapper.FromDtoToCreate(dto);
            await _context.Set<Class>().AddAsync(entity);
            await _context.SaveChangesAsync();
            var created = await _context.classes
                .Include(c => c.academic_term)
                .Include(c => c.course)
                .Include(c => c.teacher)
                    .ThenInclude(t => t.user)
                .FirstAsync(c => c.id == entity.id);

            return ClassMapper.ToDto(entity);
        }

        public async Task<ReadClassDTO?> UpdateAsync(ulong id, UpdateClassDTO dto)
        {
            var entity = await _context.classes
                .Include(c => c.academic_term)
                .Include(c => c.course)
                .Include(c => c.teacher)
                    .ThenInclude(t => t.user)
                .FirstOrDefaultAsync(c => c.id == id);
            if (entity == null) return null;

            ClassMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();
            return ClassMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(ulong id)
        {
            var entity = await _context.classes.FindAsync(id);
            if (entity == null) return false;

            _context.classes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClassExistsAsync(ulong id)
        {
            return await _context.Set<Class>().AnyAsync(m => m.id == id);
        }
    }
}
