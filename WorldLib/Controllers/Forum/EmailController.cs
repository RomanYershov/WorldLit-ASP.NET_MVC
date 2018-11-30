using ActionMailer.Net.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ActionMailer.Net;
using WorldLib.Models;

namespace WorldLib.Controllers.Forum
{
    public class EmailController : MailerBase
    {
        // GET: Email
        public EmailResult SendEmail(EmailModel model)
        {
            To.Add(model.To);
            From = model.From;
            Subject = model.Subject;
            return  Email("SendEmail" ,model);
        }

        public EmailResult SendEmail(IEnumerable<EmailModel> emails)
        {
            foreach (var email in emails)
            {
                To.Add(email.To);
                From = email.From;
                Subject = email.Subject;
                return Email("", email);
            }

            return null;
        }

       
    }

}