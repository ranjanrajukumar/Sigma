using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class ClassSectionUpdateDto
    {
        public long ClassSectionId { get; set; }
        public long ClassId { get; set; }
        public long SectionId { get; set; }
        public string? AuthLstEdt { get; set; }
    }
}
