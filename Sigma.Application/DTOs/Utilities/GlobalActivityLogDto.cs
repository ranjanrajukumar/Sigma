using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Utilities
{
    public class GlobalActivityLogDto
    {
        public string Level { get; set; }
        public string Service { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public object Exception { get; set; }
        public object Request { get; set; }
        public object User { get; set; }
    }
}
