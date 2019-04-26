using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DekkersAuto.Web.Controllers
{
    /// <summary>
    /// Controller to handle all requests regarding creation, management, and logging into accounts
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Gets and sets the UserManager
        /// Usermanager handles creation and manipulation of users
        /// </summary>
        private UserManager<IdentityUser> UserManager { get; set; }
        /// <summary>
        /// Gets and sets the RoleManager
        /// Rolemanager handles creation, manipulation and assignment of roles
        /// </summary>
        private RoleManager<IdentityRole> RoleManager { get; set; }

        /// <summary>
        /// Gets and sets the SignInManager
        /// The Signin manager handles signing in and signing out users
        /// </summary>
        private SignInManager<IdentityUser> SignInManager { get; set; }

        private DbService _dbService;

        /// <summary>
        /// Default constructor. Has services passed in through dependency injection
        /// </summary>
        /// <param name="userManager">The user manager for IdentityUsers</param>
        /// <param name="roleManager">The role manager for IdentityRoles</param>
        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, DbService dbService)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
            _dbService = dbService;
        }

        /// <summary>
        /// Default action, returns the management homepage 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Action to display the create account page. Allows Admin users to create new user accounts.
        /// Populates the Create model with the available user roles
        /// </summary>
        /// <returns>The create view</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateViewModel();
            model.RoleTypes = _dbService.GetRoles();

            return View(model);
        }

        /// <summary>
        /// Action to create a new user.
        /// Accessible only by Admin users
        /// </summary>
        /// <param name="model">Model containing details of user to create</param>
        /// <returns>Redirects to the Account index on success</returns>
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create(CreateViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.RoleTypes = _dbService.GetRoles();
                return View(model);
            }
            var result = await _dbService.CreateUserAsync(model.Username, model.Password, model.Role);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the Login page
        /// </summary>
        /// <returns>Login view</returns>
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        /// <summary>
        /// Action attempt to log a user in
        /// </summary>
        /// <param name="model">Contains username and password of user to log in</param>
        /// <returns>Redirects back to homepage on success</returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// Action to logout the currently logged in user
        /// </summary>
        /// <returns>Redirects to the homepage</returns>
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

       
    }
}