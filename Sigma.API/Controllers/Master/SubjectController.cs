using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;

namespace Sigma.API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _repository;

        public SubjectController(ISubjectRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            var data = await _repository.GetSubjects();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(long id)
        {
            var data = await _repository.GetSubjectById(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject([FromBody] Subject subject)
        {
            var id = await _repository.CreateSubject(subject);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubject([FromBody] Subject subject)
        {
            var result = await _repository.UpdateSubject(subject);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(long id)
        {
            var result = await _repository.DeleteSubject(id);
            return Ok(result);
        }
    }
}
