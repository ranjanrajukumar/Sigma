using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class UpdateClassTeacherDto
    {
        public long ClassTeacherId { get; set; }

        public long AcademicYearId { get; set; }
        public long? SchoolId { get; set; }

        public long ClassId { get; set; }
        public long SectionId { get; set; }
        public long TeacherId { get; set; }

        public bool IsActive { get; set; }

        public string? AuthLstEdt { get; set; }
    }
}
