using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class CareContract
    {
        public CareContract()
        {
            CareVisits = new HashSet<CareVisit>();
        }

        public long ContractId { get; set; }
        public string ContractNo { get; set; } = null!;
        public DateTime? ContractDate { get; set; }
        public long? PatientId { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public long SuburbId { get; set; }
        public string? WoundDescription { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Nurse { get; set; }
        public string PostalCode { get; set; }
        public long StatusId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Patient? Patient { get; set; }
        public virtual ContractStatus Status { get; set; } = null!;
        public virtual Suburb Suburb { get; set; } = null!;
        public virtual ICollection<CareVisit> CareVisits { get; set; }
    }
}
