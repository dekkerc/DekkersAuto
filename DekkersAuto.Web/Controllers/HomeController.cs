using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DekkersAuto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using DekkersAuto.Web.Models.Home;

namespace DekkersAuto.Web.Controllers
{
    public class HomeController : Controller
    {
        private DbService _dbService;

        public HomeController(DbService dbService)
        {
            _dbService = dbService;
        }
        public IActionResult Index()
        {
            var banner = _dbService.GetBanner();
            var viewModel = new BannerViewModel();
            if(banner != null)
            {
                viewModel.BannerId = banner.Id;
                viewModel.Text = banner.Text;
                viewModel.IsActive = banner.IsActive;
            }
            return View(viewModel);
        }
        
        /// <summary>
        /// Action to send an email from a customer to the DekkersAuto account
        /// </summary>
        /// <param name="model">Model containing parameters required to send an email</param>
        public void Contact(ContactViewModel model)
        {
            //TODO: Send email functionality

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
