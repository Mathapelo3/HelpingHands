using Dapper;
using HelpingHands.Areas.Identity.Data;
using HelpingHands.Data;
using HelpingHands.Models;
using HelpingHands.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Contracts;

namespace HelpingHands.Controllers
{
    [Authorize(Roles = "Admin, Office Manager")]
    public class OfficeManagerController : Controller
    {
        private readonly IDbConnection _connection;
        
        private readonly IPatientRepo _petRepo;
        private readonly ApplicationDbContext _context;

        public OfficeManagerController(IDbConnection connection, IPatientRepo patientRepo)
        {
            _connection = connection;
           
            _petRepo = patientRepo;

        }

        public async Task<IActionResult> Index()
        {
            var nurses = await _connection.QueryAsync<NurseVM>("GetAllNurses",commandType: CommandType.StoredProcedure);
            return View(nurses);
        }

        public async Task<IActionResult> NewContracts()
        {
            var contracts = await _connection.QueryAsync<CareContractVM>("GetNewContracts", commandType: CommandType.StoredProcedure);
            return View(contracts);
            
        }

        public IActionResult AssignContract(long contractId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ContractId", contractId);

            var contract = _connection.QueryFirstOrDefault<CareContractVM>("ViewSpecificContract", parameters, commandType: CommandType.StoredProcedure);

            if (contract == null)
            {
                return NotFound();
            }

            var nursesParameters = new DynamicParameters();
            nursesParameters.Add("@SuburbId", contract.SuburbId);
            nursesParameters.Add("@ContractId", contract.ContractId);

            var nurses = _connection.Query<Nurse>("GetNursesBySuburb", nursesParameters, commandType: CommandType.StoredProcedure).ToList();

            ViewBag.NurseList = new SelectList(nurses, "NurseId", "FirstName", contract.Nurse);


            var status = GetContractStatus();

            var selectLists = status.Select(update => new SelectListItem
            {
                Value = update.ContractStatusId.ToString(),
                Text = update.Status
            }).ToList();

            ViewBag.StatusList = selectLists;

            return View(contract);
        }


        [HttpPost]
        public IActionResult UpdateContract(CareContractVM contract)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@ContractId", contract.ContractId, DbType.Int64);
            parameters.Add("@NurseId", contract.Nurse, DbType.Int64);
            parameters.Add("@Status", contract.StatusId, DbType.Int64);
            parameters.Add("@StartDate", contract.StartDate, DbType.Date);
            parameters.Add("@EndDate", contract.EndDate, DbType.Date);

            _connection.Execute("UpdateCareContract", parameters, commandType: CommandType.StoredProcedure);

            return RedirectToAction("NewContracts");
        }


        private List<ContractStatusVM> GetContractStatus()
        {
            var status = _connection.Query<ContractStatusVM>("GetContractStatus", commandType: CommandType.StoredProcedure).ToList();

            return status;
        }

        //Report
        public IActionResult DisplaySuburbPerNurse()
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

        public IActionResult DisplaySuburbsByNurse(PreferredSuburb preferred)
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

        public IActionResult NurseBySuburb(long suburbId)
        {
            DynamicParameters parameters = new DynamicParameters();

            var result = _connection.QueryMultiple(
                "ListofnursesperSuburb",
                new { SuburbId = suburbId },
                commandType: CommandType.StoredProcedure
            );

            var nursesPerSuburb = new NurseSuburbVM
            {
                FirstName = result.Read<string>().FirstOrDefault(),
                Surname = result.Read<string>().FirstOrDefault(),
                Suburb = result.Read<string>().FirstOrDefault(),
                NumberOfNursesPerSuburb = result.Read<int>().FirstOrDefault()
            };

            return View(nursesPerSuburb);
        }

        public IActionResult GetSuburbs(string selectedSuburb)
        {
            List<PreferredSuburbVM> nurses = new List<PreferredSuburbVM>();

            
               

                var parameters = new DynamicParameters();
                parameters.Add("@Suburb", selectedSuburb);

                nurses = _connection.Query<PreferredSuburbVM>("ViewNursesBySuburb", parameters, commandType: CommandType.StoredProcedure).ToList();

                var suburbList = GetPreferredSuburbVMs();
                var selectList = suburbList.Select(s => new SelectListItem
                {
                    Value = s.Suburb,
                    Text = s.Suburb
                }).ToList();

                ViewBag.SuburbList = selectList;
           

            return View(nurses);
        }
        private List<PreferredSuburbVM> GetPreferredSuburbVMs()
        {
            var suburbs =  _connection.Query<PreferredSuburbVM>("SelectSuburb", commandType: CommandType.StoredProcedure).ToList();

            return suburbs;
        }



        // Date range

        public IActionResult AssignedContract(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || startDate.Value == DateTime.MinValue)
            {
                startDate = new DateTime(1753, 1, 1);
            }

            if (endDate == null || endDate.Value == DateTime.MinValue)
            {
                endDate = DateTime.MaxValue;
            }

