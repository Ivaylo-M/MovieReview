namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    
    using Application.DTOs.Users;
    using Application.Response;
    using API.Infrastructure;
    
    using static Application.Users.RegisterUser;
    using static Application.Users.LoginUser;
    using static Application.Users.LogoutUser;

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            RegisterUserCommand registerCommand = new RegisterUserCommand
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password
            };

            Result<UserDto> result = await this.mediator.Send(registerCommand);

            return Ok(result);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            LoginUserCommand loginCommand = new LoginUserCommand
            {
                Email = loginDto.Email,
                Password = loginDto.Password,
                RememberMe = loginDto.RememberMe
            };

            Result<UserDto> result = await this.mediator.Send(loginCommand);

            return Ok(result);
        }

        [Authorize]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            LogoutUserCommand logoutCommand = new LogoutUserCommand
            {
                UserId = User!.GetById()
            };

            Result<Unit> result = await this.mediator.Send(logoutCommand);

            return Ok(result);
        }
    }
}
