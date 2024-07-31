namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRoomRepository RoomRepository { get; }
        IStrangerRepository StrangerRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
