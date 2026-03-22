using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs
{
    public class EnquiryResponseDto
    {
        public long EnquiryId { get; set; }
        public string? EnquiryNo { get; set; }
        public DateTime? EnquiryDate { get; set; }

        public string? StudentName { get; set; }
        public string? StudentMobile { get; set; }
        public string? StudentEmail { get; set; }

        public string? ParentName { get; set; }
        public string? ParentMobile { get; set; }
        public string? ParentEmail { get; set; }

        public string? PreviousSchool { get; set; }
        public string? Occupation { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }

        public long? DistrictId { get; set; }
        public string? DistrictName { get; set; }   // ✅ IMPORTANT

        public long? StateId { get; set; }
        public string? StateName { get; set; }      // ✅ IMPORTANT

        public string? Pincode { get; set; }

        public string? Source { get; set; }
        public string? Priority { get; set; }

        public long? AssignedTo { get; set; }
        public DateTime? FollowupDate { get; set; }

        public string? Notes { get; set; }

        public bool IsActive { get; set; }

        public string? AuthAdd { get; set; }
        public string? AuthLstEdt { get; set; }

        public DateTime? AddOnDt { get; set; }
        public DateTime? EditOnDt { get; set; }
    }
}
