using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    [Index("RoleId", Name = "IX_AspNetUserRoles_RoleId")]
    public partial class AspNetUserRole
    {
        [Key]
        public string UserId { get; set; } = null!;
        [Key]
        public string RoleId { get; set; } = null!;
        public bool IsDeleted { get; set; }

        [ForeignKey("RoleId")]
        [InverseProperty("AspNetUserRoles")]
        public virtual AspNetRole Role { get; set; } = null!;
        [ForeignKey("UserId")]
        [InverseProperty("AspNetUserRoles")]
        public virtual User User { get; set; } = null!;
    }
}
