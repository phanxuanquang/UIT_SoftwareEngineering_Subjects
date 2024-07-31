using API.Dtos;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room?> GetRoomById(Guid roomId);
        Task<Room?> GetRoomForConnection(string connectionId);
        void RemoveConnection(Connection connection);
        void AddRoom(Room room);
        Task<Room?> DeleteRoom(Guid roomId);
        Task<Room?> EditRoom(EditRoomDto edit);
        Task DeleteAllRoom();
        Task<PagedList<RoomDto>> GetAllRoomAsync(RoomParams roomParams);
        Task<RoomDto?> GetRoomDtoById(Guid roomId);
        Task UpdateCountMember(Guid roomId, int count);
        Task UpdateBlockChat(Guid roomId, bool block);
        void RemoveConnections(IEnumerable<Connection> connections);
    }
}
