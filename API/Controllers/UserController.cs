namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

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
        public ActionResult Register()
        {
            return Ok(true);
        }

        [Route("login")]
        [HttpPost]
        public ActionResult Login()
        {
            return Ok(true);
        }

        //[Authorize]
        [Route("logout")]
        [HttpPost]
        public ActionResult Logout()
        {
            return Ok(true);
        }
    }
}
