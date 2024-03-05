using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class EmailSenderRepository : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {

            var SourceEmail = "timezone706@gmail.com";
            var Password = "123456789aA*";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials=false,
                Credentials = new NetworkCredential(SourceEmail, "fzjr aifk inpx wrau")
            };

            return client.SendMailAsync(
                new MailMessage(
                    from:SourceEmail,
                    to:email,
                    subject,
                    message
                    )
                );
        }
    }
}
