using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Account
{
    public class UpdatePasswordModel
    {
        public Guid UserId { get; set; }
        [Required,
        MinLength(8,
            ErrorMessage = "Password must be at least 8 characters"),
        DataType(DataType.Password),
        RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$",
            ErrorMessage = "Password must contain 1 uppercase, 1 lowercase, and 1 number"),
        Display(Name = "Old Password")]
        public string OldPassword { get; set; }
        [Required,
        MinLength(8,
            ErrorMessage = "Password must be at least 8 characters"),
        DataType(DataType.Password),
        RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$",
            ErrorMessage = "Password must contain 1 uppercase, 1 lowercase, and 1 number"),
        Display(Name = "New Password")]
        public string NewPassword { get; set; }
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
