using NWCodeFirstMVC.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NWCodeFirstMVC.Infrastructure.Services
{
    public class EmailService: IEmailService
    {

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mwhitfieldwork@gmail.com", "glmzwsgwfweyaqzc"),
                EnableSsl = true
            };

            var mail = new MailMessage("mwhitfieldwork@gmail.com", to, subject, body)
            {
                IsBodyHtml = true
            };

            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SMTP ERROR:");
                Console.WriteLine(ex.ToString());
                throw; // bubble it up to controller
            }
        }

        public async Task SendEmailAsync(string to, string subject, string body, byte[] attachmentBytes, string attachmentName)
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mwhitfieldwork@gmail.com", "glmzwsgwfweyaqzc"),
                EnableSsl = true
            };

            var mail = new MailMessage("mwhitfieldwork@gmail.com", to, subject, body)
            {
                IsBodyHtml = true
            };

            if (attachmentBytes != null)
            {
                mail.Attachments.Add(new Attachment(new MemoryStream(attachmentBytes), attachmentName));
            }

            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SMTP ERROR:");
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

    }
}
