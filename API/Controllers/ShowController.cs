namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    
    using Application.DTOs.Shows;
    using Application.Response;
    using API.Infrastructure;
    
    using static Application.Shows.AddShow;
    using static Application.Shows.AllShows;
    using static Application.Shows.FilterShows;
    using static Application.Shows.EditShow;
    using static Application.Shows.DeleteShow;

    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShowController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        public async Task<ActionResult> AddShow([FromBody] AddShowDto addShowDto)
        {
            AddShowCommand command = new AddShowCommand
            {
                Dto = addShowDto
            };

            Result<Unit> result = await mediator.Send(command);

            return Ok(result);
        }

        [Route("edit/{showId}")]
        [HttpPost]
        public async Task<ActionResult> EditShow([FromBody] EditShowDto editShowDto, [FromRoute] string showId)
        {
            EditShowCommand command = new EditShowCommand
            {
                Dto = editShowDto,
                ShowId = showId
            };

            Result<Unit> result = await mediator.Send(command);

            return Ok(result);
        }

        [Route("delete/{showId}")]
        [HttpPost]
        public async Task<ActionResult> DeleteShow([FromRoute] string showId)
        {
            DeleteShowCommand command = new DeleteShowCommand
            {
                ShowId = showId
            };

            Result<Unit> result = await this.mediator.Send(command);

            return Ok(result);
        }

        [Route("all")]
        [HttpGet]
        public async Task<ActionResult> AllShows()
        {
            AllShowsQuery query = new AllShowsQuery
            {
                UserId = User.GetById()
            };

            Result<IEnumerable<AllShowsDto>> result = await mediator.Send(query);

            return Ok(result);
        }

        [Route("filter")]
        [HttpGet]
        public async Task<ActionResult> FilterShows([FromBody] FilterShowsDto filterShowsDto)
        {
            FilterShowsQuery query = new FilterShowsQuery
            {
                ShowTypes = filterShowsDto.ShowTypes,
                Genres = filterShowsDto.Genres,
                MinReleaseYear = filterShowsDto.MinReleaseYear,
                MaxReleaseYear = filterShowsDto.MaxReleaseYear
            };

            Result<IEnumerable<AllShowsDto>> result = await mediator.Send(query);

            return Ok(result);
        }
    }
}