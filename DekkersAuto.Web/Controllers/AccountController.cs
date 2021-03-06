﻿using System;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Services.Database;
using DekkersAuto.Services.Email;
using DekkersAuto.Services.Models;
using DekkersAuto.Web.Models;
using DekkersAuto.Web.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
//I, Christopher Dekker, student number 000311337, certify that all code
//submitted is my own work; that I have not copied it from any other source
//I also certify that I have not allowed by work to be copied by others
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
        public AccountController(DbService dbService,
            IdentityService identityService,
            BannerService bannerService,
            ListingService listingService)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ManageAccountViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.RoleTypes = Util.GetSelectList(_identityService.GetRoles());
                return View(model);
            }
            var result = await _identityService.CreateUserAsync(model.Username, model.Password, model.Role, model.Email);

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
                ModelState.AddModelError("", "Could not find username or password");
            }
            return View(model);
        }

        [Authorize]
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


        /// <summary>
        /// Action to delete a user account
        /// </summary>
        /// <param name="userId">Id of user to delete</param>
        /// <returns></returns>
        [Authorize]
        public async Task Delete(string userId)
        {
            await _identityService.DeleteUserAsync(userId);
        }

        /// <summary>
        /// Action to edit an Account
        /// </summary>
        /// <param name="model">Model of the account to be edited</param>
        /// <returns>Redirects to index action on success</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(ManageAccountViewModel model)
        {
            try
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
                    UserId = model.UserId,
                    Email = model.Email
                };

                var result = await _identityService.UpdateUser(accountModel);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception e)
            {
            }
            model.RoleTypes = Util.GetSelectList(_identityService.GetRoles());

            return View(model);
        }

        /// <summary>
        /// Action to retrieve the edit page
        /// </summary>
        /// <param name="userId">ID of user to edit</param>
        /// <returns>Edit View</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid userId)
        {
            var user = await _identityService.GetUserAsync(userId);
            var model = new ManageAccountViewModel
            {
                Username = user.UserName,
                Role = _identityService.GetRole(user),
                RoleTypes = Util.GetSelectList(_identityService.GetRoles()),
                UserId = user.Id,
                Email = user.Email
            };

            return View(model);
        }

        /// <summary>
        /// Action to get a list of users
        /// </summary>
        /// <returns></returns>
        [Authorize]
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

        /// <summary>
        /// Action to retrieve the edit banner page
        /// </summary>
        /// <returns>Banner to edit</returns>
        [Authorize]
        public IActionResult Banner()
        {
            var banner = _bannerService.GetBanner();

            return View(banner);
        }

        /// <summary>
        /// Action to retrieve list of inactive listings
        /// </summary>
        [Authorize]
        public IActionResult InProgressListings()
        {
            var model = _listingService.GetInactiveInventoryList();

            return View(model);
        }

        /// <summary>
        /// Action to retrieve update password view
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult UpdatePassword(Guid userId)
        {
            var viewModel = new UpdatePasswordModel
            {
                UserId = userId
            };
            return PartialView("_UpdatePassword", viewModel);
        }

        /// <summary>
        /// Action to update user password
        /// </summary>
        /// <param name="model">Model containing values of password to update</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_UpdatePassword", model);
            }
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Must match new password");
            }

            var result = await _identityService.UpdatePassword(model.UserId, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return new EmptyResult();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return PartialView("_UpdatePassword", model);
        }
    }
}