using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
            AspNetUserRoles = new HashSet<AspNetUserRole>();
        }

        [Key]
        public string Id { get; set; } = null!;
        [StringLength(256)]
        public string? Name { get; set; }
        [StringLength(256)]
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
    }
}
