using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    public partial class Nurse
    {
        public Nurse()
        {
            CareVisits = new HashSet<CareVisit>();
        }

        [Key]
        public long NurseId { get; set; }
        [StringLength(9)]
        public string NureCode { get; set; } = null!;
        [StringLength(1)]
        [Unicode(false)]
        public string Gender { get; set; } = null!;
        [Column("IDNumber")]
        [StringLength(13)]
        public string Idnumber { get; set; } = null!;
        [StringLength(20)]
        [Unicode(false)]
        public string Surname { get; set; } = null!;
        [StringLength(20)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;
        [StringLength(450)]
        public string? UserId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Nurses")]
        public virtual User? User { get; set; }
        [InverseProperty("Nurse")]
        public virtual ICollection<CareVisit> CareVisits { get; set; }
    }
}
