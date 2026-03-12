using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Academics
{
    public class UpdateEnquiryDto : CreateEnquiryDto
    {
        public long EnquiryId { get; set; }
        public string AuthLstEdt { get; set; }
    }
}
