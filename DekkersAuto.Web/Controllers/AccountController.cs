using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Web.Models;
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
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, DbService dbService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _dbService = dbService;
        }

        /// <summary>
        /// Default action, returns the management homepage 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Index()
        {

            var model = new AccountViewModel();
            var banner = _dbService.GetBanner();
            var bannerModel = new BannerViewModel();
            if(banner!= null)
            {
                bannerModel.BannerId = banner.Id;
                bannerModel.IsActive = banner.IsActive;
                bannerModel.Text = banner.Text;
            }
            model.BannerModel = bannerModel;

            var user = await UserManager.GetUserAsync(User);
            model.ManageAccountModel = new ManageAccountViewModel
            {
                Username = user.UserName,
                Role = _dbService.GetRole(user),
                RoleTypes = _dbService.GetRoles(),
                UserId = user.Id
            };

            var accountList = _dbService.GetAccountList(user.Id);

            model.AccountList = accountList;

            return View(model);
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
            var model = new ManageAccountViewModel();
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
        public async Task<IActionResult> Create(ManageAccountViewModel model)
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

        public IActionResult ManageBanner(BannerViewModel model)
        {

            if (_dbService.GetBanner() == null)
            {
                _dbService.CreateBanner(model);
            }
            else
            {
                _dbService.UpdateBanner(model);
            }

            return PartialView("_ManageBanner", model);
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

        public async Task Delete(string userId)
        {
            await _dbService.DeleteUserAsync(userId);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ManageAccountViewModel model)
        {
            await _dbService.UpdateUser(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid userId)
        {
            var user = await _dbService.GetUser(userId);
            var model = new ManageAccountViewModel
            {
                Username = user.UserName,
                Role = _dbService.GetRole(user),
                RoleTypes = _dbService.GetRoles(),
                UserId = user.Id
            };

            return View(model);
        }

        public async Task<IActionResult> AccountList()
        {
            var user = await UserManager.GetUserAsync(User);
            var model =_dbService.GetAccountList(user.Id);

            return View(model);
        }

        public IActionResult Banner()
        {
            var banner = _dbService.GetBanner();
            var model = new BannerViewModel();
            if (banner != null)
            {
                model.BannerId = banner.Id;
                model.IsActive = banner.IsActive;
                model.Text = banner.Text;
            }
            return View(model);
        }


    }
}