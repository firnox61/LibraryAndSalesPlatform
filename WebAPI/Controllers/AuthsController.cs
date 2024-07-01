using Business.Abstract;
using Entities.DTOs.UsersDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {

        private IAuthService _authService;
        private IUserService _userService;

        public AuthsController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }



        [HttpPost("login")]
        public ActionResult Login(LoginUserDto loginUserDto)
        {
            var userToLogin = _authService.Login(loginUserDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterUserDto registerUserDto)
        {
            var userExists = _authService.UserExists(registerUserDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(registerUserDto, registerUserDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
