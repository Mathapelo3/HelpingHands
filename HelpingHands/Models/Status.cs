using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    [Table("Status")]
    public partial class Status
    {
        [Key]
        public int StatusId { get; set; }
        [Column("Status")]
        [StringLength(1)]
        [Unicode(false)]
        public string Status1 { get; set; } = null!;
    }
}
