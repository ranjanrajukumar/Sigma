using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Sigma.Domain.Entities.Master
{
    public class Subject
    {
        public long SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string? SubjectCode { get; set; }
        public bool? IsOptional { get; set; }
        public string SubjectType { get; set; }

        public int MinMarks { get; set; }
        public int MaxMarks { get; set; }
        public int PassMarks { get; set; }

        public string? AuthAdd { get; set; }
        public string? AuthLstEdt { get; set; }
        public string? AuthDel { get; set; }

        public DateTime? AddOnDt { get; set; }
        public DateTime? EditOnDt { get; set; }
        public DateTime? DelOnDt { get; set; }

        public bool? DelStatus { get; set; }
    }
}
