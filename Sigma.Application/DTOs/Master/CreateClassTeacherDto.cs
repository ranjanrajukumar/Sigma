using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class CreateClassTeacherDto
    {
        public long AcademicYearId { get; set; }
        public long? SchoolId { get; set; }

        public long ClassId { get; set; }
        public long SectionId { get; set; }
        public long TeacherId { get; set; }

        public bool IsActive { get; set; }

        public string? AuthAdd { get; set; }
    }
}
