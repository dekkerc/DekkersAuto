 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Account
{
    /// <summary>
    /// Holds all models required for the account index page
    /// </summary>
    public class AccountViewModel
    {
        /// <summary>
        /// Gets and sets the BannerModel
        /// Represents the model that will display the banner details
        /// </summary>
        public BannerViewModel BannerModel { get; set; }
        /// <summary>
        /// Gets and sets the ManageAccountModel
        /// Represents the model to manage the currently logged in user
        /// </summary>
        public ManageAccountViewModel ManageAccountModel { get; set; }
        /// <summary>
        /// Gets and sets the AccountList
        /// Represents the list of accounts to manage
        /// </summary>
        public AccountListViewModel AccountList { get; set; }
    }
}
