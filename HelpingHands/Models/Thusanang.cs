using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    [Table("Thusanang")]
    public partial class Thusanang
    {
        [Key]
        public long OrganizationId { get; set; }
        [StringLength(30)]
        public string? OrganizationName { get; set; }
        [StringLength(25)]
        public string AddressLine1 { get; set; } = null!;
        [StringLength(15)]
        public string AddressLine2 { get; set; } = null!;
        [StringLength(13)]
        public string ContactNumber { get; set; } = null!;
        [StringLength(45)]
        public string Email { get; set; } = null!;
        [StringLength(30)]
        public string? OperatingHours { get; set; }
        [Column("NPONumber")]
        [StringLength(10)]
        public string Nponumber { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
