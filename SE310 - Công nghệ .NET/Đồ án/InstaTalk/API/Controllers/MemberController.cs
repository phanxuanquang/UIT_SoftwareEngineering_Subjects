using API.Dtos;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Authorize]
    public class MemberController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly PresenceTracker _presenceTracker;

        public MemberController(IUnitOfWork unitOfWork, IHubContext<PresenceHub> presenceHub, PresenceTracker presenceTracker)
        {
            _unitOfWork = unitOfWork;
            _presenceHub = presenceHub;
            _presenceTracker = presenceTracker;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllMembers([FromQuery] UserParams userParams)
        {
            userParams.CurrentDisplayName = User.GetDisplayName();
            var comments = await _unitOfWork.UserRepository.GetMembersAsync(userParams);
            Response.AddPaginationHeader(comments.CurrentPage, comments.PageSize, comments.TotalCount, comments.TotalPages);

            return Ok(comments);
        }

        [HttpGet("{userId}")] // member/userId
        public async Task<ActionResult<MemberDto>> GetMember(Guid userId)
        {
            return Ok(await _unitOfWork.UserRepository.GetMemberAsync(userId));
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = "Admin,Host")]
        public async Task<ActionResult> LockedUser(Guid userId)
        {
            var u = await _unitOfWork.UserRepository.UpdateLocked(userId);
            if (u != null)
            {
                var connections = await _presenceTracker.GetConnectionsForUserID(userId);
                await _presenceHub.Clients.Clients(connections).SendAsync("OnLockedUser", true);
                return NoContent();
            }
            else
            {
                return BadRequest("Can not find given username");
            }
        }
    }
}
