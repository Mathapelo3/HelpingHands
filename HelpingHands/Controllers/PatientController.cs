using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpingHands.Controllers
{
    [Authorize(Roles = "Admin, Patient")]
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
