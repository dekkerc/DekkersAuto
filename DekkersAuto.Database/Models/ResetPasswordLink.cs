using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DekkersAuto.Database.Models
{
    /// <summary>
    /// Class to represent requests to reset password
    /// </summary>
    public class ResetPasswordLink
    {
        /// <summary>
        /// Gets and sets the Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Gets and sets the UserId
        /// </summary>
        [ForeignKey("User")]
        public string UserId { get; set; }
        
        /// <summary>
        /// Gets and sets the User
        /// </summary>
        public virtual IdentityUser User { get; set; }

        /// <summary>
        /// Gets and sets the Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
