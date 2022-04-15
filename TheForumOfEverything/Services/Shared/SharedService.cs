using System.Net.Mail;
using TheForumOfEverything.Models.Shared;

namespace TheForumOfEverything.Services.Shared
{
    public class SharedService : ISharedService
    {
        public async Task<bool> EmailSender(ContactViewModel model)
        {
            try
            {
                string from = model.From;
                string to = model.To;
                string subject = model.Subject;
                string body = model.Body;
                string phoneNumber = model.PhoneNumber;
                string name = model.Name;

                MailMessage message = new MailMessage(from, to, subject, name + phoneNumber + body);
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("theforumofeverythingemail@gmail.com", "Alex3021081");
                client.Send(message);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
