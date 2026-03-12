using Sigma.Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.Interfaces.Master
{
    public interface IEventTypeRepository
    {
        Task<long> CreateEventType(EventType eventType);

        Task<IEnumerable<EventType>> GetAllEventTypes();

        Task<EventType> GetEventTypeById(long id);

        Task<bool> UpdateEventType(EventType eventType);

        Task<bool> DeleteEventType(long id);
    }
}
