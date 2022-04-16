using System.Net;
using System.Net.Mail;
using TheForumOfEverything.Models.Shared;
using System.Configuration;

namespace TheForumOfEverything.Services.Shared
{
    public class SharedService : ISharedService
    {
        private IConfiguration configuration;
        public SharedService(IConfiguration config)
        {
            configuration = config;

        }
        public async Task EmailSender(ContactViewModel model)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(model.From);
                mail.To.Add(model.To);
                mail.Subject = model.Subject;
                mail.Body = $"<h1>Subject: {model.Subject}</h1> <p>From: {model.From}</p> <p>Name: {model.Name}</p>  <p>Phone: {model.PhoneNumber}</p>  <p>Content: {model.Body}</p>";
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    string pass = configuration.GetValue<string>("EmailPassword");
                    smtp.Credentials = new NetworkCredential("theforumofeverythingemail@gmail.com", pass);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
