using System;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Services.Database;
using DekkersAuto.Services.Models;
using DekkersAuto.Web.Models;
using DekkersAuto.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DekkersAuto.Web.Controllers
{
    /// <summary>
    /// Controller to handle all requests regarding creation, management, and logging into accounts
    /// </summary>
    public class AccountController : Controller
    {
        private DbService _dbService;

        private IdentityService _identityService;

        private BannerService _bannerService;

        private ListingService _listingService;

        /// <summary>
        /// Default constructor. Has services passed in through dependency injection
        /// </summary>
        /// <param name="userManager">The user manager for IdentityUsers</param>
        /// <param name="roleManager">The role manager for IdentityRoles</param>
        public AccountController(DbService dbService, IdentityService identityService, BannerService bannerService, ListingService listingService)
        {
            _dbService = dbService;
            _identityService = identityService;
            _bannerService = bannerService;
            _listingService = listingService;
        }

        /// <summary>
        /// Default action, returns the management homepage 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Index()
        {

            var model = new AccountViewModel();
            var banner = _bannerService.GetBanner();

            model.BannerModel = banner;

            var user = await _identityService.GetIdentityUserAsync(User);
            model.ManageAccountModel = new ManageAccountViewModel
            {
                Username = user.UserName,
                Role = _identityService.GetRole(user),
                RoleTypes = Util.GetSelectList(_identityService.GetRoles()),
                UserId = user.Id
            };

            var accountList = _identityService.GetAccountList(user.Id);

            model.AccountList = new AccountListViewModel
            {
                UserId = user.Id,
                Accounts = accountList.Select(a => new AccountItemViewModel { AccountId = a.UserId, Username = a.Username, Role = a.Role }).ToList()
            };

            return View(model);
        }


        /// <summary>
        /// Action to display the create account page. Allows Admin users to create new user accounts.
        /// Populates the Create model with the available user roles
        /// </summary>
        /// <returns>The create view</returns>
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ManageAccountViewModel();
            model.RoleTypes = Util.GetSelectList(_identityService.GetRoles());

            return View(model);
        }

        /// <summary>
        /// Action to create a new user.
        /// Accessible only by Admin users
        /// </summary>
        /// <param name="model">Model containing details of user to create</param>
        /// <returns>Redirects to the Account index on success</returns>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ManageAccountViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.RoleTypes = Util.GetSelectList(_identityService.GetRoles());
                return View(model);
            }
            var result = await _identityService.CreateUserAsync(model.Username, model.Password, model.Role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            model.RoleTypes = Util.GetSelectList(_identityService.GetRoles());
            return View(model);
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
                var result = await _identityService.SignInAsync(model.Username, model.Password);
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

        public IActionResult ManageBanner(BannerModel model)
        {

            if (_bannerService.GetBanner() == null)
            {
                _bannerService.CreateBanner(model);
            }
            else
            {
                _bannerService.UpdateBanner(model);
            }

            return PartialView("_ManageBanner", model);
        }

        /// <summary>
        /// Action to logout the currently logged in user
        /// </summary>
        /// <returns>Redirects to the homepage</returns>
        public async Task<IActionResult> Logout()
        {
            await _identityService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task Delete(string userId)
        {
            await _identityService.DeleteUserAsync(userId);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ManageAccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.RoleTypes = Util.GetSelectList(_identityService.GetRoles());
                return View(model);
            }
            var accountModel = new AccountModel
            {
                Username = model.Username,
                Role = model.Role,
                UserId = model.UserId
            };

            var result = await _identityService.UpdateUser(accountModel);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            model.RoleTypes = Util.GetSelectList(_identityService.GetRoles());
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid userId)
        {
            var user = await _identityService.GetUser(userId);
            var model = new ManageAccountViewModel
            {
                Username = user.UserName,
                Role = _identityService.GetRole(user),
                RoleTypes = Util.GetSelectList(_identityService.GetRoles()),
                UserId = user.Id
            };

            return View(model);
        }

        public async Task<IActionResult> AccountList()
        {
            var user = await _identityService.GetIdentityUserAsync(User);
            var accounts = _identityService.GetAccountList(user.Id);
            var viewModel = new AccountListViewModel
            {
                Accounts = accounts.Select(a =>
                    new AccountItemViewModel
                    {
                        AccountId = a.UserId,
                        Role = a.Role,
                        Username = a.Username
                    })
                .ToList(),
                UserId = user.Id
            };

            return View(viewModel);
        }

        public IActionResult Banner()
        {
            var banner = _bannerService.GetBanner();

            return View(banner);
        }

        public IActionResult InProgressListings()
        {
            var model = _listingService.GetInactiveInventoryList();

            return View(model);
        }


    }
}