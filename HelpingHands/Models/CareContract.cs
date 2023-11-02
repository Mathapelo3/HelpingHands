using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    public partial class CareContract
    {
        public CareContract()
        {
            CareVisits = new HashSet<CareVisit>();
        }

        [Key]
        public long ContractId { get; set; }
        [StringLength(8)]
        [Unicode(false)]
        public string ContractNo { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime? ContractDate { get; set; }
        public long? PatientId { get; set; }
        [StringLength(25)]
        public string AddressLine1 { get; set; } = null!;
        [StringLength(25)]
        public string? AddressLine2 { get; set; }
        public long SuburbId { get; set; }
        [StringLength(225)]
        public string? WoundDescription { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string? Nurse { get; set; }
        public long StatusId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("PatientId")]
        [InverseProperty("CareContracts")]
        public virtual Patient? Patient { get; set; }
        [ForeignKey("StatusId")]
        [InverseProperty("CareContracts")]
        public virtual ContractStatus Status { get; set; } = null!;
        [ForeignKey("SuburbId")]
        [InverseProperty("CareContracts")]
        public virtual Suburb Suburb { get; set; } = null!;
        [InverseProperty("Contract")]
        public virtual ICollection<CareVisit> CareVisits { get; set; }
    }
}
