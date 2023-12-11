

using Dapper;
using HelpingHands.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace HelpingHands.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ReportController : Controller
    {
        IWebHostEnvironment _webHostEnvironment;
        private readonly IDbConnection _connection;

        public ReportController(IWebHostEnvironment webHostEnvironment, IDbConnection connection)
        {
            _webHostEnvironment = webHostEnvironment;
            _connection = connection;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Print()
        {
            return View();
        }


        private List<SuburbVM> GetSuburbsFromDatabase()
        {
            var suburbs = _connection.Query<SuburbVM>("SelectSuburb", commandType: CommandType.StoredProcedure).ToList();
            return suburbs;
        }

        [HttpGet]
        public IActionResult CreateCareContract()
        {
            var suburbs = GetSuburbsFromDatabase();

            var suburbList = suburbs.Select(suburb => new SelectListItem
            {
                Value = suburb.SuburbId.ToString(),
                Text = suburb.Suburb
            }).ToList();

            ViewBag.SuburbList = suburbList;
            return View();
        }


        public IActionResult NurseList()
        {

            var nurses = GetNurseList();

            var selectLists = nurses.Select(update => new SelectListItem
            {
                Value = update.NurseId.ToString(),
                Text = update.FirstName
            }).ToList();

            ViewBag.NurseList = selectLists;



            return View(nurses);
        }

        private List<NurseVM> GetNurseList()
        {
            var nurses = _connection.Query<NurseVM>("GetAllNurses", commandType: CommandType.StoredProcedure).ToList();

            return nurses;
        }

        public IActionResult DisplaySuburbsByNurse(PreferredSuburb preferred )
        {
            DynamicParameters parameters = new();
            parameters.Add("@NurseId", preferred.NurseId, DbType.Int64);

            _connection.Execute("GetSuburbsByNurse", parameters, commandType: CommandType.StoredProcedure);

            

            return View(parameters);
        }

        public IActionResult GetPatientRecords(PatientProfileVM patient)
        {
            var patients = GetPatientList();

            var selectLists = patients.Select(update => new SelectListItem
            {
                Value = update.PatientId.ToString(),
                Text = update.FirstName
                 = update.Surname
            }).ToList();

            ViewBag.NurseList = selectLists;
            DynamicParameters parameters = new();
            parameters.Add("@PatientId", patient.PatientId, DbType.Int64);

            _connection.Execute("GetPatientCareContractAndCareVisit", parameters, commandType: CommandType.StoredProcedure);



            return View(parameters);
        }

        private List<PatientProfileVM> GetPatientList()
        {
            var nurses = _connection.Query<PatientProfileVM>("GetAllPatients", commandType: CommandType.StoredProcedure).ToList();

            return nurses;
        }

        public IActionResult CreatePreferredSuburb()
        {
            var suburbs = GetSuburbsFromDatabase();

            var suburbList = suburbs.Select(suburb => new SelectListItem
            {
                Value = suburb.SuburbId.ToString(),
                Text = suburb.Suburb
            }).ToList();

            ViewBag.SuburbList = suburbList;
            return View();
        }

        public IActionResult DisplaySuburbPerNurse()
        {
            return View();
        }

        


    }
}
