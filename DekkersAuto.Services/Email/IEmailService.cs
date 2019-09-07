using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
