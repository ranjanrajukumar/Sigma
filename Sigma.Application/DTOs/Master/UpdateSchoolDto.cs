using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class UpdateSchoolDto
    {
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
        public string? PrincipalName { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public IFormFile? Logo { get; set; }

        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }
}
