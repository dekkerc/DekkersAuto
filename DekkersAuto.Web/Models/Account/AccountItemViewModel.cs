using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Account
{
    /// <summary>
    /// Holds properties for a list display of accounts
    /// </summary>
    public class AccountItemViewModel
    {
        /// <summary>
        /// Gets and sets the AccountId
        /// Represents the Id of the account displayed
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// Gets and sets the username
        /// Represents the display of the username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Gets and sets the role
        /// Represents the display of the role
        /// </summary>
        public string Role { get; set; }

    }
}