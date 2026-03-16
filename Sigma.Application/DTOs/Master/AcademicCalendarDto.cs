using System;

namespace Sigma.Application.DTOs.Master
{
    public class AcademicCalendarDto
    {
        public long AcademicCalendarId { get; set; }

        public long AcademicYearId { get; set; }
        public string AcademicYearName { get; set; }

        public long? SchoolId { get; set; }

        public long? ClassId { get; set; }
        public string ClassName { get; set; }

        public bool IsAllClasses { get; set; }

        public long EventTypeId { get; set; }
        public string EventTypeName { get; set; }

        public string EventTitle { get; set; }
        public string EventDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsHoliday { get; set; }
    }
}