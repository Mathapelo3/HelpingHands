using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class ContactUsVM
    {
        public short Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Message { get; set; }
        public string PhoneNumber { get; set; } = null!;
    }
}
