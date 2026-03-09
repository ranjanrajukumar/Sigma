using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class UpdateSchoolLogoDto
    {
        public IFormFile? Logo { get; set; }
        public string AuthLstEdt { get; set; }
    }
}
