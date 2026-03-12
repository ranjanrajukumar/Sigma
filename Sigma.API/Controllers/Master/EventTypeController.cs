using Microsoft.AspNetCore.Mvc;
using Sigma.Application.DTOs.Master;
using Sigma.Application.Interfaces.Master;
using Sigma.Domain.Entities.Master;

namespace Sigma.API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTypeController : ControllerBase
    {
        private readonly IEventTypeRepository _repository;

        public EventTypeController(IEventTypeRepository repository)
        {
            _repository = repository;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventTypeDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var entity = new EventType
            {
                EventTypeName = dto.EventTypeName,
                Description = dto.Description
            };

            var id = await _repository.CreateEventType(entity);

            if (id == -1)
                return BadRequest("Event Type already exists.");

            return Ok(new
            {
                Message = "Event Type created successfully",
                Id = id
            });
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repository.GetAllEventTypes();

            return Ok(data);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var data = await _repository.GetEventTypeById(id);

            if (data == null)
                return NotFound("Event Type not found.");

            return Ok(data);
        }

        // UPDATE
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEventTypeDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var entity = new EventType
            {
                EventTypeId = dto.EventTypeId,
                EventTypeName = dto.EventTypeName,
                Description = dto.Description,
                IsActive = dto.IsActive
            };

            var result = await _repository.UpdateEventType(entity);

            if (!result)
                return BadRequest("Update failed or duplicate event type.");

            return Ok("Event Type updated successfully.");
        }

        // DELETE (SOFT DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _repository.DeleteEventType(id);

            if (!result)
                return NotFound("Event Type not found.");

            return Ok("Event Type deleted successfully.");
        }
    }
}