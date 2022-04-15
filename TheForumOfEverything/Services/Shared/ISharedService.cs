using TheForumOfEverything.Models.Shared;

namespace TheForumOfEverything.Services.Shared
{
    public interface ISharedService
    {
        Task<bool> EmailSender(ContactViewModel model);
    }
}
