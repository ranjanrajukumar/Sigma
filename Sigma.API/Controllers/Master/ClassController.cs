using Microsoft.AspNetCore.Mvc;
using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;

namespace Sigma.API.Controllers.Master
{
    [ApiController]
    [Route("api/master/class")]
    public class ClassController : ControllerBase
    {
        private readonly IMClassService _service;

        public ClassController(IMClassService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound("Class not found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(new { message = result });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateClassDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            return Ok(new { message = result });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(new { message = result });
        }
    }
}