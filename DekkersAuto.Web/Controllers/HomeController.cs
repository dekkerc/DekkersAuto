using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DekkersAuto.Web.Models;
using DekkersAuto.Web.Models.Home;
using DekkersAuto.Services.Database;
using DekkersAuto.Services.Email;
using DekkersAuto.Services.Models;

namespace DekkersAuto.Web.Controllers
{
    public class HomeController : Controller
    {
        private DbService _dbService;
        private BannerService _bannerService;
        private IEmailService _emailService;

        public HomeController(DbService dbService, IEmailService emailService, BannerService bannerService)
        {
            _dbService = dbService;
            _emailService = emailService;
            _bannerService = bannerService;
        }
        public IActionResult Index()
        {
            var banner = _bannerService.GetBanner();
            
            return View(banner);
        }
        
        /// <summary>
        /// Action to send an email from a customer to the DekkersAuto account
        /// </summary>
        /// <param name="model">Model containing parameters required to send an email</param>
        public async Task Contact(ContactViewModel model)
        {
            await _emailService.SendEmail(model.Email, "", model.Message);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
