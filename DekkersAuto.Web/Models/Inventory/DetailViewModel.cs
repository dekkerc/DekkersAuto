using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Inventory
{
    public class DetailViewModel
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Transmission { get; set; }
        public string DriveTrain { get; set; }
        public string Colour { get; set; }
        public int Year { get; set; }
        public int Kilometers { get; set; }
        public string BodyType { get; set; }
        public string FuelType { get; set; }
        public int Doors { get; set; }
        public int Seats { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }

    }
}
