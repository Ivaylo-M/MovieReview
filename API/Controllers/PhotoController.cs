namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    
    using Application.Response;
    using Application.DTOs.Photos;
    
    using static Application.Photos.AddShowPhoto;
    using static Application.Photos.DeleteShowPhoto;

    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IMediator mediator;

        public PhotoController(IMediator mediator)
        {
            this.mediator = mediator;   
        }

        [Route("add-show-photo")]
        [HttpPost]
        public async Task<ActionResult> AddShowPhoto([FromForm] AddShowPhotoDto addShowPhotoDto)
        {
            AddShowPhotoCommand command = new()
            {
                File = addShowPhotoDto.File,
                ShowId = addShowPhotoDto.ShowId
            };

            Result<Unit> result = await this.mediator.Send(command);

            return Ok(result);
        }

        [Route("delete-show-photo")]
        [HttpPost]
        public async Task<ActionResult> DeleteShowPhoto([FromBody] string showId)
        {
            DeleteShowPhotoCommand command = new()
            {
                ShowId = showId
            };

            Result<Unit> result = await this.mediator.Send(command);

            return Ok(result);
        }
    }
}
