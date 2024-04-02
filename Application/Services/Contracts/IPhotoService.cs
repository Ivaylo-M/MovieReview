namespace Application.Services.Contracts
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using CloudinaryDotNet.Actions;
    
    using Domain;
    using Application.Response;

    public interface IPhotoService
    {
        Task<Result<ImageUploadResult>> AddPhotoAsync(IFormFile file, string path);

        Task<Result<DeletionResult>> DeletePhotoAsync(Photo photo);
    }
}
