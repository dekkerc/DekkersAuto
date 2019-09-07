using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//I, Christopher Dekker, student number 000311337, certify that all code
//submitted is my own work; that I have not copied it from any other source
//I also certify that I have not allowed by work to be copied by others
namespace DekkersAuto.Web.Controllers
{
    /// <summary>
    /// Controller handling actions for the About Page
    /// </summary>
    public class AboutController : Controller
    {
        /// <summary>
        /// Displays the About page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}