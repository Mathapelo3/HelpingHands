using System;
using System.Collections.Generic;

namespace HelpingHands.Models
{
    public partial class Thusanang
    {
        public long OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string AddressLine2 { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? OperatingHours { get; set; }
        public string Nponumber { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
