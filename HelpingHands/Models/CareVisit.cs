using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    [Table("CareVisit")]
    public partial class CareVisit
    {
        [Key]
        public long CareVisitId { get; set; }
        public long ContractId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? VisitDate { get; set; }
        [Column("Approx.ArriveDate")]
        public TimeSpan? ApproxArriveDate { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public TimeSpan? DepartTime { get; set; }
        [StringLength(225)]
        public string? WoundCondition { get; set; }
        [StringLength(225)]
        public string? Notes { get; set; }
        public long? NurseId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ContractId")]
        [InverseProperty("CareVisits")]
        public virtual CareContract Contract { get; set; } = null!;
        [ForeignKey("NurseId")]
        [InverseProperty("CareVisits")]
        public virtual Nurse? Nurse { get; set; }
    }
}
