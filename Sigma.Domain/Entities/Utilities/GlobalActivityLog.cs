using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Domain.Entities.Utilities
{
    public class GlobalActivityLog
    {
        public long LogNo { get; set; }
        public string Level { get; set; }
        public string Service { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public object Exception { get; set; }
        public object Request { get; set; }
        public object User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
