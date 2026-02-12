using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class ClassSectionCreateDto
    {
        public long ClassId { get; set; }
        public long SectionId { get; set; }
        public string? AuthAdd { get; set; }
    }
}
