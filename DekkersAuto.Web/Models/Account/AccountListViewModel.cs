using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Account
{
    /// <summary>
    /// Represents a list of accounts to be managed by an admin user
    /// </summary>
    public class AccountListViewModel
    {
        /// <summary>
        /// Gets and sets the UserId
        /// Represents the Id of the logged in user
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Gets and sets the Accounts
        /// Represents the list of accounts the logged in user can manage
        /// </summary>
        public List<AccountItemViewModel> Accounts { get; set; }
    }
}
