using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Utilities
{
    public class GlobalActivityLogDto
    {
        public long LogNo { get; set; }
        public string Level { get; set; } = string.Empty;
        public string Service { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        public string? Exception { get; set; }
        public string? Request { get; set; }
        public string? User { get; set; }

        public string? TraceId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
