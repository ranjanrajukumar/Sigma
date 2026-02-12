using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;

namespace Sigma.API.Controllers.Master
{
    [ApiController]
    [Route("api/master/section")]
    public class SectionLookupController : ControllerBase
    {
        private readonly ISectionLookupService _service;

        public SectionLookupController(ISectionLookupService service)
        {
            _service = service;
        }

        // ✅ 1️⃣ Get All
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // ✅ 2️⃣ Get By Id
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound("Section not found.");

            return Ok(result);
        }

        // ✅ 3️⃣ Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SectionLookupCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = id },
                new { message = "Section created successfully", sectionId = id }
            );
        }

        // ✅ 4️⃣ Update
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SectionLookupUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateAsync(dto);

            if (!result)
                return NotFound("Section not found.");

            return Ok("Section updated successfully.");
        }

        // ✅ 5️⃣ Soft Delete
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id, [FromQuery] string deletedBy)
        {
            if (string.IsNullOrEmpty(deletedBy))
                return BadRequest("deletedBy is required.");

            var result = await _service.DeleteAsync(id, deletedBy);

            if (!result)
                return NotFound("Section not found.");

            return Ok("Section deleted successfully.");
        }
    }
}
