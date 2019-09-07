using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//I, Christopher Dekker, student number 000311337, certify that all code
//submitted is my own work; that I have not copied it from any other source
//I also certify that I have not allowed by work to be copied by others
namespace DekkersAuto.Services.Email
{
    /// <summary>
    /// Service to handle sending of emails
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendEmail(string email, string subject, string message);
    }
}
