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

        public string? RoomNumber { get; set; }

        public string? Floor { get; set; }

        public int? SectionCapacity { get; set; }

        public long? ClassTeacherId { get; set; }

        public string? ClassSectionCode { get; set; }
    }
}
