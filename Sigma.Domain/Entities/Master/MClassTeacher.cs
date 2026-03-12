using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Domain.Entities.Master
{
    public class MClassTeacher
    {
        public long ClassTeacherId { get; set; }
        public long AcademicYearId { get; set; }
        public long? SchoolId { get; set; }

        public long ClassId { get; set; }
        public long SectionId { get; set; }
        public long TeacherId { get; set; }

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