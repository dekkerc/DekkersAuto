using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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