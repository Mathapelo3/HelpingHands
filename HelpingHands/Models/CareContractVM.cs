using HelpingHands.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HelpingHands.Models
{
    public class CareContractVM
    {
        public CareContractVM()
        {
            suburbs = new List<SuburbVM>();
        }

        public long NurseId { get; set; }
        public long ContractId { get; set; }
        public string ContractNo { get; set; }
        public DateTime ContractDate { get; set; }
        public long? PatientId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public long SuburbId { get; set; }

        public int ConditionId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }


        [Required(ErrorMessage = "Please enter wound description")]
        public string? WoundDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long Nurse { get; set; }
        public string PostalCode { get; set; }
        public long StatusId { get; set; }
        public bool IsDeleted { get; set; }
        public string NurseCode { get; set; }

        public string Suburb { get; set; }

        public string Status { get; set; }

        public List<SuburbVM> suburbs { get; set; }
    }
}
