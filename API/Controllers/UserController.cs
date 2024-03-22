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
            RegisterUserCommand command = new RegisterUserCommand
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = registerDto.Password
            };

            Result<UserDto> result = await this.mediator.Send(command);

            return Ok(result);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            LoginUserCommand command = new LoginUserCommand
            {
                Email = loginDto.Email,
                Password = loginDto.Password,
                RememberMe = loginDto.RememberMe
            };

            Result<UserDto> result = await this.mediator.Send(command);

            return Ok(result);
        }

        [Authorize]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            LogoutUserCommand command = new LogoutUserCommand
            {
                UserId = User!.GetById()
            };

            Result<Unit> result = await this.mediator.Send(command);

            return Ok(result);
        }
    }
}
