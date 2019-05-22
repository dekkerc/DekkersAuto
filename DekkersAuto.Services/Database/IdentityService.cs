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
    public class IdentityService : DbServiceBase
    {
        /// <summary>
        /// Gets and sets the RoleManager
        /// Rolemanager handles creation, manipulation and assignment of roles
        /// </summary>
        private RoleManager<IdentityRole> RoleManager { get; set; }
        
        /// <summary>
        /// Gets and sets the UserManager
        /// Usermanager handles creation and manipulation of users
        /// </summary>
        private UserManager<IdentityUser> UserManager { get; set; }

        private SignInManager<IdentityUser> SignInManager { get; set; }

        public IdentityService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext db) : base(db)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public List<string> GetRoles()
        {
            return RoleManager.Roles.Select(r => r.Name).ToList();
        }

        public async Task<bool> CreateUserAsync(string username, string password, string role)
        {
            var user = new IdentityUser
            {
                UserName = username
            };
            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var newUser = await UserManager.FindByNameAsync(user.UserName);
                var roleResult = await UserManager.AddToRoleAsync(newUser, role);
            }
            return result.Succeeded;
        }

        public async Task<IdentityUser> GetUser(Guid accountId)
        {
            return await UserManager.FindByIdAsync(accountId.ToString());
        }

        public string GetRole(IdentityUser user)
        {
            return _db.UserRoles.SingleOrDefault(ur => ur.UserId == user.Id)?.RoleId;
        }

        public async Task UpdateUser(AccountModel model)
        {
            var user = await UserManager.FindByIdAsync(model.UserId);

            user.UserName = model.Username;
            await UserManager.UpdateAsync(user);
            var roles = await UserManager.GetRolesAsync(user);

            if (roles.Count == 0)
            {
                await UserManager.AddToRoleAsync(user, model.Role);
            }
            else if (roles.FirstOrDefault() != model.Role)
            {
                await UserManager.RemoveFromRoleAsync(user, roles.FirstOrDefault());
                await UserManager.AddToRoleAsync(user, model.Role);
            }
            _db.SaveChanges();
        }

        public async Task DeleteUserAsync(string userId)
        {
            await UserManager.DeleteAsync(await UserManager.FindByIdAsync(userId));
        }



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

        public async Task<IdentityUser> GetIdentityUserAsync(ClaimsPrincipal user)
        {
            return await UserManager.GetUserAsync(user);
        }

        public async Task<SignInResult> SignInAsync(string username, string password)
        {
            return await SignInManager.PasswordSignInAsync(username, password, false, false);
        }

        public async Task SignOutAsync()
        {
            await SignInManager.SignOutAsync();
        }

    }
}
