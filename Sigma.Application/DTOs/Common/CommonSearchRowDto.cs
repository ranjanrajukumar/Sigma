using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Common
{
    public class CommonSearchRowDto
    {
        public int Id { get; set; }
        public Dictionary<string, string> Columns { get; set; } = new();
    }
}
