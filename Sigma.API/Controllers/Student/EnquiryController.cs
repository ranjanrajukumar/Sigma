using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sigma.Application.DTOs.Academics;
using Sigma.Application.Interfaces.Academics;

namespace Sigma.API.Controllers.Academics
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly IEnquiryRepository _repository;

        public EnquiryController(IEnquiryRepository repository)
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
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEnquiryDto dto)
        {
            var id = await _repository.CreateAsync(dto);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateEnquiryDto dto)
        {
            var result = await _repository.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, [FromQuery] string authDel)
        {
            var result = await _repository.DeleteAsync(id, authDel);
            return Ok(result);
        }
    }
}
