using System.ComponentModel.DataAnnotations;

namespace HelpingHands.Models
{
    public class CareVisitVM
    {
        public long ContractId { get; set; }
        public string ContractNo { get; set; }
        public DateTime ContractDate { get; set; }
        public long PatientId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public long SuburbId { get; set; }


        [Required(ErrorMessage = "Please enter wound description")]
        public string? WoundDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long Nurse { get; set; }
        public string PostalCode { get; set; }
        public long StatusId { get; set; }
        public bool IsDeleted { get; set; }
        public string PhoneNumber { get; set; }
        public string Suburb { get; set; }

        public string Status { get; set; }


        public long CareVisitId { get; set; }
        
        public DateTime VisitDate { get; set; }
        public TimeSpan ApproxArriveDate { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public TimeSpan DepartTime { get; set; }
        public string WoundCondition { get; set; }
        public string Notes { get; set; }
        public long NurseId { get; set; }
   

    }
}
