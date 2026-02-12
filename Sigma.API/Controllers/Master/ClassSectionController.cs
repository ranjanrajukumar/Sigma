using Microsoft.AspNetCore.Mvc;
using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;

namespace Sigma.API.Controllers.Master
{
    [ApiController]
    [Route("api/master/class-section")]
    public class ClassSectionController : ControllerBase
    {
        private readonly IClassSectionService _service;

        public ClassSectionController(IClassSectionService service)
        {
            _service = service;
        }

        // 🔹 GET ALL WITH NAMES (JOIN)
        [HttpGet("with-names")]
        public async Task<IActionResult> GetAllWithNames()
        {
            var result = await _service.GetAllWithNamesAsync();
            return Ok(result);
        }

        // 🔹 GET BY ID WITH NAMES (JOIN)
        [HttpGet("with-names/{id:long}")]
        public async Task<IActionResult> GetByIdWithNames(long id)
        {
            var result = await _service.GetByIdWithNamesAsync(id);

            if (result == null)
                return NotFound("Class-Section mapping not found.");

            return Ok(result);
        }

        // 🔹 CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassSectionCreateDto dto)
        {
            var id = await _service.CreateAsync(dto);

            return Ok(new
            {
                message = "Class-Section mapped successfully",
                classSectionId = id
            });
        }

        // 🔹 UPDATE
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClassSectionUpdateDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok("Mapping updated successfully.");
        }

        // 🔹 SOFT DELETE
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id, [FromQuery] string deletedBy)
        {
            await _service.DeleteAsync(id, deletedBy);
            return Ok("Mapping deleted successfully.");
        }
    }
}
