using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly PresenceTracker _presenceTracker;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserShareScreenTracker _shareScreenTracker;
        private readonly UserManager<AppUser> _userManager;

        public ChatHub(IUnitOfWork unitOfWork,
            UserShareScreenTracker shareScreenTracker,
            PresenceTracker presenceTracker,
            IHubContext<PresenceHub> presenceHub,
            UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _presenceTracker = presenceTracker;
            _presenceHub = presenceHub;
            _shareScreenTracker = shareScreenTracker;
            _userManager = userManager;
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

            var userConnection = new UserConnectionInfo(userId, displayName, roomId);

            await _presenceTracker.UserConnected(userConnection, Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());//khi user click vao room se join vao
            await AddConnectionToGroup(roomId); // luu db DbSet<Connection> de khi disconnect biet

            //var usersOnline = await _unitOfWork.UserRepository.GetUsersOnlineAsync(currentUsers);
            var oneUserOnline = await _unitOfWork.UserRepository.GetMemberAsync(userId);
            await Clients.Group(roomId.ToString()).SendAsync("UserOnlineInGroup", oneUserOnline);

            var currentUsers = await _presenceTracker.GetOnlineUsers(roomId);
            await _unitOfWork.RoomRepository.UpdateCountMember(roomId, currentUsers.Length);
            await _unitOfWork.Complete();

            var currentConnections = await _presenceTracker.GetConnectionsForUser(userConnection);
            await _presenceHub.Clients.AllExcept(currentConnections).SendAsync("CountMemberInGroup",
                   new { roomId = roomId, countMember = currentUsers.Length });

            //share screen user vao sau cung
            var userIsSharing = await _shareScreenTracker.GetUserIsSharing(roomId);
            if (userIsSharing != null)
            {
                var currentBeginConnectionsUser = await _presenceTracker.GetConnectionsForUser(userIsSharing);
                if (currentBeginConnectionsUser?.Count > 0)
                    await Clients.Clients(currentBeginConnectionsUser).SendAsync("OnShareScreenLastUser", new { userIdTo = userId, isShare = true });
                await Clients.Caller.SendAsync("OnUserIsSharing", userIsSharing.DisplayName);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.User == null)
                throw new HubException("401");

            var userId = Context.User.GetUserId();
            var displayName = Context.User.GetDisplayName();
            var group = await RemoveConnectionFromGroup();

            if (group == null)
                throw new HubException("Room doesn't exists");

            var isOffline = await _presenceTracker.UserDisconnected(new UserConnectionInfo(userId, displayName, group.RoomId), Context.ConnectionId);

            await _shareScreenTracker.DisconnectedByUser(userId, group.RoomId);
            if (isOffline)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.RoomId.ToString());
                var temp = await _unitOfWork.UserRepository.GetMemberAsync(userId);
                await Clients.Group(group.RoomId.ToString()).SendAsync("UserOfflineInGroup", temp);

                var currentUsers = await _presenceTracker.GetOnlineUsers(group.RoomId);

                await _unitOfWork.RoomRepository.UpdateCountMember(group.RoomId, currentUsers.Length);
                await _unitOfWork.Complete();

                await _presenceHub.Clients.All.SendAsync("CountMemberInGroup",
                       new { roomId = group.RoomId, countMember = currentUsers.Length });

                /*if (currentUsers.Length < 1 || Context.User.IsInRole("Host") || Context.User.IsInRole("Admin"))
                    await LockdownRoom(group.RoomId);*/
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            if (Context.User == null)
                throw new HubException("401");

            var userId = Context.User.GetUserId();
            var displayName = Context.User.GetDisplayName();
            var group = await _unitOfWork.RoomRepository.GetRoomForConnection(Context.ConnectionId);

            if (group == null)
                throw new HubException("group == null");

            if (group.BlockedChat)
                throw new HubException("Chat has been blocked by host");

            var message = new MessageDto
            {
                SenderUserID = userId,
                SenderDisplayName = displayName,
                Content = createMessageDto.Content,
                MessageSent = DateTime.UtcNow
            };
            //Luu message vao db
            //code here
            //send meaasge to group
            await Clients.Group(group.RoomId.ToString()).SendAsync("NewMessage", message);
        }

        [Authorize(Roles = "Admin,Host")]
        public async Task BlockChat(bool block)
        {
            if (Context.User == null)
                throw new HubException("401");

            var group = await _unitOfWork.RoomRepository.GetRoomForConnection(Context.ConnectionId);

            if (group != null)
            {
                await _unitOfWork.RoomRepository.UpdateBlockChat(group.RoomId, block);
                await _unitOfWork.Complete();
                await Clients.Group(group.RoomId.ToString()).SendAsync("OnBlockChat", new { block });
            }
        }

        public async Task MuteMicro(bool muteMicro)
        {
            var group = await _unitOfWork.RoomRepository.GetRoomForConnection(Context.ConnectionId);
            if (group != null)
            {
                if (Context.User == null)
                    throw new HubException("401");

                await Clients.Group(group.RoomId.ToString()).SendAsync("OnMuteMicro", new
                {
                    userId = Context.User.GetUserId(),
                    mute = muteMicro
                });
            }
            else
            {
                throw new HubException("group == null");
            }
        }

        [Authorize(Roles = "Admin,Host")]
        public async Task MuteAllMicro(Guid userId, bool muteMicro)
        {
            var group = await _unitOfWork.RoomRepository.GetRoomForConnection(Context.ConnectionId);
            if (group != null)
            {
                if (!group.Connections.Any(item => item.UserID == userId))
                    throw new HubException("user_id not in current group");

                await Clients.Group(group.RoomId.ToString()).SendAsync("OnMuteAllMicro", new
                {
                    userId = userId,
                    mute = muteMicro
                });
            }
            else
            {
                throw new HubException("group == null");
            }
        }

        public async Task MuteCamera(bool muteCamera)
        {
            var group = await _unitOfWork.RoomRepository.GetRoomForConnection(Context.ConnectionId);
            if (group != null)
            {
                if (Context.User == null)
                    throw new HubException("401");

                await Clients.Group(group.RoomId.ToString()).SendAsync("OnMuteCamera", new
                {
                    userId = Context.User.GetUserId(),
                    mute = muteCamera
                });
            }
            else
            {
                throw new HubException("group == null");
            }
        }

        public async Task ShareScreen(Guid roomid, bool isShareScreen)
        {
            if (Context.User == null)
                throw new HubException("401");

            var userConnection = new UserConnectionInfo(Context.User.GetUserId(), Context.User.GetDisplayName(), roomid);
            if (isShareScreen)//true is doing share
            {
                await _shareScreenTracker.UserConnectedToShareScreen(userConnection);
                await Clients.Group(roomid.ToString()).SendAsync("OnUserIsSharing", Context.User.GetDisplayName());
            }
            else
            {
                await _shareScreenTracker.UserDisconnectedShareScreen(userConnection);
            }
            await Clients.Group(roomid.ToString()).SendAsync("OnShareScreen", isShareScreen);
        }

        public async Task ShareScreenToUser(Guid roomid, Guid userId, bool isShare)
        {
            var currentBeginConnectionsUser = await _presenceTracker.GetConnectionsForUser(new UserConnectionInfo(userId, string.Empty, roomid));
            if (currentBeginConnectionsUser?.Count > 0)
                await Clients.Clients(currentBeginConnectionsUser).SendAsync("OnShareScreen", isShare);
        }

        [Authorize(Roles = "Admin,Host")]
        public async Task KickMember(Guid roomId, Guid userId)
        {
            var connections = await _presenceTracker.GetConnectionsForUser(new UserConnectionInfo(userId, string.Empty, roomId));
            if (connections != null && connections.Count > 0)
            {
                await Clients.Users(userId.ToString()).SendAsync("OnIsKicked", new { userId, roomId });
                await Task.WhenAll(connections.Select(cid => Groups.RemoveFromGroupAsync(cid, roomId.ToString())));
                var u = await _unitOfWork.UserRepository.UpdateLocked(userId);
                var temp = await _unitOfWork.UserRepository.GetMemberAsync(userId);
                await Clients.Group(roomId.ToString()).SendAsync("UserOfflineInGroup", temp);

                var currentUsers = await _presenceTracker.GetOnlineUsers(roomId);

                await _unitOfWork.RoomRepository.UpdateCountMember(roomId, currentUsers.Length);
                await _unitOfWork.Complete();

                await _presenceHub.Clients.All.SendAsync("CountMemberInGroup",
                       new { roomId = roomId, countMember = currentUsers.Length });
            }
        }

        #region Private
        private async Task<Room?> RemoveConnectionFromGroup()
        {
            var group = await _unitOfWork.RoomRepository.GetRoomForConnection(Context.ConnectionId);
            var connection = group?.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

            if (connection == null)
                throw new HubException("Room doesn't exists");

            _unitOfWork.RoomRepository.RemoveConnection(connection);

            if (await _unitOfWork.Complete()) return group;

            throw new HubException("Fail to remove connection from room");
        }

        private async Task<Room?> AddConnectionToGroup(Guid roomId)
        {
            if (Context.User == null)
                throw new HubException("401");

            var group = await _unitOfWork.RoomRepository.GetRoomById(roomId);
            var connection = new Connection(Context.ConnectionId, Context.User.GetUserId());
            if (group != null)
            {
                group.Connections.Add(connection);
            }

            if (await _unitOfWork.Complete()) return group;

            throw new HubException("Failed to add connection to room");
        }

        private async Task LockdownRoom(Guid roomId)
        {
            await Clients.Group(roomId.ToString()).SendAsync("LockdownRoom");
            var connectionsInRoom = await _unitOfWork.RoomRepository.GetRoomById(roomId);
            await _presenceTracker.RemoveAllConnectionByRoomID(roomId);
            if (connectionsInRoom != null)
            {
                await Task.WhenAll(connectionsInRoom.Connections.Select(item => Groups.RemoveFromGroupAsync(item.ConnectionId, roomId.ToString())));
                _unitOfWork.RoomRepository.RemoveConnections(connectionsInRoom.Connections);
                await _unitOfWork.RoomRepository.DeleteRoom(roomId);
                foreach (var user in connectionsInRoom.Connections)
                {
                    var dbUser = await _unitOfWork.UserRepository.GetUserByIdAsync(user.UserID);
                    if (dbUser != null) await _userManager.DeleteAsync(dbUser);
                }
            }
            await _unitOfWork.Complete();
        }
        #endregion
    }
}
