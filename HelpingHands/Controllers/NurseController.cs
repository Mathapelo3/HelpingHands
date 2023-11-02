using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpingHands.Controllers
{
    [Authorize(Roles = "Admin,Nurse")]
    public class NurseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
