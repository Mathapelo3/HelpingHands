using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    public partial class ChronicCondition
    {
        [Key]
        public long ConditionId { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
