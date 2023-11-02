using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    [Keyless]
    public partial class PatientCondition
    {
        public long PatientId { get; set; }
        public long ConditionId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ConditionId")]
        public virtual ChronicCondition Condition { get; set; } = null!;
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; } = null!;
    }
}
