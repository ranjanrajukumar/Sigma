using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Domain.Entities.Master
{
    public class SectionLookup
    {
        public long SectionId { get; set; }
        public string SectionName { get; set; }
        public string? AuthAdd { get; set; }
        public string? AuthLstEdt { get; set; }
        public string? AuthDel { get; set; }
        public DateTime? AddOnDt { get; set; }
        public DateTime? EditOnDt { get; set; }
        public DateTime? DelOnDt { get; set; }
        public bool DelStatus { get; set; } = false;
    }
}
