using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Domain.Entities.Master
{
    public class EventType
    {
        public long EventTypeId { get; set; }

        public string EventTypeName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
