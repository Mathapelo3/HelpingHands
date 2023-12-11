using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class Suburb
    {
        public Suburb()
        {
            CareContracts = new HashSet<CareContract>();
        }

        public long SuburbId { get; set; }
        public string Suburbs { get; set; } = null!;
        public int PostalCode { get; set; }
        public long CityId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual City City { get; set; } = null!;
        public ICollection<CareContract> CareContracts { get; set; }
    }
}