            var contracts = _connection.Query<CareContractVM>("GetAssignedCareContractsByDateRange",
                new { StartDate = startDate, EndDate = endDate },
                commandType: CommandType.StoredProcedure);

            return View(contracts);
        }

        public IActionResult ClosedContracts(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || startDate.Value == DateTime.MinValue)
            {
                startDate = new DateTime(1753, 1, 1);
            }

            if (endDate == null || endDate.Value == DateTime.MinValue)
            {
                endDate = DateTime.MaxValue;
            }

            var contracts = _connection.Query<CareContractVM>("GetClosedCareContractsByDateRange",
                new { StartDate = startDate, EndDate = endDate },
                commandType: CommandType.StoredProcedure);

            return View(contracts);
        }

        //Nurses Assigned contracts

        public IActionResult NurseContracts(long nurseId, string statusChar)
        {
            // Ensure that a non-null, non-empty status character is passed to the stored procedure.
            // If it's null or empty, default to 'A'.
            char? status = string.IsNullOrEmpty(statusChar) ? '2' : statusChar[0];

            var parameters = new DynamicParameters();
            parameters.Add("@Id", nurseId);
            parameters.Add("@Status", status);

            var contracts = _connection.Query<CareContractVM>(
                "NurseContracts",
                parameters,
                commandType: CommandType.StoredProcedure
            ).ToList();

            var statusList = GetContractStatus(); // Assuming this method retrieves the status list
            ViewBag.StatusList = statusList.Select(s => new SelectListItem
            {
                Value = s.ContractStatusId.ToString(),
                Text = s.Status
            });

            return View(contracts);
        }




        [HttpGet]
        public IActionResult GetContract(long contractId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ContractId", contractId);

            var contract = _connection.QueryFirstOrDefault<CareVisitVM>("ViewAssignedContractBySuburb", parameters, commandType: CommandType.StoredProcedure);

            if (contract == null)
            {
                return NotFound();
            }


            return View(contract);
        }

        public IActionResult GetCareVisits(long contractId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", contractId);

            var contract = _connection.Query<CareVisitVM>("CareVisitsbyContract", parameters, commandType: CommandType.StoredProcedure);

            if (contract == null)
            {
                return NotFound();
            }


            return View(contract);
        }


        [HttpGet]
        public IActionResult ViewVisit(long visitId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", visitId);

            var contract = _connection.QueryFirstOrDefault<CareVisitVM>("ViewCareVisit", parameters, commandType: CommandType.StoredProcedure);

            if (contract == null)
            {
                return NotFound();
            }


            return View(contract);
        }

       
        public IActionResult ViewNurse(long nurseId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", nurseId);

            var contract = _connection.Query<CareVisitVM>("NurseCareVisit", parameters, commandType: CommandType.StoredProcedure);

            if (contract == null)
            {
                return NotFound();
            }


            return View(contract);
        }


        //Reports
        public IActionResult Reports()
        {


            var nurses = GetNurseList();

            var selectLists = nurses.Select(update => new SelectListItem
            {
                Value = update.NurseId.ToString(),
                Text = update.FirstName + " " + update.Surname
            }).ToList();

            ViewBag.NurseList = selectLists;


            return View();
        }


        public IActionResult SuburbPerNurse(string name)
        {
            List<PreferredSuburbVM> nurse = new List<PreferredSuburbVM>();

            var parameters = new DynamicParameters();
            parameters.Add("@Name", name);

            var suburbs = _connection.Query<PreferredSuburbVM>("SuburbByNurse", parameters, commandType: CommandType.StoredProcedure);

           

            var nurses = GetNurseList();

            var selectLists = nurses.Select(update => new SelectListItem
            {
                Value = update.NurseId.ToString(),
                Text = update.FirstName + " " + update.Surname
            }).ToList();

            ViewBag.NurseList = selectLists;

            return View(suburbs);
        }



        public IActionResult PatientContract(string name)
        {
            List<CareContractVM> nurse = new List<CareContractVM>();

            var parameters = new DynamicParameters();
            parameters.Add("@Name", name);

            var suburbs = _connection.Query<CareContractVM>("GetPatientCareContractAndCareVisit", parameters, commandType: CommandType.StoredProcedure);

            var patients = GetPatientList();

            var selectLists = patients.Select(update => new SelectListItem
            {
                Value = update.PatientId.ToString(),
                Text = update.FirstName + " " + update.Surname
            }).ToList();

            ViewBag.PatientList = selectLists;

            return View(suburbs);
        }

        public IActionResult ViewContract(long contractId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ContractId", contractId);

            var contract = _connection.QueryFirstOrDefault<CareContractVM>("ViewSpecificContract", parameters, commandType: CommandType.StoredProcedure);

            

            return View(contract);
        }

        public async Task<IActionResult> GetNurses()
        {
            var nurses = await _connection.QueryAsync<NurseVM>("GetAllNurses", commandType: CommandType.StoredProcedure);
            return View(nurses);
        }


    }
}
