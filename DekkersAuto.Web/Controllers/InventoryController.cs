using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Web.Data;
using DekkersAuto.Web.Models.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DekkersAuto.Web.Controllers
{
    public class InventoryController : Controller
    {
        private ApplicationDbContext _db;

        public InventoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var model = new InventoryViewModel();

            model.Filter = new FilterViewModel
            {
                ModelList = _db.Models.Select(m => new SelectListItem { Value = m.Name }).ToList(),
                MakeList = _db.Makes.Select(m => new SelectListItem { Value = m.Name }).ToList(),
                ColourList = Util.GetColours()
            };
            

            return View(model);
        }
    }
}