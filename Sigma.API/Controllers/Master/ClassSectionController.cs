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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllWithNamesAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClassSectionCreateDto dto)
        {
            var id = await _service.CreateAsync(dto);

            return Ok(new
            {
                message = "Class Section created",
                classSectionId = id
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(ClassSectionUpdateDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, [FromQuery] string deletedBy)
        {
            await _service.DeleteAsync(id, deletedBy);
            return Ok("Deleted successfully");
        }
    }
}