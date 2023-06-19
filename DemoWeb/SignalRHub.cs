using Microsoft.AspNetCore.SignalR;

namespace DemoWeb
{
    public class SignalRHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.Group(user).SendAsync("Connect", message);
            
        }

        public override async Task<Task> OnConnectedAsync()
        {
            var sessionId = Context.GetHttpContext()?.Request?.Query["sessionId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
            return base.OnConnectedAsync();
        }
    }
}
