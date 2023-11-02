using Dapper;
using HelpingHands.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace HelpingHands.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDbConnection _connection;

        public HomeController(ILogger<HomeController> logger, IDbConnection connection)
        {
            _connection = connection;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }
        
        public IActionResult ContactUs()
        {
            return View();
        }


        public IActionResult GetInTouch(ContactUsVM contactUs)
        {
            DynamicParameters parameters = new();
            parameters.Add("@Name", contactUs.Name, DbType.String);
            parameters.Add("@Email", contactUs.Email, DbType.String);
            parameters.Add("@Message", contactUs.Message, DbType.String);
            parameters.Add("@PhoneNumber", contactUs.PhoneNumber, DbType.String);
            _connection.Execute("InsertContactUs", parameters, commandType: CommandType.StoredProcedure);
            return RedirectToAction("SuccessMessage");
        }


        public IActionResult SuccessMessage()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}