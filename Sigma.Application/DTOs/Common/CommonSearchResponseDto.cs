using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Common
{
    public class CommonSearchResponseDto
    {

        public string DisplayName { get; set; } = string.Empty;
        public List<string> Headers { get; set; } = new();
        public List<CommonSearchRowDto> Data { get; set; } = new();

    }
}
