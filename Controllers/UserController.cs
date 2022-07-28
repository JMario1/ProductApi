using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using productMgtApi.Controllers.Resources;
using productMgtApi.Domain.Models;
using productMgtApi.Domain.Services;

namespace productMgtApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAppUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IAppUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(AppUser), 200)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest request)
        {
            if (request.Password == null)
            {
                return BadRequest();
            }
            AppUser user = _mapper.Map<CreateUserRequest, AppUser>(request);
            Response<AppUser> result = await _userService.CreateUserAsync(user, request.Password, UserRoles.User);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data!.Email);
        }

        [HttpPost("register-admin")]
        [Authorize(Roles = "SuperAdmin")]
        [ProducesResponseType(typeof(AppUser), 200)]
        public async Task<IActionResult> CreateAdminAsync([FromBody] CreateUserRequest request)
        {
            if (request.Password == null)
            {
                return BadRequest();
            }
            AppUser user = _mapper.Map<CreateUserRequest, AppUser>(request);
            Response<AppUser> result = await _userService.CreateUserAsync(user, request.Password, UserRoles.Admin);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data!.Email);
        }
    }
}