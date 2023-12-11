using Dapper;
using HelpingHands.Areas.Identity.Data;
using HelpingHands.Data;
using HelpingHands.Models;
using HelpingHands.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HelpingHands.Controllers
{

    [Authorize(Roles = "Admin, OfficeManager, Nurse, Patient")]
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly HelpingHandsContext _db;
        private readonly PatientRepository _patientRepository;
        private readonly UserRepo _userRepo;


        public AccountController(HelpingHandsContext db, PatientRepository patientRepository, UserRepo userRepo, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
            _userRepo = userRepo;
           _patientRepository = patientRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddUser()
        { 
            
            return View();
        }
        
        public async Task<IActionResult> Lock(string id)
        {
            if(id== null)
            {
                return NotFound();
            }

            var helpingHandsUser = await _db.Users.FirstOrDefaultAsync (x => x.Id == id);

            if (helpingHandsUser == null)
            {
                return NotFound();
            }

            helpingHandsUser.LockoutEnd = DateTime.Now.AddMonths(1);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Unlock(string id)
        {
            if (id == null)
            {

            }

            var helpingHandsUser = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (helpingHandsUser == null)
            {
                return NotFound();
            }

            helpingHandsUser.LockoutEnd = DateTime.Now;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction("ChangePasswordConfirmation");
        }

        [HttpGet]
        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }




        ////Get: Patient to create profile
        //[HttpGet]
        //public async Task <IActionResult> PatientProfile(string userId)
        //{
        //    var userDetails = await _userRepo.GetUserByIdAsync(userId);

        //    if (userDetails == null)
        //    {
        //        return NotFound(userId);
        //    }
        //   return View();
        //}


        //[HttpPost]
        //public IActionResult SetUpProfile(Patient patient)
        //{
        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("@Surname", patient.Surname);
        //    parameters.Add("@FirstName", patient.FirstName);
        //    parameters.Add("@gender", patient.Gender);
        //    parameters.Add("@DoB", patient.BirthDate);
        //    parameters.Add("@Suburb", patient.SuburbId);
        //    parameters.Add("@EmergencyP", patient.EmergencyPerson);
        //    parameters.Add("@EmergencyC", patient.EmergencyContact);
        //    parameters.Add("@Info", patient.AdditionalInformation);
        //    parameters.Add("@Id", patient.UserId);
        //    _patientRepository.DMLPatient("CreatePatient", parameters, System.Data.CommandType.StoredProcedure);
        //    return RedirectToAction("Dashboard", "Patient");

        //    return RedirectToAction("Dashboard", "Patient");
        //}

        //public async Task<IActionResult> AddUser(CreateUsers createUsers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new User { UserName = model.Username };
        //        var result = await _userManager.CreateAsync(user, model.Password);

        //        if (result.Succeeded)
        //        {
        //            await _signManager.SignInAsync(user, false);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }
        //        }
        //    }

        //    return View();
        //}


    }
}
