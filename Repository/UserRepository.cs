using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Mappers;
using SIADAL.Models;
using SIADAL.Models.DTOs;
using SIADAL.Models.DTOs.CreateUserDTO;
using SIADAL.Models.DTOs.ReadUserDTO;
using SIADAL.Models.DTOs.UpdateUserDTO;

namespace SIADAL.Repository
{
    public class UserRepository : IUser
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<ReadUserDTO> BuildQuery(UserQueryObject query)
        {
            var queryable = _context.Set<User>()
                .AsNoTracking()
                .Select(m => new ReadUserDTO
                {
                    id = m.id,
                    email = m.email,
                    first_name = m.first_name,
                    last_name = m.last_name,
                    is_active = m.is_active,
                    created_at = m.created_at
                });

            if (!string.IsNullOrEmpty(query.email))
                queryable = queryable.Where(m => m.email.Contains(query.email));

            if (!string.IsNullOrEmpty(query.first_name))
                queryable = queryable.Where(m => m.first_name.Contains(query.first_name));

            if (!string.IsNullOrEmpty(query.last_name))
                queryable = queryable.Where(m => m.last_name.Contains(query.last_name));

            if (query.is_active.HasValue)
                queryable = queryable.Where(m => m.is_active == query.is_active.Value);

            return queryable.OrderBy(m => m.last_name);
        }

        public async Task<List<ReadUserDTO>> GetAllAsync(UserQueryObject query)
        {
            return await BuildQuery(query).ToListAsync();
        }

        public async Task<PaginatedResultDTO<ReadUserDTO>> GetAllAsync(UserQueryObject query, int page = 1, int perPage = 10)
        {
            var queryable = BuildQuery(query);
            var totalItems = await queryable.CountAsync();
            var items = await queryable.Skip((page - 1) * perPage).Take(perPage).ToListAsync();

            return new PaginatedResultDTO<ReadUserDTO>
            {
                Items = items,
                CurrentPage = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)perPage)
            };
        }

        public async Task<ReadUserDTO?> GetByIdAsync(int id)
        {
            return await _context.Set<User>()
                .Where(m => m.id == id)
                .Select(m => new ReadUserDTO
                {
                    id = m.id,
                    email = m.email,
                    first_name = m.first_name,
                    last_name = m.last_name,
                    is_active = m.is_active,
                    created_at = m.created_at
                }).FirstOrDefaultAsync();
        }

        public async Task<ReadUserDTO> CreateAsync(CreateUserDTO dto)
        {
            var entity = UserMapper.FromDtoToCreate(dto);
            await _context.Set<User>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return UserMapper.ToDto(entity);
        }

        public async Task<ReadUserDTO?> UpdateAsync(int id, UpdateUserDTO dto)
        {
            var entity = await _context.Set<User>().FindAsync(id);
            if (entity == null) return null;

            UserMapper.FromDtoToUpdate(entity, dto);
            await _context.SaveChangesAsync();
            return UserMapper.ToDto(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<User>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<User>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Set<User>().AnyAsync(m => m.id == id);
        }
    }
}
