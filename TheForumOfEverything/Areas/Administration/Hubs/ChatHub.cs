using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TheForumOfEverything.Areas.Administration.Models.ChatModels;

namespace TheForumOfEverything.Areas.Administration.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync(
                "NewMessage",
                new Message
                {
                    User = Context.User.Identity.Name,
                    Text = message,
                });
        }
    }
}
