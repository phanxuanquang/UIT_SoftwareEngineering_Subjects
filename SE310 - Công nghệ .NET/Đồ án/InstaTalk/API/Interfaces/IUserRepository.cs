using API.Dtos;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser?> GetUserByIdAsync(Guid id);
        Task<AppUser?> GetUserByUsernameAsync(string username);
        Task<MemberDto?> GetMemberAsync(Guid userId);
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<IEnumerable<MemberDto>> SearchMemberAsync(string displayname);
        Task<IEnumerable<MemberDto?>> GetUsersOnlineAsync(UserConnectionInfo[] userOnlines);
        Task<AppUser?> UpdateLocked(Guid userId);
    }
}
