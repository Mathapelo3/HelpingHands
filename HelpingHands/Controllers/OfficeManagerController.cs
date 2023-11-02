using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpingHands.Controllers
{
    [Authorize(Roles = "Admin, Office Manager")]
    public class OfficeManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
