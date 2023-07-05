
using AvaloniaChat.Infrastructure;
using AvaloniaChat.Domain;
using AvaloniaChat.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


namespace AvaloniaChat.Backend.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        

        private readonly ChatDbContext _chatDbContext;
        public ChatHub(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }

        //public async Task SendMessage(string user, string message, Guid groupId)
        //{
        //    await Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", user, message);
        //    await Clients.Group(Context.ConnectionId).SendAsync("ReceiveMessage", user, message);
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);

        //}
        
        /// <summary>
        /// when the user enters the chat for the first time
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public async Task JoinChat(string user, string groupName)
        {
            await Clients.Group(groupName).SendAsync("Join", user);
        }
        /// <summary>
        ///  when the user leave the chat
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task LeaveChat(string user, string groupName)
        {
            await Clients.Group(groupName).SendAsync("Leave", user);
        }

        public override Task OnConnectedAsync()
        {
            var userGroup = _chatDbContext.UserGroups.Where(x=> x.User.Username == Context.User.Identity.Name).Select(x => x.GroupId.ToString()).ToList();

            foreach (var group in userGroup)
            {
            Groups.AddToGroupAsync(Context.ConnectionId, group);

            }
            return base.OnConnectedAsync();
        }
    }
}
