using Dapper;
using HelpingHands.Models;
using HelpingHands.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.Common;

namespace HelpingHands.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private ICityRepo _cityRepo;
        private IDbConnection _connection;
        private ISuburbsRepo _suburbsRepo;

        public AdminController(IDbConnection connection, ICityRepo cityRepo, ISuburbsRepo suburbsRepo)
        {
            _connection = connection;
            _cityRepo = cityRepo;
            _suburbsRepo = suburbsRepo;
        }



        public IActionResult Index()
        {
           return View();
        }

       



        public IActionResult ManageNurse()
        {
            return View();
        }

        public IActionResult ManageOfficeManager()
        {
            return View();
        }

        public IActionResult ManageUsers()
        {
            return View();
        }

        public IActionResult ManageConditions()
        {
            return View();
        }

        public IActionResult BusinessInfo()
        {
           
            return View();
        }
        //City table
        public IActionResult ManageCities()
        {
            var cities = _cityRepo.GetCities("GetCities", null, System.Data.CommandType.StoredProcedure);
            return View(cities);
        }
        [HttpGet]
        public IActionResult CreateCity()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCities(City city)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", city.Name);
            parameters.Add("@Abbreviation", city.Abbreviation);
            _cityRepo.DMLCities("InsertCity", parameters, System.Data.CommandType.StoredProcedure);


            //DynamicParameters parameters = new();
            //parameters.Add("@Name", city.Name, DbType.String);
            //parameters.Add("@Abbreviation", city.Abbreviation, DbType.String);
            //_connection.Execute("InsertCity", parameters, commandType: CommandType.StoredProcedure);

            return RedirectToAction("ManageCities");
        }
        [HttpGet]
        public IActionResult UpdateCity()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpdateCity(long cityId)
        {
            DynamicParameters parameters = new();
            parameters.Add("@Id", cityId, DbType.Int64);
            var city = _cityRepo.GetCityById("GetCityById", parameters, System.Data.CommandType.StoredProcedure);
            return View(city);
               
        }

        [HttpPost]
        public  IActionResult UpdateCity(City city)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Name", city.Name);
            parameters.Add("@Abbreviation", city.Abbreviation);
            _cityRepo.DMLCities("UpdateCity", parameters, System.Data.CommandType.StoredProcedure);

            return RedirectToAction("ManageCities");
        }
        //[HttpPost]
        //public IActionResult GetSuburbsByCity()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult GetSuburbByCity(long cityId)
        {
            //DynamicParameters parameters = new();
            //parameters.Add("@Id", id, DbType.Int64);
            var suburbs = _suburbsRepo.GetSuburbsByCity(cityId);

            if (suburbs != null)
            {
                return NotFound();
            }
            
            return View(suburbs);
        
            
        }
        //Suburbs Table
        public IActionResult ManageSuburbs()
        {
            return View();
        }

        private List<AspNetRole> GetRoles()
        {
            var role = _connection.Query<AspNetRole>("RoleList", commandType: CommandType.StoredProcedure).ToList();

            return role;
        }

        [HttpGet]
        public IActionResult RoleList()
        {
            var roles = GetRoles();

            var roleList = roles.Select(role => new SelectListItem
            {
                Value = role.Id.ToString(),
                Text = role.Name
            }).ToList();

            ViewBag.RoleList = roleList;
            return View();
        }








    }
}
