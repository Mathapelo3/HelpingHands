using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HelpingHands.Models
{
    [Table("ThusanagGallery")]
    public partial class ThusanagGallery
    {
        [Key]
        public int ImageId { get; set; }
        [Column(TypeName = "image")]
        public byte[] Img { get; set; } = null!;
        [StringLength(50)]
        public string? Description { get; set; }
    }
}
