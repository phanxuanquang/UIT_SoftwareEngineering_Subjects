using API.Dtos;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    [Authorize]
    public class StrangerHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StrangerTracker _tracker;

        public StrangerHub(IUnitOfWork unitOfWork, StrangerTracker tracker)
        {
            _unitOfWork = unitOfWork;
            _tracker = tracker;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            if (!Guid.TryParse(httpContext?.Request.Query["roomId"].ToString(), out var roomId))
                throw new HubException("Invalid query param");

            if (Context.User == null)
                throw new HubException("401");

            var userId = Context.User.GetUserId();
            var displayName = Context.User.GetDisplayName();

            var isOnline = await _tracker.UserConnected(new UserConnectionInfo(userId, displayName, Guid.Empty), Context.ConnectionId);

            var matches = await _unitOfWork.StrangerRepository.StrangerFindMatch();
            foreach (var group in matches.Where(item => !item.All(x => x.StrangerFilter?.CurrentRoom != null)))
            {
                var joinToRoom = group.FirstOrDefault(item => item.StrangerFilter?.CurrentRoom != null);
                if (joinToRoom == null)
                {
                    var clients = await Task.WhenAll(group.Select(item => _tracker.GetConnectionsForUserID(item.Id)));
                    var callClient = clients.SelectMany(item => item);
                    if (callClient != null && callClient.Any())
                        await Clients.Clients(callClient).SendAsync("JoinStrangerRoom", new
                        {
                            roomId = group.FirstOrDefault()?.Rooms?.FirstOrDefault()?.RoomId
                        });
                }
                else
                {
                    var clients = await Task.WhenAll(group.Where(item => item.StrangerFilter?.CurrentRoom == null).Select(item => _tracker.GetConnectionsForUserID(item.Id)));

                    var callClient = clients.SelectMany(item => item);
                    if (callClient != null && callClient.Any())
                        await Clients.Clients(callClient).SendAsync("JoinStrangerRoom", new
                        {
                            roomId = joinToRoom.StrangerFilter?.CurrentRoom?.RoomId
                        });
                }
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var isOffline = await _tracker.UserDisconnected(new UserConnectionInfo(Context.User.GetUserId(), Context.User.GetDisplayName(), Guid.Empty), Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
