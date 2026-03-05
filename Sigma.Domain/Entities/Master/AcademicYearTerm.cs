using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Domain.Entities.Master
{
    public class AcademicYearTerm
    {
        public long TermId { get; set; }

        public long AcademicYearId { get; set; }

        public string TermName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int WorkingDays { get; set; }

        public bool DelStatus { get; set; }
    }
}
