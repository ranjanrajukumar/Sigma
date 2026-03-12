using Microsoft.AspNetCore.Mvc;
using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;

namespace Sigma.API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicCalendarController : ControllerBase
    {
        private readonly IAcademicCalendarRepository _repository;

        public AcademicCalendarController(IAcademicCalendarRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAcademicCalendarDto dto)
        {
            var entity = new AcademicCalendar
            {
                AcademicYearId = dto.AcademicYearId,
                SchoolId = dto.SchoolId,
                ClassId = dto.ClassId,
                IsAllClasses = dto.IsAllClasses,
                EventTypeId = dto.EventTypeId,
                EventTitle = dto.EventTitle,
                EventDescription = dto.EventDescription,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsHoliday = dto.IsHoliday
            };

            var id = await _repository.CreateAcademicCalendar(entity);

            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repository.GetAllAcademicCalendars();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _repository.GetAcademicCalendarById(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAcademicCalendarDto dto)
        {
            var entity = new AcademicCalendar
            {
                AcademicCalendarId = dto.AcademicCalendarId,
                AcademicYearId = dto.AcademicYearId,
                SchoolId = dto.SchoolId,
                ClassId = dto.ClassId,
                IsAllClasses = dto.IsAllClasses,
                EventTypeId = dto.EventTypeId,
                EventTitle = dto.EventTitle,
                EventDescription = dto.EventDescription,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsHoliday = dto.IsHoliday
            };

            var result = await _repository.UpdateAcademicCalendar(entity);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _repository.DeleteAcademicCalendar(id);

            return Ok(result);
        }
    }
}