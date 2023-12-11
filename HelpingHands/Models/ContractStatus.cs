using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class ContractStatus
    {
        public ContractStatus()
        {
            CareContracts = new HashSet<CareContract>();
        }

        public long ContractStatusId { get; set; }
        public string Status { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual ICollection<CareContract> CareContracts { get; set; }
    }
}
