using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Master
{
    public class CreateStudentDocumentDto
    {
        public string DocumentName { get; set; }
        public string DocumentCode { get; set; }
        public string Description { get; set; }

        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }
    }
}
