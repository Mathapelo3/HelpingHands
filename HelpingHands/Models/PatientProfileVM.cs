using System.ComponentModel.DataAnnotations;

namespace HelpingHands.Models
{
    public class PatientProfileVM
    {
        public long PatientId { get; set; }
        public string EmergencyPerson { get; set; }
        [Required(ErrorMessage = "You must provide a phone number,Phone Number at least 10 digit")]
        [StringLength(10)]
        public string EmergencyContact { get; set; }
        [StringLength(50, MinimumLength = 0)]
        public string AdditionalInformation { get; set; }
        public string UserId { get; set; }
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; } 

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
