﻿
using Dapper;
using HelpingHands.Areas.Identity.Data;
using HelpingHands.Data;
using HelpingHands.Models;
using HelpingHands.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Diagnostics.Contracts;

namespace HelpingHands.Controllers
{
    [Authorize(Roles = "Admin,Nurse")]
    public class NurseController : Controller
    {

        private readonly IDbConnection _connection;
        private readonly UserManager<HelpingHandsUser> _userManager;
        private readonly IPatientRepo _petRepo;
        private readonly ApplicationDbContext _context;

        public NurseController(IDbConnection connection, UserManager<HelpingHandsUser> userManager, IPatientRepo patientRepo, ApplicationDbContext context)
        {
            _connection = connection;
            _userManager = userManager;
            _petRepo = patientRepo;
            _context = context;
        }

        public IActionResult Index()
        {
            var newContractCount = _connection.QueryFirstOrDefault<int>("CountNewContracts", commandType: CommandType.StoredProcedure);
            var assignedContractCount = _connection.QueryFirstOrDefault<int>("CountAssignedContracts", commandType: CommandType.StoredProcedure);
            var closedContractCount = _connection.QueryFirstOrDefault<int>("CountClosedContracts", commandType: CommandType.StoredProcedure);

            ViewBag.NewContractCount = newContractCount;
            ViewBag.AssignedContractCount = assignedContractCount;
            ViewBag.ClosedContractCount = closedContractCount;

            return View();
           
        }

        //Profile
        public async Task<IActionResult> NurseProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.Id, DbType.String, ParameterDirection.Input);

            var nurseProfile = await _connection.QueryFirstOrDefaultAsync<NurseVM>("GetNurseById", parameters, commandType: CommandType.StoredProcedure);

