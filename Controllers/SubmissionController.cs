using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Models.DTOs.CreateSubmissionDTO;
using SIADAL.Models.DTOs.UpdateSubmissionDTO;

namespace SIADAL.Controllers
{
    [Route("api/submission")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmission _repository;
        private readonly IFileService _fileService;

        public SubmissionController(ISubmission repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        [Authorize]
        [HttpGet("")]
        [HttpGet("view_all")]
        public async Task<IActionResult> Get([FromQuery] SubmissionQueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                if (query.Paginated)
                {
                    var items = await _repository.GetAllAsync(query, query.Page, query.PerPage);
                    return Ok(items);
                }
                else
                {
                    var items = await _repository.GetAllAsync(query);
                    return Ok(items);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet("{id:int}")]
        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var item = await _repository.GetByIdAsync(id);
                if (item == null) return NotFound();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpPost("")]
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] CreateSubmissionDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var item = await _repository.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = item.id }, item);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateSubmissionDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updatedItem = await _repository.UpdateAsync(id, dto);
                if (updatedItem == null) return NotFound();
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var deleted = await _repository.DeleteAsync(id);
                if (!deleted) return NotFound();
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [Authorize(Roles = "student")]
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
                var oldFileName = await _repository.GetFileUrlAsync(id);
                if (oldFileName != null)
                    _fileService.DeleteFile(oldFileName);

                var fileName = await _fileService.SaveFileAsync(file);
                var result = await _repository.SetFileUrlAsync(id, fileName);
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

        [Authorize(Roles = "teacher,admin,student")]
        [HttpGet("{id:int}/pdf")]
        public async Task<IActionResult> GetPdf(int id)
        {
            if (User.IsInRole("student"))
            {
                var claimUserId = User.FindFirst("ID")?.Value;
                if (!int.TryParse(claimUserId, out var userId))
                    return Unauthorized();

                var ownerUserId = await _repository.GetOwnerUserIdAsync(id);
                if (ownerUserId == null) return NotFound();
                if (ownerUserId != userId) return Forbid();
            }

            try
            {
                var fileName = await _repository.GetFileUrlAsync(id);
                if (fileName == null) return NotFound("Esta entrega no tiene PDF.");

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
    }
}
