using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    public partial class Patient
    {
        public Patient()
        {
            CareContracts = new HashSet<CareContract>();
        }

        [Key]
        public long PatientId { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string Surname { get; set; } = null!;
        [StringLength(15)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;
        [StringLength(1)]
        [Unicode(false)]
        public string Gender { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        public long SuburbId { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string? EmergencyPerson { get; set; }
        [StringLength(10)]
        public string? EmergencyContact { get; set; }
        [StringLength(50)]
        public string? AdditionalInformation { get; set; }
        [StringLength(450)]
        public string? UserId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("SuburbId")]
        [InverseProperty("Patients")]
        public virtual Suburb Suburb { get; set; } = null!;
        [ForeignKey("UserId")]
        [InverseProperty("Patients")]
        public virtual User? User { get; set; }
        [InverseProperty("Patient")]
        public virtual ICollection<CareContract> CareContracts { get; set; }
    }
}
