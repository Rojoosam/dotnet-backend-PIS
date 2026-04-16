using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Mappers;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateProgramDTO;
using SIADAL.Models.DTOs.ReadProgramDTO;
using SIADAL.Models.DTOs.UpdateProgramDTO;

namespace SIADAL.Repository
{
    public class ProgramRepository : IProgram
    {
        private readonly AppDbContext _context;

        public ProgramRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadProgramDTO> BuildQuery(ProgramQueryObject query)
        {
            var queryable = _context.Set<Models.Program>()
                .AsNoTracking()
                .Select(m => new ReadProgramDTO
                {
                    id = m.id,
                    name = m.name,
                    level_id = m.level_id,
                    level_name = m.educational_level.name
                });

            if (!string.IsNullOrEmpty(query.name))
                queryable = queryable.Where(m => m.name.Contains(query.name));

            if (query.level_id.HasValue)
                queryable = queryable.Where(m => m.level_id == query.level_id.Value);

            return queryable.OrderBy(m => m.name);
        }

        public async Task<List<ReadProgramDTO>> GetAllAsync(ProgramQueryObject query)
        {
            return await BuildQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadProgramDTO>> GetAllAsync(ProgramQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadProgramDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadProgramDTO?> GetByIdAsync(int id)
        {
            return await _context.Set<Models.Program>()
                .Where(m => m.id == id)
                .Select(m => new ReadProgramDTO
                {
                    id = m.id,
                    name = m.name,
                    level_id = m.level_id,
                    level_name = m.educational_level.name
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadProgramDTO> CreateAsync(CreateProgramDTO dto)
        {
            var entity = ProgramMapper.FromDtoToCreate(dto);
            await _context.Set<Models.Program>().AddAsync(entity);
            await _context.SaveChangesAsync();

            await _context.Entry(entity).Reference(p => p.educational_level).LoadAsync();
            return ProgramMapper.ToDto(entity);
        }

        public async Task<ReadProgramDTO?> UpdateAsync(int id, UpdateProgramDTO dto)
        {
            var entity = await _context.Set<Models.Program>()
                .Include(p => p.educational_level)
                .FirstOrDefaultAsync(p => p.id == id);
            if (entity == null) return null;

            ProgramMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();

            if (dto.level_id.HasValue)
                await _context.Entry(entity).Reference(p => p.educational_level).LoadAsync();

            return ProgramMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<Models.Program>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<Models.Program>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
