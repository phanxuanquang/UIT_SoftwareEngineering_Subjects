using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class StrangerController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public StrangerController(IUnitOfWork unitOfWork,
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

        [HttpPost("add-stranger")]
        [AllowAnonymous]
        public async Task<ActionResult> AddStranger(RegisterStrangerDto register)
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
                StrangerFilter = register.StrangerFilter,
                Gender = register.Gender,
                Age = register.Age,
                Nationality = register.Nationality,
                //Type = register.Type,
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

        [HttpPost("join-stranger")]
        public async Task<ActionResult> JoinStranger(JoinStrangerRoomDto join)
        {
            var room = await _unitOfWork.RoomRepository.GetRoomById(join.RoomId);
            if (room == null)
                return NotFound();

            if ((string.IsNullOrEmpty(room.SecurityCode) && string.IsNullOrEmpty(join.SecurityCode)) || room.SecurityCode == join.SecurityCode)
            {
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(HttpContext.User.GetUserId());
                if (user?.StrangerFilter == null)
                    return BadRequest("User doesn't have filter");

                user.StrangerFilter.CurrentRoom = room;
                await _unitOfWork.Complete();

                var userDto = new UserDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                    LastActive = user.LastActive,
                    StrangerFilter = _mapper.Map<StrangerFilterDto>(user.StrangerFilter),
                    Gender = user.Gender,
                    Age = user.Age,
                    Nationality = user.Nationality,
                    //Type = user.Type,
                    Token = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token") ?? string.Empty
                };

                return Ok(new
                {
                    User = userDto,
                    Room = _mapper.Map<RoomDto>(room)
                });
            }
            return Unauthorized();
        }
    }
}
