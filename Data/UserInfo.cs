using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    /// <summary>
    /// Class representing the information required to define a user
    /// Contains username and password
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Gets and sets the Primary Key
        /// </summary>
        [Key]
        public Guid Id { get; set; }
       
        /// <summary>
        /// Gets and sets the Role
        /// Represents the role of the user
        /// </summary>
        [Required]
        public RoleType Role { get; set; }
        /// <summary>
        /// Gets and sets the username
        /// Username used for login and display
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// Gets and sets the password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets and sets the LastLogin
        /// </summary>
        public DateTime? LastLogin { get; set; }
    }
}
