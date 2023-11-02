using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    [Keyless]
    [Table("PreferredSuburb")]
    public partial class PreferredSuburb
    {
        public long? NurseId { get; set; }
        public long? SuburbId { get; set; }

        [ForeignKey("NurseId")]
        public virtual Nurse? Nurse { get; set; }
        [ForeignKey("SuburbId")]
        public virtual Suburb? Suburb { get; set; }
    }
}
