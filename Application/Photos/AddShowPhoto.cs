namespace Application.Photos
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using CloudinaryDotNet.Actions;
    
    using Domain;
    using Persistence.Repositories;
    using Application.Response;
    using Application.Services.Contracts;

    using static Common.ExceptionMessages.Photo;
    using static Common.ExceptionMessages.Show;
    using static Common.FailMessages.Photo;
    using static Common.SuccessMessages.Photo;

    public class AddShowPhoto
    {
        public class AddShowPhotoCommand : IRequest<Result<Unit>>
        {
            public string ShowId { get; set; } = null!;

            public IFormFile File { get; set; } = null!;
        }

        public class AddShowPhotoHandler : IRequestHandler<AddShowPhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;
            private readonly IRepository repository;

            public AddShowPhotoHandler(IPhotoService photoService, IRepository repository)
            {
                this.photoService = photoService;
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(AddShowPhotoCommand request, CancellationToken cancellationToken)
            {
                if (request.File == null || request.File.Length == 0)
                {
                    return Result<Unit>.Failure(EmptyPhoto);
                }

                Show? show = await this.repository.FirstOrDefaultAsync<Show>(s => s.ShowId.ToString().Equals(request.ShowId.ToLower()));

                if (show == null)
                {
                    return Result<Unit>.Failure(ShowNotFound);
                }

                if (show.PhotoId != null)
                {
                    return Result<Unit>.Failure(ShowAlreadyHavePhoto);
                }

                string path = $"{show.ShowType}s/{show.Title}";

                try
                {
                    Result<ImageUploadResult> result = await this.photoService.AddPhotoAsync(request.File, path);

                    if (!result.IsSuccess)
                    {
                        return Result<Unit>.Failure(result.ErrorMessage!);
                    }

                    Photo photo = new Photo
                    {
                        PhotoId = result.Data!.PublicId,
                        Url = result.Data.Url.AbsoluteUri
                    };

                    show.PhotoId = photo.PhotoId;

                    await this.repository.AddAsync(photo);
                    await this.repository.SaveChangesAsync();

                    return Result<Unit>.Success(Unit.Value, SuccessfullyUploadPhoto);
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(FailedUploadPhoto);
                }
            }
        }
    }
}
