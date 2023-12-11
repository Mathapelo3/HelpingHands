namespace HelpingHands.Models
{
    public class PatientConditionVM
    {
        public string UserId { get; set; }
        public long PatientId { get; set; }
        public int ConditionId { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set;}
        public string Name { get; set;}
        public string Description { get; set;}
        

        public bool isSelected { get; set; }
        public IEnumerable<object> ConditionIds { get; internal set; }
    }
}
