using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    public partial class City
    {
        public City()
        {
            Suburbs = new HashSet<Suburb>();
        }

        [Key]
        public long CityId { get; set; }
        [StringLength(25)]
        public string Name { get; set; } = null!;
        [StringLength(3)]
        [Unicode(false)]
        public string Abbreviation { get; set; } = null!;
        public bool IsDeleted { get; set; }

        [InverseProperty("City")]
        public virtual ICollection<Suburb> Suburbs { get; set; }

        
       
    }
}
