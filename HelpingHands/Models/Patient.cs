using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class Patient
    {
        public Patient()
        {
            CareContracts = new HashSet<CareContract>();
        }

        public long PatientId { get; set; }
        public byte[] Image { get; set; }
        public string? EmergencyPerson { get; set; }
        public string? EmergencyContact { get; set; }
        public string? AdditionalInformation { get; set; }
        public string? UserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DoB { get; set; }
        public string? Gender { get; set; }
        public string Surname { get; set; } = null!;
        public string FirstName { get; set; } = null!;
      
        public virtual User? User { get; set; }
        public virtual ICollection<CareContract> CareContracts { get; set; }
    }
}