            return View(nurseProfile);
        }

        [HttpPost]
        public async Task<IActionResult> EditNurseProfile(NurseVM nurse)
        {
            var user = await _userManager.GetUserAsync(User);

            DynamicParameters parameters = new();
            parameters.Add("@Id", user.Id, DbType.String, ParameterDirection.Input);
            parameters.Add("@Surname", nurse.Surname, DbType.String);
            parameters.Add("@FirstName", nurse.FirstName, DbType.String);
            parameters.Add("@IDNumber", nurse.Idnumber, DbType.String);
            parameters.Add("@Gender", nurse.Gender, DbType.String);


            _connection.Execute("UpdateNurse", parameters, commandType: CommandType.StoredProcedure);
            return RedirectToAction("Index");
        }
        //PreferredSuburbs
        public IActionResult PreferredSuburbs()
        {
            var closedContractCount = _connection.QueryFirstOrDefault<int>("CountClosedContracts", commandType: CommandType.StoredProcedure);
            return View();
        }
        //assigned
        public async Task<IActionResult> GetAssignedContract( )
        {
            var user = await _userManager.GetUserAsync(User);

            IEnumerable<CareContractVM> assignedCareContracts = RetrieveAssignedCareContractsFromDatabase(user.Id);

            return View(assignedCareContracts);
        }

        private IEnumerable<CareContractVM> RetrieveAssignedCareContractsFromDatabase(string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", userId, DbType.String, ParameterDirection.Input);

            return _connection.Query<CareContractVM>("GetMyAssignedContracts", parameters, commandType: CommandType.StoredProcedure);
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

        //new contract

        private IEnumerable<CareContractVM> RetrieveNewCareContractsFromDatabase(string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", userId, DbType.String, ParameterDirection.Input);

            return _connection.Query<CareContractVM>("NewCareContractByNurseId", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IActionResult> GetNewContract()
        {
            var user = await _userManager.GetUserAsync(User);

            IEnumerable<CareContractVM> newCareContracts = RetrieveNewCareContractsFromDatabase(user.Id);

            return View(newCareContracts);
        }

        public IActionResult AssignContract(long contractId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ContractId", contractId);

            var contract = _connection.QueryFirstOrDefault<CareContractVM>("ViewSpecificContract", parameters, commandType: CommandType.StoredProcedure);

            var status = GetContractStatus();

            var selectLists = status.Select(update => new SelectListItem
            {
                Value = update.ContractStatusId.ToString(),
                Text = update.Status
            }).ToList();

            ViewBag.StatusList = selectLists;

            return View(contract);
        }

        private List<ContractStatusVM> GetContractStatus()
        {
            var status = _connection.Query<ContractStatusVM>("GetContractStatus", commandType: CommandType.StoredProcedure).ToList();

            return status;
        }

        public IActionResult UpdateContract(CareContractVM contract)
        {
            var user = _userManager.GetUserAsync(User);

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@UserId", user.Id, DbType.String, ParameterDirection.Input);
            parameters.Add("@ContractId", contract.ContractId, DbType.Int64);
            parameters.Add("@Status", contract.StatusId, DbType.Int64);
            parameters.Add("@StartDate", contract.StartDate, DbType.Date);
            parameters.Add("@EndDate", contract.EndDate, DbType.Date);

           var affectedRows = _connection.Execute("NurseAssignContract", parameters, commandType: CommandType.StoredProcedure);
            
            if (affectedRows > 0)
            {
                TempData["SuccessMessage"] = "Care Contract Assigned";
            }
            else
            {
                TempData["ErrorMessage"] = "Care contract Not Assigned";
            }

            return RedirectToAction("GetNewContract");
        }

        //CareVisit
        public async Task<IActionResult> CreateCareVisit( CareVisitVM care)
        {
            var user = await _userManager.GetUserAsync(User);

            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.Id);
            parameters.Add("@VisitDate", care.VisitDate);
            parameters.Add("@ProxArrive", care.ApproxArriveDate);
            parameters.Add("@ContractId", care.ContractId);

            var affectedRows = await _connection.ExecuteAsync("CreateCareVisit", parameters, commandType: CommandType.StoredProcedure);

            if (affectedRows > 0)
            {
                TempData["SuccessMessage"] = "Care Visit Created";
            }
            else
            {
                TempData["ErrorMessage"] = "Time already selected of that day";
            }

            return RedirectToAction("GetAssignedContract");
        }

        public async Task<IActionResult> ViewCareVisits()
        {
            var user = await _userManager.GetUserAsync(User);

            IEnumerable<CareVisitVM> viewCareVisit = RetrieveCareVisitsFromDatabase(user.Id);

            return View(viewCareVisit);
        }

        private IEnumerable<CareVisitVM> RetrieveCareVisitsFromDatabase(string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", userId, DbType.String, ParameterDirection.Input);

            return _connection.Query<CareVisitVM>("GetMyAssignedContracts", parameters, commandType: CommandType.StoredProcedure);
        }


        //PreferredSuburb

        public async Task<IActionResult> GetNursePreferredSuburb()
        {
            var user = await _userManager.GetUserAsync(User);

            List<PreferredSuburbVM> patientConditions = RetrieveNurseSuburbsFromDatabase(user.Id);


            return View(patientConditions);
        }

        private List<PreferredSuburbVM> RetrieveNurseSuburbsFromDatabase(string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", userId, DbType.String, ParameterDirection.Input);

            List<PreferredSuburbVM> patientConditions = _connection.Query<PreferredSuburbVM>("GetNurseSuburbs", parameters, commandType: CommandType.StoredProcedure).AsList();

            return patientConditions;
        }

        private List<SuburbVM> GetSuburbsFromDatabase()
        {
            var suburbs = _connection.Query<SuburbVM>("SelectSuburb", commandType: CommandType.StoredProcedure).ToList();
            return suburbs;
        }

        public long GetNurseId(string userId)
        {
             // Replace with your actual connection string
            long nurseId;

            
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId, DbType.String);

                nurseId = _connection.QueryFirstOrDefault<long>(
                    "GetNurseIdByUserId",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            

            return nurseId;
        }

        public async Task<IActionResult> CreatePreferredSuburb()
        {
            var user = await _userManager.GetUserAsync(User);
            var nurseId = GetNurseId(user.Id);
            var suburbs = GetSuburbsFromDatabase();

            var suburbList = suburbs.Select(suburb => new SelectListItem
            {
                Value = suburb.SuburbId.ToString(),
                Text = suburb.Suburb
            }).ToList();

            ViewBag.SuburbList = suburbList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPreferredSuburb(PreferredSuburbVM suburb)
        {
            var user = await _userManager.GetUserAsync(User);
            var nurseId = GetNurseId(user.Id);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@NurseId", nurseId, DbType.Int64);
            parameters.Add("@SuburbId", suburb.SuburbId, DbType.Int64);

            var affectedRows = await _connection.ExecuteAsync(
                "InsertNursePreferredSub",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            if (affectedRows > 0)
            {
                TempData["SuccessMessage"] = "Suburb added successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to add the suburb.";
            }

            return RedirectToAction("CreatePreferredSuburb");
        }






    }
}
