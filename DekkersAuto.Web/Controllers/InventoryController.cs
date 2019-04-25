using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DekkersAuto.Web.Data;
using DekkersAuto.Web.Models.Inventory;
using Microsoft.AspNetCore.Mvc;

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
            var model = new InventoryViewModel
            {
                Filter = new FilterViewModel
                {
                }
            };


            return View();
        }
    }
}