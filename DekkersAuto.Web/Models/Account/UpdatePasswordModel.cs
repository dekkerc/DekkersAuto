using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Account
{
    /// <summary>
    /// Model containing parameters required to update password
    /// </summary>
    public class UpdatePasswordModel
    {
        /// <summary>
        /// ID of user to update password on
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Gets and Sets the old password
        /// </summary>
        [Required,
        MinLength(8,
            ErrorMessage = "Password must be at least 8 characters"),
        DataType(DataType.Password),
        RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$",
            ErrorMessage = "Password must contain 1 uppercase, 1 lowercase, and 1 number"),
        Display(Name = "Old Password")]
        public string OldPassword { get; set; }
        /// <summary>
        /// Gets and sets the new password
        /// </summary>
        [Required,
        MinLength(8,
            ErrorMessage = "Password must be at least 8 characters"),
        DataType(DataType.Password),
        RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$",
            ErrorMessage = "Password must contain 1 uppercase, 1 lowercase, and 1 number"),
        Display(Name = "New Password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets and sets the confirm password
        /// </summary>
        [Required,
        MinLength(8,
            ErrorMessage = "Password must be at least 8 characters"),
        DataType(DataType.Password),
        RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$",
            ErrorMessage = "Password must contain 1 uppercase, 1 lowercase, and 1 number"),
        Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
