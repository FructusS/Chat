using AvaloniaChat.Backend.Context;
using AvaloniaChat.Backend.Models;
using Microsoft.AspNetCore.Authorization;
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

        public async Task SendMessage(string user, string message, int groupId, int userId)
        {
            await Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", user, message);
            _chatDbContext.Messages.Add(new Message
            {
                MessageText = message,
                UsergroupId = groupId,
                UserId = userId
                
            });
        }
        public async Task JoinChat(string user)
        {

            await Clients.Group(groupname).SendAsync("Join", user);

        }
        public async Task LeaveChat(string user)
        {

            await Clients.Group(groupname).SendAsync("Leave", user);

        }
    }
}
