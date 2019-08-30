using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DekkersAuto.Services.Email
{
    /// <summary>
    /// Service to handle the sending of emails
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Method to send an email to the Dekkers Auto email 
        /// Containing the customer's message, and an email address they can be reached at
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendEmail(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["Email:Email"],
                    Password = _configuration["Email:Password"]
                };

                client.Credentials = credential;
                client.Host = _configuration["Email:Host"];
                client.Port = int.Parse(_configuration["Email:Port"]);
                client.EnableSsl = true;
            
            using (var emailMessage = new MailMessage())
            {
                emailMessage.To.Add(new MailAddress(_configuration["Email:Email"]));
                emailMessage.From = new MailAddress(email);
                emailMessage.Subject = subject;
                emailMessage.Body = $"You have received an email from {email}. \n\n{message}";
                client.Send(emailMessage);
            }
            }
        }
    }
}
