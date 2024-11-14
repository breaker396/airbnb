using Airbnb.Api.Infrastructure;
using Airbnb.Api.Models;
using Airbnb.Api.Services;
using Airbnb.Api.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Airbnb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly JwtSettings _jwtSettings;
        public UserController(IUserService userService, IOptions<JwtSettings> jwtSettings)
        {
            this._userService = userService;
            this._jwtSettings = jwtSettings.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var user = await _userService.Register(userRegisterDto);
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRegisterDto userRegisterDto)
        {
            var user = await _userService.Login(userRegisterDto);
            if (user != null)
            {
                TokenGenerate tokenGenerate = new TokenGenerate(_jwtSettings);
                string token = tokenGenerate.GenerateJwt(user.Id, user.Name);
                this.HttpContext.Response.Cookies.Append(CookieAuthenticationDefaults.AuthenticationScheme, token);
                return Ok(user);
            }
            return BadRequest();
        }
    }
}
