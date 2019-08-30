using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DekkersAuto.Web.Controllers
{
    /// <summary>
    /// Controller handling actions for the Services page
    /// </summary>
    public class ServicesController : Controller
    {
        /// <summary>
        /// Action to display services page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}