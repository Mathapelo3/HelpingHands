using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class PreferredSuburb
    {
        public long? NurseId { get; set; }
        public long? SuburbId { get; set; }

        public virtual Nurse? Nurse { get; set; }
        public virtual Suburb? Suburb { get; set; }
    }
}
