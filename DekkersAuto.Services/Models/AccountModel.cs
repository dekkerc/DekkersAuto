using System;
using System.Collections.Generic;
using System.Text;

namespace DekkersAuto.Services.Models
{
    /// <summary>
    /// Model representing a user account 
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// Gets and sets the UserId
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Gets and sets the Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Gets and sets the role
        /// </summary>
        public string Role { get; set; }
    }
}
