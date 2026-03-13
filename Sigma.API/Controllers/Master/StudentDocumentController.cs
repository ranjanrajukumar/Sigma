using Microsoft.AspNetCore.Mvc;
using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Services.Master;

namespace Sigma.API.Controllers.Master
{
    [Route("api/student-document")]
    [ApiController]
    public class StudentDocumentController : ControllerBase
    {
        private readonly IStudentDocumentService _service;

        public StudentDocumentController(IStudentDocumentService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateStudentDocumentDto dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}