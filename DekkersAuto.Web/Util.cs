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
                    Value=  "Red"
                },
                new SelectListItem
                {
                    Value=  "Blue"
                }
            };
        }
    }
}
