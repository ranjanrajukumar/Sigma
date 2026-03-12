using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Application.DTOs.Academics
{
    public class CreateEnquiryDto
    {
        public string EnquiryNo { get; set; }

        public string StudentName { get; set; }
        public string StudentMobile { get; set; }
        public string StudentEmail { get; set; }

        public string ParentName { get; set; }
        public string ParentMobile { get; set; }
        public string ParentEmail { get; set; }

        public string PreviousSchool { get; set; }
        public string Occupation { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public long? DistrictId { get; set; }
        public long? StateId { get; set; }
        public string Pincode { get; set; }

        public string Source { get; set; }
        public string Priority { get; set; }

        public long? AssignedTo { get; set; }
        public DateTime? FollowupDate { get; set; }

        public string Notes { get; set; }

        public string AuthAdd { get; set; }
    }
}
