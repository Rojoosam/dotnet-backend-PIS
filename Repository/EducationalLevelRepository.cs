using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Mappers;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateEducationalLevelDTO;
using SIADAL.Models.DTOs.ReadEducationalLevelDTO;
using SIADAL.Models.DTOs.UpdateEducationalLevelDTO;

namespace SIADAL.Repository
{
    public class EducationalLevelRepository : IEducationalLevel
    {
        private readonly AppDbContext _context;

        public EducationalLevelRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadEducationalLevelDTO> BuildQuery(EducationalLevelQueryObject query)
        {
            var queryable = _context.Set<EducationalLevel>()
                .AsNoTracking()
                .Select(m => new ReadEducationalLevelDTO
                {
                    id = m.id,
                    name = m.name
                });

            if (!string.IsNullOrEmpty(query.name))
                queryable = queryable.Where(m => m.name.Contains(query.name));

            return queryable.OrderBy(m => m.name);
        }

        public async Task<List<ReadEducationalLevelDTO>> GetAllAsync(EducationalLevelQueryObject query)
        {
            return await BuildQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadEducationalLevelDTO>> GetAllAsync(EducationalLevelQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadEducationalLevelDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadEducationalLevelDTO?> GetByIdAsync(int id)
        {
            return await _context.Set<EducationalLevel>()
                .Where(m => m.id == id)
                .Select(m => new ReadEducationalLevelDTO
                {
                    id = m.id,
                    name = m.name
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadEducationalLevelDTO> CreateAsync(CreateEducationalLevelDTO dto)
        {
            var entity = EducationalLevelMapper.FromDtoToCreate(dto);
            await _context.Set<EducationalLevel>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return EducationalLevelMapper.ToDto(entity);
        }

        public async Task<ReadEducationalLevelDTO?> UpdateAsync(int id, UpdateEducationalLevelDTO dto)
        {
            var entity = await _context.Set<EducationalLevel>().FindAsync(id);
            if (entity == null) return null;

            EducationalLevelMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();
            return EducationalLevelMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<EducationalLevel>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<EducationalLevel>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
