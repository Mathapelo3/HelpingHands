using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class CareVisit
    {
        public long CareVisitId { get; set; }
        public long ContractId { get; set; }
        public DateTime? VisitDate { get; set; }
        public TimeSpan? ApproxArriveDate { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public TimeSpan? DepartTime { get; set; }
        public string? WoundCondition { get; set; }
        public string? Notes { get; set; }
        public long? NurseId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual CareContract Contract { get; set; } = null!;
        public virtual Nurse? Nurse { get; set; }
    }
}
