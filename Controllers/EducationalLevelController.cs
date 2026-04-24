using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Models.DTOs.CreateEducationalLevelDTO;
using SIADAL.Models.DTOs.UpdateEducationalLevelDTO;

namespace SIADAL.Controllers
{
    [Route("api/educational_levels")]
    [ApiController]
    public class EducationalLevelController : ControllerBase
    {
        private readonly IEducationalLevel _repository;

        public EducationalLevelController(IEducationalLevel repository)
        {
            _repository = repository;
        }

        [Authorize]
        [HttpGet("")]
        [HttpGet("view_all")]
        public async Task<IActionResult> Get([FromQuery] EducationalLevelQueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var items = await _repository.GetAllAsync(query);
                return Ok(items);
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

        [Authorize(Roles = "admin")]
        [HttpPost("")]
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] CreateEducationalLevelDTO dto)
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

        [Authorize(Roles = "admin")]
        [HttpPut("{id:int}")]
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateEducationalLevelDTO dto)
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

        [Authorize(Roles = "admin")]
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
    }
}
