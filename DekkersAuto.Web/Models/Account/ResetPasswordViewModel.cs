using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Account
{
    /// <summary>
    /// Model holding properties for resetting password from email 
    /// </summary>
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Id of the reset request
        /// </summary>
        public Guid ResetId { get; set; }
        /// <summary>
        /// Gets and sets the UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets and sets the Password
        /// </summary>
        [Required,
        MinLength(8,
            ErrorMessage = "Password must be at least 8 characters"),
        DataType(DataType.Password),
        RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$",
            ErrorMessage = "Password must contain 1 uppercase, 1 lowercase, and 1 number"),
        Display(Name = "Password")]
        public string Password { get; set; }
        /// <summary>
        /// Gets and sets the Confirm Password
        /// </summary>
        [Required,
        MinLength(8,
            ErrorMessage = "Password must be at least 8 characters"),
        DataType(DataType.Password),
        RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$",
            ErrorMessage = "Password must contain 1 uppercase, 1 lowercase, and 1 number"),
        Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Gets and sets Is Valid
        /// </summary>
        public bool IsValid { get; set; }
    }
}
