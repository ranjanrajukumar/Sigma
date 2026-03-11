using Microsoft.AspNetCore.Mvc;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;

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
            var data = await _repository.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var data = await _repository.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubjectMapping entity)
        {
            var id = await _repository.CreateAsync(entity);

            return Ok(new
            {
                SubjectMappingId = id,
                Message = "Created Successfully"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SubjectMapping entity)
        {
            var result = await _repository.UpdateAsync(entity);

            if (!result)
                return NotFound();

            return Ok("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, [FromQuery] string deletedBy)
        {
            var result = await _repository.DeleteAsync(id, deletedBy);

            if (!result)
                return NotFound();

            return Ok("Deleted Successfully");
        }
    }
}