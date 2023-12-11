using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class City
    {
        public City()
        {
            Suburbs = new HashSet<Suburb>();
        }

        public long CityId { get; set; }
        public string Name { get; set; } = null!;
        public string Abbreviation { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual ICollection<Suburb> Suburbs { get; set; }
    }
}
