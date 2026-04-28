using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Mappers;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateAcademicPeriodDTO;
using SIADAL.Models.DTOs.ReadAcademicPeriodDTO;
using SIADAL.Models.DTOs.UpdateAcademicPeriodDTO;

namespace SIADAL.Repository
{
    public class AcademicPeriodRepository : IAcademicPeriod
    {
        private readonly AppDbContext _context;

        public AcademicPeriodRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadAcademicPeriodDTO> BuildQuery(AcademicPeriodQueryObject query)
        {
            var queryable = _context.Set<AcademicPeriod>()
                .AsNoTracking()
                .Select(m => new ReadAcademicPeriodDTO
                {
                    id = m.id,
                    name = m.name,
                    start_date = m.start_date,
                    end_date = m.end_date,
                    is_active = m.is_active
                });

            if (!string.IsNullOrEmpty(query.name))
                queryable = queryable.Where(m => m.name.Contains(query.name));

            if (query.is_active.HasValue)
                queryable = queryable.Where(m => m.is_active == query.is_active.Value);

            return queryable.OrderBy(m => m.name);
        }

        public async Task<List<ReadAcademicPeriodDTO>> GetAllAsync(AcademicPeriodQueryObject query)
        {
            return await BuildQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadAcademicPeriodDTO>> GetAllAsync(AcademicPeriodQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadAcademicPeriodDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadAcademicPeriodDTO?> GetByIdAsync(int id)
        {
            return await _context.Set<AcademicPeriod>()
                .Where(m => m.id == id)
                .Select(m => new ReadAcademicPeriodDTO
                {
                    id = m.id,
                    name = m.name,
                    start_date = m.start_date,
                    end_date = m.end_date,
                    is_active = m.is_active
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadAcademicPeriodDTO> CreateAsync(CreateAcademicPeriodDTO dto)
        {
            var entity = AcademicPeriodMapper.FromDtoToCreate(dto);
            await _context.Set<AcademicPeriod>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return AcademicPeriodMapper.ToDto(entity);
        }

        public async Task<ReadAcademicPeriodDTO?> UpdateAsync(int id, UpdateAcademicPeriodDTO dto)
        {
            var entity = await _context.Set<AcademicPeriod>().FindAsync(id);
            if (entity == null) return null;

            AcademicPeriodMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();
            return AcademicPeriodMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<AcademicPeriod>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<AcademicPeriod>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
