using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Account
{
    /// <summary>
    /// View model holding all fields required to create a user
    /// </summary>
    public class ManageAccountViewModel
    {
        /// <summary>
        /// Gets and sets the Password
        /// Password must contain at least 1 lowercase, 1 uppercase, 1 number and be a minimum of 8 characters
        /// </summary>
        [Required, MinLength(8), DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// Gets and sets the username
        /// Represents the name of the user to be created
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// Gets and sets the RoleTypes
        /// Represents the list of roles a user can be created with
        /// </summary>
        public List<SelectListItem> RoleTypes { get; set; }
        /// <summary>
        /// Gets and sets the Role
        /// Represents the role the user will be created with. Determines the permissions available to the user
        /// </summary>
        [Required]
        public string Role { get; set; }
    }
}
