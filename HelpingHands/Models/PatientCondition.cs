using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class PatientCondition
    {
        public long PatientId { get; set; }
        public int ConditionId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ChronicCondition Condition { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
    }
}
