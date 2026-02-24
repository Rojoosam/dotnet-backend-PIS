using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Interfaces;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateRoleDTO;
using SIADAL.Models.DTOs.ReadRoleDTO;
using SIADAL.Models.DTOs.UpdateRoleDTO;
using SIADAL.Mappers;
using SIADAL.Helpers;

namespace SIADAL.Repository
{
    public class RoleRepository : IRole
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadRoleDTO> BuildRoleQuery(RoleQueryObject query)
        {
            var queryable = _context.Set<Role>()
                .AsNoTracking()
                .Select(m => new ReadRoleDTO
                {
                    id          = m.id,
                    name        = m.name,
                    created_at  = m.created_at
                });

            if (!string.IsNullOrEmpty(query.name))
                queryable = queryable.Where(m => m.name.Contains(query.name));

            return queryable.OrderBy(m => m.name);
        }

        public async Task<List<ReadRoleDTO>> GetAllAsync(RoleQueryObject query)
        {
            return await BuildRoleQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadRoleDTO>> GetAllAsync(RoleQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildRoleQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadRoleDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadRoleDTO?> GetByIdAsync(ulong id)
        {
            return await _context.Set<Role>()
                .Where(m => m.id == id)
                .Select(m => new ReadRoleDTO
                {
                    id          = m.id,
                    name        = m.name,
                    created_at  = m.created_at
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadRoleDTO> CreateAsync(CreateRoleDTO dto)
        {
            var entity = RoleMapper.FromDtoToCreate(dto);
            await _context.Set<Role>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return RoleMapper.ToDto(entity);
        }

        public async Task<ReadRoleDTO?> UpdateAsync(ulong id, UpdateRoleDTO dto)
        {
            var entity = await _context.Set<Role>().FindAsync(id);
            if (entity == null) return null;

            RoleMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();
            return RoleMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(ulong id)
        {
            var entity = await _context.Set<Role>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<Role>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RoleExistsAsync(ulong id)
        {
            return await _context.Set<Role>().AnyAsync(m => m.id == id);
        }
    }
}
