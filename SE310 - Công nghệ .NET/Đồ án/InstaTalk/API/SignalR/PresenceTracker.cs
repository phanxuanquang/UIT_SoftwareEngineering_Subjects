using API.Dtos;

namespace API.SignalR
{
    public class PresenceTracker
    {
        private static readonly Dictionary<UserConnectionInfo, List<string>> OnlineUsers = new Dictionary<UserConnectionInfo, List<string>>();

        public Task<bool> UserConnected(UserConnectionInfo user, string connectionId)
        {
            bool isOnline = false;
            lock (OnlineUsers)
            {
                var temp = OnlineUsers.FirstOrDefault(x => x.Key.UserID == user.UserID && x.Key.RoomId == user.RoomId);

                if (temp.Key == null)//chua co online
                {
                    OnlineUsers.Add(user, new List<string> { connectionId });
                    isOnline = true;
                }
                else if (OnlineUsers.ContainsKey(temp.Key))
                {
                    OnlineUsers[temp.Key].Add(connectionId);
                }
            }

            return Task.FromResult(isOnline);
        }

        public Task<bool> UserDisconnected(UserConnectionInfo user, string connectionId)
        {
            bool isOffline = false;
            lock (OnlineUsers)
            {
                var temp = OnlineUsers.FirstOrDefault(x => x.Key.UserID == user.UserID && x.Key.RoomId == user.RoomId);
                if (temp.Key == null)
                    return Task.FromResult(isOffline);

                OnlineUsers[temp.Key].Remove(connectionId);
                if (OnlineUsers[temp.Key].Count == 0)
                {
                    OnlineUsers.Remove(temp.Key);
                    isOffline = true;
                }
            }

            return Task.FromResult(isOffline);
        }

        public Task<UserConnectionInfo[]> GetOnlineUsers(Guid roomId)
        {
            UserConnectionInfo[] onlineUsers;
            lock (OnlineUsers)
            {
                onlineUsers = OnlineUsers.Where(u => u.Key.RoomId == roomId).Select(k => k.Key).ToArray();
            }

            return Task.FromResult(onlineUsers);
        }

        public Task<List<string>?> GetConnectionsForUser(UserConnectionInfo user)
        {
            List<string>? connectionIds = new List<string>();
            lock (OnlineUsers)
            {
                var temp = OnlineUsers.SingleOrDefault(x => x.Key.UserID == user.UserID && x.Key.RoomId == user.RoomId);
                if (temp.Key != null)
                {
                    connectionIds = OnlineUsers.GetValueOrDefault(temp.Key);
                }
            }
            return Task.FromResult(connectionIds);
        }

        public Task<List<string>> GetConnectionsForUserID(Guid userId)
        {
            List<string> connectionIds = new List<string>();
            lock (OnlineUsers)
            {
                // 1 user co nhieu lan dang nhap
                var listTemp = OnlineUsers.Where(x => x.Key.UserID == userId).ToList();
                if (listTemp.Count > 0)
                {
                    foreach (var user in listTemp)
                    {
                        connectionIds.AddRange(user.Value);
                    }
                }
            }
            return Task.FromResult(connectionIds);
        }

        public Task RemoveAllConnectionByRoomID(Guid roomID)
        {
            lock (OnlineUsers)
            {
                var temp = OnlineUsers.Where(x => x.Key.RoomId == roomID).ToList();

                foreach (var item in temp)
                    OnlineUsers.Remove(item.Key);
            }
            return Task.CompletedTask;
        }
    }
}
