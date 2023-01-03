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
        public async Task CreateUserGroup(string groupTitle)
        {
            _chatDbContext.Groups.Add(new Group() { GroupTitle = groupTitle });
            await _chatDbContext.SaveChangesAsync();
        }
        public Task LeaveUserGroup()
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupname);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.Group(groupname).SendAsync("ReceiveMessage", user, message);

        }

    }
}
