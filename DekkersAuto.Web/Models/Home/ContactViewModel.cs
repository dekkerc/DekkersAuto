using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekkersAuto.Web.Models.Home
{
    /// <summary>
    /// Holds data required to fill a contact form and send an email
    /// </summary>
    public class ContactViewModel
    {
        /// <summary>
        /// Gets and sets the Email
        /// Represents the email address the reply should go to
        /// </summary>
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        /// <summary>
        /// Gets and sets the Message
        /// Represents the message sent
        /// </summary>
        [Required]
        public string Message { get; set; }
        
        /// <summary>
        /// Gets and sets the Subject
        /// Represents the subject header of the email
        /// </summary>
        public string Subject { get; set; }
    }
}
