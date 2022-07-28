using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using productMgtApi.Controllers.Resources;
using productMgtApi.Domain.Security;
using productMgtApi.Domain.Services;

namespace productMgtApi.Controllers
{

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [Route("/api/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            
            Response<AccessToken> result = await _authService.CreateAccessTokenAsync(request.Email!, request.Password!);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            TokenResponse res = _mapper.Map<AccessToken, TokenResponse>(result.Data!);
            return Ok(res);
        }

        [Route("/api/token/refresh")]
        [HttpPost]
        public async Task<IActionResult> RefreshAsync([FromBody] TokenRequest request)
        {
            
            Response<AccessToken> result = await _authService.RefreshTokenAsync(request.Token!, request.Email!);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(_mapper.Map<AccessToken, TokenResponse>(result.Data!));
        }

        // [Route("/api/token/refresh")]
        [Route("/api/logout")]
        [HttpPost]
        public IActionResult Logout([FromBody] RevokeTokenRequest token)
        {
            
            _authService.RevokeRefreshToken(token.RefreshToken!);

            return NoContent();
        }
    }
}