using API.Data;
using API.Dtos;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RoomRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<Room?> GetRoomById(Guid roomId)
        {
            return _context.Rooms.Include(x => x.Connections).FirstOrDefaultAsync(x => x.RoomId == roomId);
        }

        public Task<RoomDto?> GetRoomDtoById(Guid roomId)
        {
            return _context.Rooms.Where(r => r.RoomId == roomId).ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();//using Microsoft.EntityFrameworkCore;
        }

        public Task<Room?> GetRoomForConnection(string connectionId)
        {
            return _context.Rooms.Include(x => x.Connections)
                .Where(x => x.Connections.Any(c => c.ConnectionId == connectionId))
                .FirstOrDefaultAsync();
        } 

        public void RemoveConnection(Connection connection)
        {
            _context.Connections.Remove(connection);
        }

        public void RemoveConnections(IEnumerable<Connection> connections)
        {
            _context.Connections.RemoveRange(connections);
        }

        public void AddRoom(Room room)
        {
            _context.Rooms.Add(room);
        }

        /// <summary>
        /// return null no action to del else delete thanh cong
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Room?> DeleteRoom(Guid roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }
            return room;
        }

        public async Task<Room?> EditRoom(EditRoomDto edit)
        {
            var room = await _context.Rooms.FindAsync(edit.RoomId);
            if (room != null)
            {
                room.RoomName = edit.RoomName;
                room.SecurityCode = edit.SecurityCode;
            }
            return room;
        }

        public async Task DeleteAllRoom()
        {
            var list = await _context.Rooms.ToListAsync();
            _context.RemoveRange(list);
        }

        public async Task<PagedList<RoomDto>> GetAllRoomAsync(RoomParams roomParams)
        {
            var list = _context.Rooms.AsQueryable();
            return await PagedList<RoomDto>.CreateAsync(list.ProjectTo<RoomDto>(_mapper.ConfigurationProvider).AsNoTracking(), roomParams.PageNumber, roomParams.PageSize);
        }

        public async Task UpdateCountMember(Guid roomId, int count)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null)
            {
                room.CountMember = count;
            }
        }

        public async Task UpdateBlockChat(Guid roomId, bool block)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room != null)
            {
                room.BlockedChat = block;
            }
        }
    }
}
