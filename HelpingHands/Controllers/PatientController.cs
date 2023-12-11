using Dapper;
using HelpingHands.Areas.Identity.Data;
using HelpingHands.Data;
using HelpingHands.Models;
using HelpingHands.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;

namespace HelpingHands.Controllers
{

    [Authorize(Roles = "Admin, Patient")]
    public class PatientController : Controller
    {

        private readonly IDbConnection _connection;
        private readonly UserManager<HelpingHandsUser> _userManager;
        private readonly IPatientRepo _petRepo;
        private readonly ApplicationDbContext _context;
        
        public PatientController(IDbConnection connection, UserManager<HelpingHandsUser> userManager,IPatientRepo patientRepo)
        {
            _connection = connection;
            _userManager = userManager;
            _petRepo = patientRepo;
            
        }

        public IActionResult Index()
        {
            return View();
        }
     
        public async Task <IActionResult> PatientProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.Id, DbType.String, ParameterDirection.Input);

            var patientProfile = await _connection.QueryFirstOrDefaultAsync<PatientProfileVM>("GetPatientById", parameters, commandType: CommandType.StoredProcedure);
                    
            return View(patientProfile);
        }


        //[HttpGet]
        //public IActionResult EditProfile()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> EditPatientProfile(PatientProfileVM patient)
        {
            var user = await _userManager.GetUserAsync(User);

            DynamicParameters parameters = new();
            parameters.Add("@Id", user.Id, DbType.String, ParameterDirection.Input);
            parameters.Add("@Surname", patient.Surname, DbType.String);
            parameters.Add("@FirstName", patient.FirstName, DbType.String);
            parameters.Add("@DoB", patient.DoB, DbType.Date);
            parameters.Add("@gender", patient.Gender, DbType.String);
            parameters.Add("@EContact", patient.EmergencyContact, DbType.String);
            parameters.Add("@EmergencyP", patient.EmergencyPerson, DbType.String);
            parameters.Add("@Info", patient.AdditionalInformation, DbType.String);

            _connection.Execute("UpdatePatient", parameters, commandType: CommandType.StoredProcedure);
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> GetPatientCondition()
        {
            var user = await _userManager.GetUserAsync(User);

            List<PatientConditionVM> patientConditions = RetrievePatientConditionsFromDatabase(user.Id);

           
            return View(patientConditions);
        }

        private List<PatientConditionVM> RetrievePatientConditionsFromDatabase(string userId)
        {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", userId, DbType.String, ParameterDirection.Input);

                List<PatientConditionVM> patientConditions = _connection.Query<PatientConditionVM>("GetConditionByPatientId", parameters, commandType: CommandType.StoredProcedure).AsList();

                return patientConditions;
        }

        [HttpGet]
        public IActionResult CreatePatientCondition()
        {
            var conditions = GetAllChronicConditions();

            var conditionList = conditions.Select(condition => new SelectListItem
            {
                Value = condition.ConditionId.ToString(),
                Text = condition.Name
            }).ToList();

            ViewBag.ConditionList = conditionList;
            return View();
        }
      
        private List<ChronicCondition> GetAllChronicConditions()
        {
            var conditions = _connection.Query<ChronicCondition>("GetAllConditions", commandType: CommandType.StoredProcedure).ToList();

            return conditions;
        }
        [HttpPost]
        public async Task<IActionResult> AddPatientCondition(PatientConditionVM patient)
        {
            var user = await _userManager.GetUserAsync(User);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", user.Id, DbType.String);
            parameters.Add("@ConditionId", patient.ConditionId, DbType.Int32);

            var affectedRows = await _connection.ExecuteAsync(
                "InsertPatientCondition",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            if (affectedRows > 0)
            {
                TempData["SuccessMessage"] = "Condition added successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Condition already exists for the patient.";
            }

            return RedirectToAction("CreatePatientCondition");
        }

        [HttpPost]
        public IActionResult DeleteMyCondition(int conditionId, long patientId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ConditionId", conditionId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PatientId", patientId, DbType.Int64, ParameterDirection.Input);

            _connection.Execute("DeletePatientCondition", parameters, commandType: CommandType.StoredProcedure);

            return RedirectToAction("GetPatientCondition", "Patient");
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


            //CareContractVM careContract = new CareContractVM();

            //careContract.suburbs = _connection.Query<SuburbVM>("SelectSuburb", commandType: CommandType.StoredProcedure).AsList();


            //var selectList = new List<SelectListItem>();
            //foreach (var suburb in careContract.suburbs)
            //{
            //    selectList.Add(new SelectListItem { Value = suburb.SuburbId.ToString(), Text = suburb.Suburb });
            //}

            //ViewBag.SuburbList = selectList;

            //ViewBag.SuburbList = GetSuburbList();
            //return View(careContract);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCareContract(CareContractVM contractVM)
        {
            var user = await _userManager.GetUserAsync(User);

            

            DynamicParameters parameters = new();
            parameters.Add("@Id", user.Id, DbType.String, ParameterDirection.Input);
            parameters.Add("@Line1", contractVM.AddressLine1, DbType.String);
            parameters.Add("@Line2",contractVM.AddressLine2, DbType.String);
            parameters.Add("@SuburbId", contractVM.SuburbId, DbType.Int64);
            parameters.Add("@Wound", contractVM.WoundDescription, DbType.String);

            await _connection.ExecuteAsync("CreateCareContract", parameters, commandType: CommandType.StoredProcedure);

            return RedirectToAction("Index");
        }

        private IEnumerable<CareContractVM> RetrieveCareContractsFromDatabase(string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", userId, DbType.String, ParameterDirection.Input);

            return _connection.Query<CareContractVM>("GetMyContracts", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IActionResult> GetCareContracts()
        {
            var user = await _userManager.GetUserAsync(User);

            IEnumerable<CareContractVM> patientCareContracts = RetrieveCareContractsFromDatabase(user.Id);

            return View(patientCareContracts);
        }

    

        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}



