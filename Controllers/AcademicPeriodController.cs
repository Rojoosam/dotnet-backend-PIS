using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Models.DTOs.CreateAcademicPeriodDTO;
using SIADAL.Models.DTOs.UpdateAcademicPeriodDTO;

namespace SIADAL.Controllers
{
    [Route("api/academic_period")]
    [ApiController]
    public class AcademicPeriodController : ControllerBase
    {
        private readonly IAcademicPeriod _repository;

        public AcademicPeriodController(IAcademicPeriod repository)
        {
            _repository = repository;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("")]
        [HttpGet("view_all")]
        public async Task<IActionResult> Get([FromQuery] AcademicPeriodQueryObject query)
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

        [Authorize(Roles = "admin")]
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
        public async Task<IActionResult> Post([FromBody] CreateAcademicPeriodDTO dto)
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
        public async Task<IActionResult> Put(int id, [FromBody] UpdateAcademicPeriodDTO dto)
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
