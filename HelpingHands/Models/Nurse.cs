using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class Nurse
    {
        public Nurse()
        {
            CareVisits = new HashSet<CareVisit>();
        }

        public long NurseId { get; set; }
       
        public string? UserId { get; set; }
        public bool IsDeleted { get; set; }
        public int? Gender { get; set; }
        public string? Idnumber { get; set; }
        public string Surname { get; set; } = null!;
        public string FirstName { get; set; } = null!;

        public virtual User? User { get; set; }
        public virtual ICollection<CareVisit> CareVisits { get; set; }
    }
}
