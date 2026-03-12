using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Domain.Entities.Master
{
    public class AcademicCalendar
    {
        public long AcademicCalendarId { get; set; }

        public long AcademicYearId { get; set; }

        public long? SchoolId { get; set; }

        public long? ClassId { get; set; }

        public bool IsAllClasses { get; set; }

        public long EventTypeId { get; set; }

        public string EventTitle { get; set; }

        public string EventDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsHoliday { get; set; }
    }
}