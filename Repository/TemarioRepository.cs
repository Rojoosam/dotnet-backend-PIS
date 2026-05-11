using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Interfaces;
using SIADAL.Models;
using SIADAL.Models.DTOs.TemarioDTO;

namespace SIADAL.Repository
{
    public class TemarioRepository : ITemario
    {
        private readonly AppDbContext _context;

        public TemarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReadTemarioDTO>> GetByProgramIdAsync(int programId)
        {
            return await _context.Set<Temario>()
                .AsNoTracking()
                .Where(t => t.program_id == programId)
                .OrderBy(t => t.id)
                .Select(t => new ReadTemarioDTO
                {
                    id = t.id,
                    program_id = t.program_id,
                    name = t.name,
                    has_pdf = t.pdf_file_name != null
                })
                .ToListAsync();
        }

        public async Task<ReadTemarioDTO?> GetByIdAsync(int id)
        {
            return await _context.Set<Temario>()
                .AsNoTracking()
                .Where(t => t.id == id)
                .Select(t => new ReadTemarioDTO
                {
                    id = t.id,
                    program_id = t.program_id,
                    name = t.name,
                    has_pdf = t.pdf_file_name != null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ReadTemarioDTO> CreateAsync(CreateTemarioDTO dto)
        {
            var entity = new Temario
            {
                program_id = dto.program_id,
                name = dto.name
            };
            await _context.Set<Temario>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return new ReadTemarioDTO
            {
                id = entity.id,
                program_id = entity.program_id,
                name = entity.name,
                has_pdf = false
            };
        }

        public async Task<ReadTemarioDTO?> SetPdfAsync(int id, string fileName)
        {
            var entity = await _context.Set<Temario>().FindAsync(id);
            if (entity == null) return null;

            entity.pdf_file_name = fileName;
            await _context.SaveChangesAsync();
            return new ReadTemarioDTO
            {
                id = entity.id,
                program_id = entity.program_id,
                name = entity.name,
                has_pdf = true
            };
        }

        public async Task<string?> GetPdfFileNameAsync(int id)
        {
            var entity = await _context.Set<Temario>().FindAsync(id);
            return entity?.pdf_file_name;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<Temario>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<Temario>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
