using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIADAL.Interfaces;
using SIADAL.Models.DTOs.TemarioDTO;

namespace SIADAL.Controllers
{
    [Route("api/temario")]
    [ApiController]
    public class TemarioController : ControllerBase
    {
        private readonly ITemario _repository;
        private readonly IFileService _fileService;

        public TemarioController(ITemario repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        [Authorize]
        [HttpGet("by-program/{programId:int}")]
        public async Task<IActionResult> GetByProgram(int programId)
        {
            try
            {
                var items = await _repository.GetByProgramIdAsync(programId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("")]
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] CreateTemarioDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var item = await _repository.CreateAsync(dto);
                return CreatedAtAction(nameof(GetByProgram), new { programId = item.program_id }, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{id:int}/pdf")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadPdf(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No se envió ningún archivo.");

            if (!Path.GetExtension(file.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Solo se permiten archivos PDF.");

            if (file.Length > 20 * 1024 * 1024)
                return BadRequest("El archivo excede el límite de 20 MB.");

            try
            {
                var oldFileName = await _repository.GetPdfFileNameAsync(id);
                if (oldFileName != null)
                    _fileService.DeleteFile(oldFileName);

                var fileName = await _fileService.SaveFileAsync(file);
                var result = await _repository.SetPdfAsync(id, fileName);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("{id:int}/pdf")]
        public async Task<IActionResult> GetPdf(int id)
        {
            try
            {
                var fileName = await _repository.GetPdfFileNameAsync(id);
                if (fileName == null) return NotFound("Este temario no tiene PDF.");

                var (stream, contentType, safeName) = _fileService.GetFile(fileName);
                return File(stream, contentType, safeName);
            }
            catch (FileNotFoundException)
            {
                return NotFound("Archivo no encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var fileName = await _repository.GetPdfFileNameAsync(id);
                var deleted = await _repository.DeleteAsync(id);
                if (!deleted) return NotFound();

                if (fileName != null)
                    _fileService.DeleteFile(fileName);

                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
