using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpingHands.Models
{
    public class PatientContractViewModel
    {

        public List<CareContractVM> Contracts { get; set; }
        public List<SelectListItem> PatientList { get; set; }
    }
}
