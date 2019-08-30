using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web
{
    /// <summary>
    /// Utility class holding helper methods
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Retrieves the list of transmissions
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Converts a list of strings into a list of select list items
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetSelectList(List<string> list)
        {
            return list.Select(i => new SelectListItem { Text = i, Value = i }).ToList();
        }
    }
}
