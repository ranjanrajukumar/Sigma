using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class AcademicYearTermDto
    {

        public long TermId { get; set; }

        public string TermName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int WorkingDays { get; set; }
    }
}
