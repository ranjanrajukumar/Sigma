using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class ClassSectionResponseDto
    {
        public long ClassSectionId { get; set; }
        public long ClassId { get; set; }
       
        public string ClassName { get; set; } = string.Empty;
        public long SectionId { get; set; }
        public string SectionName { get; set; } = string.Empty;
    }
}
