using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Domain.Entities.Master
{
    public class MClass
    {
        public long ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int ClassOrder { get; set; }

        public string? AuthAdd { get; set; }
        public string? AuthLstEdt { get; set; }
        public string? AuthDel { get; set; }

        public DateTimeOffset? AddOnDt { get; set; }
        public DateTimeOffset? EditOnDt { get; set; }
        public DateTimeOffset? DelOnDt { get; set; }

        public bool? DelStatus { get; set; }
    }
}
