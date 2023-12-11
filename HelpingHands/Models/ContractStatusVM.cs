namespace HelpingHands.Models
{
    public class ContractStatusVM
    {
        public long ContractStatusId { get; set; }
        public string Status { get; set; } = null!;
        public bool IsDeleted { get; set; }

    }
}
