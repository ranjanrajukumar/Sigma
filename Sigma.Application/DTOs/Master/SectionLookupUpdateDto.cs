using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class SectionLookupUpdateDto
    {
        public long SectionId { get; set; }
        public string SectionName { get; set; }
        public string? AuthLstEdt { get; set; }
    }
}
