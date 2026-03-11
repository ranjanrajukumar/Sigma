using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class SubjectMappingDto
    {
        public long? AcademicYearId { get; set; }
        public long? SchoolId { get; set; }

        public long ClassId { get; set; }
        public long? SectionId { get; set; }
        public bool IsAllSections { get; set; }

        public long? TermId { get; set; }

        public long SubjectId { get; set; }

        public int? PeriodsPerWeek { get; set; }
        public string? SubjectType { get; set; }

        public string? AuthAdd { get; set; }

    }
}
