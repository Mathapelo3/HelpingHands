using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class ThusanagGallery
    {
        public int ImageId { get; set; }
        public byte[] Img { get; set; } = null!;
        public string? Description { get; set; }
    }
}
