
using AvaloniaChat.Infrastructure;
using AvaloniaChat.Domain;
using AvaloniaChat.Domain.Models;
using Microsoft.AspNetCore.SignalR;


namespace AvaloniaChat.Backend.Hubs
{
    
    public class ChatHub : Hub
    {
        private string groupname = "tets";

        private readonly ChatDbContext _chatDbContext;
        public ChatHub(ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }
        public Task JoinUserGroup()
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupname);
        }
        public Task LeaveUserGroup()
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupname);
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

        public async Task JoinChat(string user)
        {
            await Clients.Group(groupname).SendAsync("Join", user);
        }
        /// <summary>
        ///  when the user leave the chat
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task LeaveChat(string user)
        {
            await Clients.Group(groupname).SendAsync("Leave", user);
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, groupname);
            return base.OnConnectedAsync();
        }
    }
}
