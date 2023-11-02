using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    [Table("ContractStatus")]
    public partial class ContractStatus
    {
        public ContractStatus()
        {
            CareContracts = new HashSet<CareContract>();
        }

        [Key]
        public long ContractStatusId { get; set; }
        [StringLength(1)]
        [Unicode(false)]
        public string Status { get; set; } = null!;
        public bool IsDeleted { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<CareContract> CareContracts { get; set; }
    }
}
