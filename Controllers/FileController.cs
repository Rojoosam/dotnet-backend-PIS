using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIADAL.Interfaces;

namespace SIADAL.Controllers;

[Route("api/files")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    /// <summary>Alumnos suben un archivo y reciben de vuelta el nombre único para guardarlo en la submission.</summary>
    [Authorize]
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No se envió ningún archivo.");

        if (file.Length > 20 * 1024 * 1024)
            return BadRequest("El archivo excede el límite de 20 MB.");

        try
        {
            var fileName = await _fileService.SaveFileAsync(file);
            return Ok(new { file_name = fileName });
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

    /// <summary>Descarga o visualiza un archivo por su nombre único.</summary>
    [Authorize]
    [HttpGet("{fileName}")]
    public IActionResult Download(string fileName)
    {
        try
        {
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
