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
                Credentials = new NetworkCredential("mwhitfieldwork@gmail.com", "glmz wsgw fwey aqzc"),
                EnableSsl = true
            };

            var mail = new MailMessage("mwhitfieldwork@gmail.com", to, subject, body)
            {
                IsBodyHtml = true
            };

            await smtp.SendMailAsync(mail);
        }
        public async Task SendEmailAsync(string to, string subject, string body, byte[] attachmentBytes, string attachmentName)
        {
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mwhitfieldwork@gmail.com", "glmz wsgw fwey aqzc"),
                EnableSsl = true
            };

            var mail = new MailMessage("yourEmail@gmail.com", to, subject, body)
            {
                IsBodyHtml = true
            };

            if (attachmentBytes != null)
            {
                mail.Attachments.Add(new Attachment(new MemoryStream(attachmentBytes), attachmentName));
            }

            await smtp.SendMailAsync(mail);
        }

    }
}
