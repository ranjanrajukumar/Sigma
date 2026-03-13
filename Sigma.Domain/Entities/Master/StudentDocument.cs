using System;
using System.Collections.Generic;
using System.Text;

namespace Sigma.Domain.Entities.Master
{
    public class StudentDocument
    {
        public long StudentDocumentId { get; set; }

        public string DocumentName { get; set; }
        public string DocumentCode { get; set; }

        public string Description { get; set; }

        public bool IsMandatory { get; set; }
        public bool IsActive { get; set; }

        public string AuthAdd { get; set; }
        public string AuthLstEdt { get; set; }
        public string AuthDel { get; set; }

        public DateTime AddOnDt { get; set; }
        public DateTime? EditOnDt { get; set; }
        public DateTime? DelOnDt { get; set; }

        public bool DelStatus { get; set; }
    }
}
