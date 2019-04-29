using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web
{
    public static class Util
    {
        public static List<SelectListItem> GetColours()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "--Select Colour--",
                    Value = ""
                },
                new SelectListItem
                {
                    Text = "Red"
                },
                new SelectListItem
                {
                    Text = "Blue"
                }
            };
        }

        public static List<SelectListItem> GetTransmissions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Auto"
                },
                new SelectListItem
                {
                    Text = "Manual"
                }
            };
        }
    }
}
