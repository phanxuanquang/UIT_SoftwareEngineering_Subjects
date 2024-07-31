using API.Dtos;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly PresenceTracker _tracker;
        public PresenceHub(PresenceTracker tracker)
        {
            _tracker = tracker;
        }
        public override async Task OnConnectedAsync()
        {
            var isOnline = await _tracker.UserConnected(new UserConnectionInfo(Context.User.GetUserId(), Context.User.GetDisplayName(), Guid.Empty), Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var isOffline = await _tracker.UserDisconnected(new UserConnectionInfo(Context.User.GetUserId(), Context.User.GetDisplayName(), Guid.Empty), Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
