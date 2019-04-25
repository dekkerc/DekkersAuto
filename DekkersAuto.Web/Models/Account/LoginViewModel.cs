using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Account
{
    /// <summary>
    /// Model containing information required for a user to login
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets and sets the Username
        /// Represents the username of the user attempting to login
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// Gets and sets the Password
        /// Represents the password of the user attempting to login
        /// </summary>
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets and sets the Return URL
        /// Represents where the login action should redirect to
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
