using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DekkersAuto.Web.Models;
using DekkersAuto.Web.Models.Home;
using DekkersAuto.Services.Database;
using DekkersAuto.Services.Email;
using DekkersAuto.Services.Models;
//I, Christopher Dekker, student number 000311337, certify that all code
//submitted is my own work; that I have not copied it from any other source
//I also certify that I have not allowed by work to be copied by others
namespace DekkersAuto.Web.Controllers
{
    /// <summary>
    /// Controller to handle homepage interactions
    /// </summary>
    public class HomeController : Controller
    {
        private BannerService _bannerService;
        private IEmailService _emailService;

        /// <summary>
        /// Default home constructor
        /// Taking all required services with dependency injection
        /// </summary>
        /// <param name="emailService"></param>
        /// <param name="bannerService"></param>
        public HomeController(IEmailService emailService, BannerService bannerService)
        {
            _emailService = emailService;
            _bannerService = bannerService;
        }
        /// <summary>
        /// Action to retrieve homepage
        /// </summary>
        /// <returns></returns>
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
            await _emailService.SendEmail(model.Email, model.Subject, model.Message);

        }
    }
}
