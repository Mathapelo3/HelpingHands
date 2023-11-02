using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    public partial class Suburb
    {
        public Suburb()
        {
            CareContracts = new HashSet<CareContract>();
            Patients = new HashSet<Patient>();
        }

        [Key]
        public long SuburbId { get; set; }
        [Column("Suburb")]
        [StringLength(25)]
        [Unicode(false)]
        public string Suburbs { get; set; } = null!;
        public int PostalCode { get; set; }
        public long CityId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("CityId")]
        [InverseProperty("Suburbs")]
        public virtual City City { get; set; } = null!;
        [InverseProperty("Suburb")]
        public virtual ICollection<CareContract> CareContracts { get; set; }
        [InverseProperty("Suburb")]
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
