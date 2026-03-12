using Microsoft.AspNetCore.Mvc;
using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;

namespace Sigma.API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class MClassTeacherController : ControllerBase
    {
        private readonly IMClassTeacherRepository _repository;

        public MClassTeacherController(IMClassTeacherRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repository.GetAllAsync();

            var result = data.Select(x => new ClassTeacherDto
            {
                ClassTeacherId = x.ClassTeacherId,
                AcademicYearId = x.AcademicYearId,
                SchoolId = x.SchoolId,
                ClassId = x.ClassId,
                SectionId = x.SectionId,
                TeacherId = x.TeacherId,
                IsActive = x.IsActive,
                AddOnDt = x.AddOnDt
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassTeacherDto dto)
        {
            var entity = new MClassTeacher
            {
                AcademicYearId = dto.AcademicYearId,
                SchoolId = dto.SchoolId,
                ClassId = dto.ClassId,
                SectionId = dto.SectionId,
                TeacherId = dto.TeacherId,
                IsActive = dto.IsActive,
                AuthAdd = dto.AuthAdd
            };

            var id = await _repository.CreateAsync(entity);

            return Ok(new { ClassTeacherId = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateClassTeacherDto dto)
        {
            var entity = new MClassTeacher
            {
                ClassTeacherId = id,
                AcademicYearId = dto.AcademicYearId,
                SchoolId = dto.SchoolId,
                ClassId = dto.ClassId,
                SectionId = dto.SectionId,
                TeacherId = dto.TeacherId,
                IsActive = dto.IsActive,
                AuthLstEdt = dto.AuthLstEdt
            };

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