using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class SubjectDTO
    {
        public long SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string? SubjectCode { get; set; }
        public bool? IsOptional { get; set; }
        public string SubjectType { get; set; }
    }
}
