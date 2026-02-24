using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIADAL.Helpers;
using SIADAL.Interfaces;
using SIADAL.Models.DTOs.CreateRoleDTO;
using SIADAL.Models.DTOs.UpdateRoleDTO;

namespace SIADAL.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _repository;

        public RoleController(IRole repository)
        {
            _repository = repository;
        }

        [Authorize]
        [HttpGet("")]
        [HttpGet("view_all")]
        public async Task<IActionResult> Get([FromQuery] RoleQueryObject query)
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
        public async Task<IActionResult> Get(ulong id)
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
        public async Task<IActionResult> Post([FromBody] CreateRoleDTO dto)
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
        public async Task<IActionResult> Put(ulong id, [FromBody] UpdateRoleDTO dto)
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
        public async Task<IActionResult> Delete(ulong id)
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

