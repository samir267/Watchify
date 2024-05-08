using back_wachify.Business_Logic_Layer.Dto;
using back_wachify.Business_Logic_Layer.Interfaces;
using back_wachify.Data;
using back_wachify.Data.Model;
using back_wachify.Dto;
using back_wachify.Model;
using back_wachify.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace back_wachify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        private readonly ApplicationDbContext _dbContext;


        public AuthController(IConfiguration configuration, IUserService userService, IAuthService authService, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _userService = userService;
            _authService = authService;

            _dbContext = dbContext;

        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var userName = _userService.GetMyName();
            return Ok(userName);
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            try
            {
                var registeredUser = await _authService.RegisterUser(request);
                return Ok(registeredUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while registering the user.the error is:" + ex);
            }
        }
        [HttpPost("registerSocial")]
        public async Task<ActionResult<User>> RegisterSocial(SocialregisterDto request)
        {
            try
            {
                var registeredUser = await _authService.RegisterUserSocial(request);
                return Ok(registeredUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while registering the user.the error is:" + ex);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AuthDto request)
        {
            var (token, isPasswordWrong,role) = await _authService.login(request);

            if (token == null)
            {
                if (isPasswordWrong)
                {
                    return BadRequest("Wrong password.");
                }
                else
                {
                    return BadRequest("User not found.");
                }
            }

            // Return JWT token
            return Ok(new { token, role });
        }
        [HttpPost("loginSocial")]
        public async Task<ActionResult<string>> LoginSocial(SocialAuthDto request)
        {
            var (token, IsSocialNotLogged) = await _authService.loginSocial(request);

            if (token == null)
            {
                if (IsSocialNotLogged)
                {
                    return BadRequest("you must loggedIn from facebook first");
                }
                else
                {
                    return BadRequest("User not found.");
                }
            }

            // Return JWT token
            return Ok(new { token });
        }


        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var (token, message) = await _authService.RefreshToken(refreshToken);

            if (token == null)
            {
                return Unauthorized(message);
            }

            return Ok(token);
        }





    }
}
