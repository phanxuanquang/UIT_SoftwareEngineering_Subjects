using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class RoomController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public RoomController(IUnitOfWork unitOfWork,
            IMapper mapper,
            ITokenService tokenService,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("add-room")]
        [AllowAnonymous]
        public async Task<ActionResult> AddRoom(RegisterDto register)
        {
            var user = _mapper.Map<AppUser>(register);
            user.UserName = Guid.NewGuid().ToString().ToLower();

            string password = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Host");
            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            var userDto = new UserDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                LastActive = user.LastActive,
                Token = await _tokenService.CreateTokenAsync(user)
            };

            var fakeLogin = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!fakeLogin.Succeeded) return BadRequest("Something wrong when create room");

            var room = new Room
            {
                RoomName = register.RoomName,
                SecurityCode = register.SecurityCode,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow
            };

            _unitOfWork.RoomRepository.AddRoom(room);

            if (await _unitOfWork.Complete())
            {
                return Ok(new
                {
                    User = userDto,
                    Room = await _unitOfWork.RoomRepository.GetRoomDtoById(room.RoomId)
                });
            }

            return BadRequest("Problem adding room");
        }

        [HttpPost("join-room")]
        [AllowAnonymous]
        public async Task<ActionResult> JoinRoom(JoinRoomDto join)
        {
            var room = await _unitOfWork.RoomRepository.GetRoomById(join.RoomId);
            if (room == null)
                return NotFound();

            if ((string.IsNullOrEmpty(room.SecurityCode) && string.IsNullOrEmpty(join.SecurityCode)) || room.SecurityCode == join.SecurityCode)
            {
                var user = _mapper.Map<AppUser>(join);
                user.UserName = Guid.NewGuid().ToString().ToLower();

                string password = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded) return BadRequest(result.Errors);

                if (room.Connections.Any())
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "Member");
                    if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);
                }
                else
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "Host");
                    if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);
                }

                var userDto = new UserDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                    LastActive = user.LastActive,
                    Token = await _tokenService.CreateTokenAsync(user)
                };

                var fakeLogin = await _signInManager.CheckPasswordSignInAsync(user, password, false);

                if (!fakeLogin.Succeeded) return BadRequest("Something wrong when create room");

                return Ok(new
                {
                    User = userDto,
                    Room = _mapper.Map<RoomDto>(room)
                });
            }
            return Unauthorized();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Host")]
        public async Task<ActionResult> EditRoom(EditRoomDto edit)
        {
            Room? room = await _unitOfWork.RoomRepository.GetRoomById(edit.RoomId);
            if (room == null) return NotFound();
            if (room.UserId != HttpContext.User.GetUserId())
                return Unauthorized();
            room = await _unitOfWork.RoomRepository.EditRoom(edit);
            if (room != null)
            {
                if (_unitOfWork.HasChanges())
                {
                    if (await _unitOfWork.Complete())
                        return Ok(new RoomDto
                        {
                            RoomId = room.RoomId,
                            RoomName = room.RoomName,
                            UserId = room.UserId
                        });
                    return BadRequest("Problem edit room");
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Host")]
        public async Task<ActionResult> DeleteRoom(Guid id)
        {
            Room? room = await _unitOfWork.RoomRepository.GetRoomById(id);
            if (room == null) return NotFound();
            if (room.UserId != HttpContext.User.GetUserId())
                return Unauthorized();
            var entity = await _unitOfWork.RoomRepository.DeleteRoom(id);

            if (entity != null)
            {
                if (await _unitOfWork.Complete())
                    return Ok(new RoomDto
                    {
                        RoomId = entity.RoomId,
                        RoomName = entity.RoomName,
                        UserId = entity.UserId
                    });
                return BadRequest("Problem delete room");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("delete-all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAllRoom()
        {
            await _unitOfWork.RoomRepository.DeleteAllRoom();

            if (_unitOfWork.HasChanges())
            {
                if (await _unitOfWork.Complete())
                    return Ok();//xoa thanh cong
                return BadRequest("Problem delete all room");
            }
            else
            {
                return NoContent();//ko co gi de xoa
            }
        }
    }
}
