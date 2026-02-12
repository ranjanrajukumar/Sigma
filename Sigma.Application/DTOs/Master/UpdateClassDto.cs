using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class UpdateClassDto
    {
        public long ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int ClassOrder { get; set; }
    }
}
