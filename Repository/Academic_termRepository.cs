using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Interfaces;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateAcademic_termDTO;
using SIADAL.Models.DTOs.ReadAcademic_termDTO;
using SIADAL.Models.DTOs.UpdateAcademic_termDTO;
using SIADAL.Mappers;
using SIADAL.Helpers;

namespace SIADAL.Repository
{
    public class Academic_termRepository : IAcademic_term
    {
        private readonly AppDbContext _context;

        public Academic_termRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadAcademic_termDTO> BuildAcademic_termQuery(Academic_termQueryObject query)
        {
            var queryable = _context.Set<Academic_term>()
                .AsNoTracking()
                .Select(m => new ReadAcademic_termDTO
                {
                    id = m.id,
                    name = m.name,
                    start_date = m.start_date,
                    end_date = m.end_date,
                    is_active = m.is_active
                });

            if (!string.IsNullOrEmpty(query.name))
                queryable = queryable.Where(m => m.name.Contains(query.name));

            return queryable.OrderBy(m => m.name);
        }

        public async Task<List<ReadAcademic_termDTO>> GetAllAsync(Academic_termQueryObject query)
        {
            return await BuildAcademic_termQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadAcademic_termDTO>> GetAllAsync(Academic_termQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildAcademic_termQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadAcademic_termDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadAcademic_termDTO?> GetByIdAsync(ulong id)
        {
            return await _context.Set<Academic_term>()
                .Where(m => m.id == id)
                .Select(m => new ReadAcademic_termDTO
                {
                    id = m.id,
                    name = m.name,
                    start_date = m.start_date,
                    end_date = m.end_date,
                    is_active = m.is_active
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadAcademic_termDTO> CreateAsync(CreateAcademic_termDTO dto)
        {
            var entity = Academic_termMapper.FromDtoToCreate(dto);
            await _context.Set<Academic_term>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return Academic_termMapper.ToDto(entity);
        }

        public async Task<ReadAcademic_termDTO?> UpdateAsync(ulong id, UpdateAcademic_termDTO dto)
        {
            var entity = await _context.Set<Academic_term>().FindAsync(id);
            if (entity == null) return null;

            Academic_termMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();
            return Academic_termMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(ulong id)
        {
            var entity = await _context.Set<Academic_term>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<Academic_term>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Academic_termExistsAsync(ulong id)
        {
            return await _context.Set<Academic_term>().AnyAsync(m => m.id == id);
        }
    }
}
