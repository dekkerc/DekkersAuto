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
        /// Retrieves the list of drivetrains
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetDriveTrains()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "2WD"
                },
                new SelectListItem
                {
                    Text = "AWD"
                },
                new SelectListItem
                {
                    Text = "FWD"
                },
                new SelectListItem
                {
                    Text = "RWD"
                },
                new SelectListItem
                {
                    Text = "4x4"
                }
            };
        }

        /// <summary>
        /// Retrieves the list of Fuel Types
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetFuelType()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Gas"
                },
                new SelectListItem
                {
                    Text = "Diesel"
                },
                new SelectListItem
                {
                    Text = "Hybrid"
                },
                new SelectListItem
                {
                    Text = "Electric"
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
