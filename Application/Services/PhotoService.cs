namespace Application.Services
{
    using System.Threading.Tasks;
    
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    
    using Domain;
    using Application.Response;
    using Application.Services.Contracts;
    using CloudinaryDotNet.Actions;

    using static Common.FailMessages.Photo;

    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;

        public PhotoService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;                 
        }

        public async Task<Result<ImageUploadResult>> AddPhotoAsync(IFormFile file)
        {
            using (Stream stream = file.OpenReadStream())
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                };

                try
                {
                    ImageUploadResult imageUploadResult = await this.cloudinary.UploadAsync(uploadParams);

                    return Result<ImageUploadResult>.Success(imageUploadResult);
                }
                catch (Exception)
                {
                    return Result<ImageUploadResult>.Failure(FailedUploadPhoto);
                }
            }
        }

        public async Task<Result<DeletionResult>> DeletePhotoAsync(Photo photo)
        {
            try
            {
                DeletionParams deletionParams = new DeletionParams(photo.PhotoId);

                DeletionResult result = await cloudinary.DestroyAsync(deletionParams);

                return Result<DeletionResult>.Success(result);
            }
            catch (Exception)
            {
                return Result<DeletionResult>.Failure(FailedDeletePhoto);
            }
        }
    }
}
