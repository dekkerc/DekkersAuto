using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DekkersAuto.Database;
using DekkersAuto.Services.Models;
using Microsoft.AspNetCore.Identity;

namespace DekkersAuto.Services.Database
{
    /// <summary>
    /// Service to handle Identity related actions
    /// </summary>
    public class IdentityService : DbServiceBase
    {
        /// <summary>
        /// Gets and sets the RoleManager
        /// Rolemanager handles creation, manipulation and assignment of roles
        /// </summary>
        private RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Gets and sets the UserManager
        /// Usermanager handles creation and manipulation of users
        /// </summary>
        private UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Gets and sets the signin manager
        /// Handles authorization of users
        /// </summary>
        private SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Identity service constructor accepting all required services
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="db"></param>
        public IdentityService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext db) : base(db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Retrieves all available user roles
        /// </summary>
        /// <returns></returns>
        public List<string> GetRoles()
        {
            return _roleManager.Roles.Select(r => r.Name).ToList();
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateUserAsync(string username, string password, string role)
        {
            var user = new IdentityUser
            {
                UserName = username
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var newUser = await _userManager.FindByNameAsync(user.UserName);
                var roleResult = await _userManager.AddToRoleAsync(newUser, role);
            }
            return result;
        }

        /// <summary>
        /// Gets a user by user id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<IdentityUser> GetUser(Guid accountId)
        {
            return await _userManager.FindByIdAsync(accountId.ToString());
        }

        /// <summary>
        /// Gets the role of a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GetRole(IdentityUser user)
        {
            return _db.UserRoles.SingleOrDefault(ur => ur.UserId == user.Id)?.RoleId;
        }

        /// <summary>
        /// Updates a user's information
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateUser(AccountModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            user.UserName = model.Username;
            var result = await _userManager.UpdateAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Count == 0)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            else if (roles.FirstOrDefault() != model.Role)
            {
                await _userManager.RemoveFromRoleAsync(user, roles.FirstOrDefault());
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            _db.SaveChanges();
            return result;
        }
        /// <summary>
        /// Updates a user's password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdatePassword(Guid userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            await _db.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Deletes a user account
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task DeleteUserAsync(string userId)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(userId));
        }

        /// <summary>
        /// Gets a list of user accounts and their roles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<AccountModel> GetAccountList(string id)
        {
            var accountList = _db.UserRoles.Join(
                _db.Users,
                userRole => userRole.UserId,
                user => user.Id,
                (userRole, user) => new
                {
                    Id = user.Id,
                    Username = user.UserName,
                    RoleId = userRole.RoleId
                }).Join(
                _db.Roles,
                user => user.RoleId,
                role => role.Id,
                (user, role) => new
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = role.Name
                })
                .Where(u => u.Id != id)
                .Select(u => new AccountModel { UserId = u.Id, Username = u.Username, Role = u.Role })
                .ToList();
            return accountList;
        }

        /// <summary>
        /// Gets an identity user from their Claims
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IdentityUser> GetIdentityUserAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        /// <summary>
        /// Signs a user in
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<SignInResult> SignInAsync(string username, string password)
        {
            return await _signInManager.PasswordSignInAsync(username, password, false, false);
        }

        /// <summary>
        /// Signs a user out
        /// </summary>
        /// <returns></returns>
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
