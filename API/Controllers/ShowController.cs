namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    
    using Application.DTOs.Shows;
    using Application.Response;
    using API.Infrastructure;
    
    using static Application.Shows.AddShow;

    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShowController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<ActionResult> AddShow([FromBody] AddShowDto addShowDto)
        {
            AddShowCommand command = new AddShowCommand
            {
                UserId = User!.GetById(),
            };

            Result<Unit> result = await mediator.Send(command);

            return Ok(result);
        }
    }
}
