using API.Entities;

namespace API.Interfaces
{
    public interface IStrangerRepository
    {
        Task<List<List<AppUser>>> StrangerFindMatch();
    }
}
