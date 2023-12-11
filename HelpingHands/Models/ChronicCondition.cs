using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class ChronicCondition
    {
        public int ConditionId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
