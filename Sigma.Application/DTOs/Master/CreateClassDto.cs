using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class CreateClassDto
    {
        public string ClassName { get; set; } = string.Empty;
        public int ClassOrder { get; set; }
    }
}
