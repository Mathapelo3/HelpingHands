namespace HelpingHands.Models
{
    public class NurseVM
    {
        public long NurseId { get; set; }

        public string? UserId { get; set; }
        public bool IsDeleted { get; set; }
        public string Gender { get; set; }
        public string? Idnumber { get; set; }
        public string Surname { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string PhoneNumber { get; set; }
        public string NurseCode { get; set; }
        public string Email { get; set; }
        public long SuburbId { get; set; }
    }
}
