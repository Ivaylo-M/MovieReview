namespace Application.DTOs.Photos
{
    using Microsoft.AspNetCore.Http;

    public class AddShowPhotoDto
    {
        public IFormFile File { get; set; } = null!;

        public string ShowId { get; set; } = null!;
    }
}
