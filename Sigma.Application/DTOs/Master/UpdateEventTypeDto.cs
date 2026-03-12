using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class UpdateEventTypeDto
    {
        public long EventTypeId { get; set; }

        public string EventTypeName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

    }
}
