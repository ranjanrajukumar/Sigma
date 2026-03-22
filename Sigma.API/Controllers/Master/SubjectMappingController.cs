using Microsoft.AspNetCore.Mvc;
using Sigma.Application.DTOs.Master;
using Sigma.Application.DTOs.Master.Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;

namespace Sigma.API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectMappingController : ControllerBase
    {
        private readonly ISubjectMappingRepository _repository;

        public SubjectMappingController(ISubjectMappingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubjectMappingCreateDto dto)
        {
            try
            {
                var id = await _repository.CreateAsync(dto);
                return Ok(new { id, message = "Created Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] SubjectMappingCreateDto dto)
        {
            var result = await _repository.UpdateAsync(id, dto);
            if (!result) return NotFound();
            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, [FromQuery] string deletedBy)
        {
            var result = await _repository.DeleteAsync(id, deletedBy);
            if (!result) return NotFound();
            return Ok("Deleted Successfully");
        }
    }
}