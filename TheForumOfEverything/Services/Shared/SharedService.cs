using System.Net;
using System.Net.Mail;
using TheForumOfEverything.Models.Shared;

namespace TheForumOfEverything.Services.Shared
{
    public class SharedService : ISharedService
    {
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
                    smtp.Credentials = new NetworkCredential("theforumofeverythingemail@gmail.com", "B&L_=puDxjuDdY?gf=DgE@g=+n2T#a3NUY@WmZrT?ScJ_+-6w#e^tHVf9sa??JLSA%34S*m2mqAfb?Kgp653UR@yL8$mwZBA3zbL");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
