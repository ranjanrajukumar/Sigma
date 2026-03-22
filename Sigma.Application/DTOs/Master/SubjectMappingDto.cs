using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    namespace Sigma.Application.DTOs.Master
    {
        public class SubjectMappingCreateDto
        {
            public long AcademicYearId { get; set; }
            public long SchoolId { get; set; }
            public long ClassId { get; set; }
            public long? SectionId { get; set; }
            public bool IsAllSections { get; set; }
            public long TermId { get; set; }
            public long SubjectId { get; set; }
            public int PeriodsPerWeek { get; set; }
            public string SubjectType { get; set; } 
            public string AuthAdd { get; set; } 
        }

        public class SubjectMappingResponseDto
        {
            public long SubjectMappingId { get; set; }

            public long AcademicYearId { get; set; }
            public string? AcademicYearName { get; set; }

            public long SchoolId { get; set; }
            public string? SchoolName { get; set; }

            public long ClassId { get; set; }
            public string? ClassName { get; set; }

            public long? SectionId { get; set; }
            public string? SectionName { get; set; }

            public bool IsAllSections { get; set; }

            public long TermId { get; set; }
            public string? TermName { get; set; }

            public long SubjectId { get; set; }
            public string? SubjectName { get; set; }

            public int PeriodsPerWeek { get; set; }
            public string? SubjectType { get; set; }

            public bool IsActive { get; set; }

            public string? AuthAdd { get; set; }
            public string? AuthLstEdt { get; set; }
            public string? AuthDel { get; set; }

            public DateTime? AddOnDt { get; set; }
            public DateTime? EditOnDt { get; set; }
            public DateTime? DelOnDt { get; set; }

            public bool DelStatus { get; set; }
        }
    }
}