
using AvaloniaChat.Infrastructure;
using AvaloniaChat.Domain;
using AvaloniaChat.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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

        /// <summary>
        /// when the user enters the group for the first time
        /// </summary>
        /// <param name="user"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task JoinChat(string user, string groupName)
        {
            await Clients.Group(groupName).SendAsync("Join", user);
        }
        /// <summary>
        /// when the user leave from group
        /// </summary>
        /// <param name="user"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task LeaveChat(string user, string groupName)
        {
            await Clients.Group(groupName).SendAsync("Leave", user);
        }

        public override Task OnConnectedAsync()
        {
            var userGroup = _chatDbContext.UserGroups.Where(x => x.User.Username == Context.User.Identity.Name).Select(x => x.GroupId.ToString()).ToList();

            foreach (var group in userGroup)
            {
                Groups.AddToGroupAsync(Context.ConnectionId, group);
            }
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userGroup = _chatDbContext.UserGroups.Where(x => x.User.Username == Context.User.Identity.Name).Select(x => x.GroupId.ToString()).ToList();

            foreach (var group in userGroup)
            {
                Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
