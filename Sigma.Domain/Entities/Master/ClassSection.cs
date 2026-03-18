using System;

namespace Sigma.Domain.Entities.Master
{
    public class ClassSection
    {
        public long ClassSectionId { get; set; }

        public long ClassId { get; set; }

        public long SectionId { get; set; }

        public string? RoomNumber { get; set; }

        public string? Floor { get; set; }

        public int? SectionCapacity { get; set; }

        public long? ClassTeacherId { get; set; }

        public string? ClassSectionCode { get; set; }

        public string? AuthAdd { get; set; }
        public string? AuthLstEdt { get; set; }
        public string? AuthDel { get; set; }

        public DateTime? AddOnDt { get; set; }
        public DateTime? EditOnDt { get; set; }
        public DateTime? DelOnDt { get; set; }

        public bool DelStatus { get; set; }
    }
}