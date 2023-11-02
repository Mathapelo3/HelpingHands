using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    public partial class ContactUsVM
    {
        [Key]
        public short Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(40)]
        public string Email { get; set; } = null!;
        [StringLength(255)]
        public string? Message { get; set; }
        [StringLength(10)]
        public string PhoneNumber { get; set; } = null!;
    }
}
