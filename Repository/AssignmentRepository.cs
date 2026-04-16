using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Mappers;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateAssignmentDTO;
using SIADAL.Models.DTOs.ReadAssignmentDTO;
using SIADAL.Models.DTOs.UpdateAssignmentDTO;

namespace SIADAL.Repository
{
    public class AssignmentRepository : IAssignment
    {
        private readonly AppDbContext _context;

        public AssignmentRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadAssignmentDTO> BuildQuery(AssignmentQueryObject query)
        {
            var queryable = _context.Set<Assignment>()
                .AsNoTracking()
                .Select(m => new ReadAssignmentDTO
                {
                    id = m.id,
                    class_id = m.class_id,
                    class_name = m._class.name,
                    name = m.name,
                    duedate = m.duedate,
                    points = m.points,
                    details = m.details
                });

            if (!string.IsNullOrEmpty(query.name))
                queryable = queryable.Where(m => m.name.Contains(query.name));

            if (query.class_id.HasValue)
                queryable = queryable.Where(m => m.class_id == query.class_id.Value);

            return queryable.OrderBy(m => m.name);
        }

        public async Task<List<ReadAssignmentDTO>> GetAllAsync(AssignmentQueryObject query)
        {
            return await BuildQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadAssignmentDTO>> GetAllAsync(AssignmentQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadAssignmentDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadAssignmentDTO?> GetByIdAsync(int id)
        {
            return await _context.Set<Assignment>()
                .Where(m => m.id == id)
                .Select(m => new ReadAssignmentDTO
                {
                    id = m.id,
                    class_id = m.class_id,
                    class_name = m._class.name,
                    name = m.name,
                    duedate = m.duedate,
                    points = m.points,
                    details = m.details
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadAssignmentDTO> CreateAsync(CreateAssignmentDTO dto)
        {
            var entity = AssignmentMapper.FromDtoToCreate(dto);
            await _context.Set<Assignment>().AddAsync(entity);
            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(a => a._class).LoadAsync();
            return AssignmentMapper.ToDto(entity);
        }

        public async Task<ReadAssignmentDTO?> UpdateAsync(int id, UpdateAssignmentDTO dto)
        {
            var entity = await _context.Set<Assignment>()
                .Include(a => a._class)
                .FirstOrDefaultAsync(a => a.id == id);
            if (entity == null) return null;

            AssignmentMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();

            if (dto.class_id.HasValue)
                await _context.Entry(entity).Reference(a => a._class).LoadAsync();

            return AssignmentMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<Assignment>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<Assignment>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
