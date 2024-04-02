namespace Application.Photos
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using CloudinaryDotNet.Actions;
    
    using Domain;
    using Persistence.Repositories;
    using Application.Response;
    using Application.Services.Contracts;
    
    using static Common.ExceptionMessages.Show;
    using static Common.ExceptionMessages.Photo;
    using static Common.FailMessages.Photo;
    using static Common.SuccessMessages.Photo;

    public class DeleteShowPhoto
    {
        public class DeleteShowPhotoCommand : IRequest<Result<Unit>>
        {
            public string ShowId { get; set; } = null!;
        }

        public class DeleteShowPhotoHandler : IRequestHandler<DeleteShowPhotoCommand, Result<Unit>>
        {
            private readonly IRepository repository;
            private readonly IPhotoService photoService;

            public DeleteShowPhotoHandler(IRepository repository, IPhotoService photoService)
            {
                this.repository = repository;
                this.photoService = photoService;
            }

            public async Task<Result<Unit>> Handle(DeleteShowPhotoCommand request, CancellationToken cancellationToken)
            {
                Show? show = await this.repository.FirstOrDefaultAsync<Show>(s => s.ShowId.ToString().Equals(request.ShowId.ToLower()));

                if (show == null)
                {
                    return Result<Unit>.Failure(ShowNotFound);
                }

                Photo? photo = await this.repository.FirstOrDefaultAsync<Photo>(p => p.PhotoId.Equals(show.PhotoId.ToLower()));

                if (photo == null)
                {
                    return Result<Unit>.Failure(PhotoNotFound);
                }

                if (show.PhotoId == null)
                {
                    return Result<Unit>.Failure(NoPhotoYet);
                }

                try
                {
                    Result<DeletionResult> result = await this.photoService.DeletePhotoAsync(photo);

                    if (!result.IsSuccess)
                    {
                        return Result<Unit>.Failure(result.ErrorMessage!);
                    }

                    this.repository.Delete(photo);

                    show.PhotoId = null;

                    await this.repository.SaveChangesAsync();

                    return Result<Unit>.Success(Unit.Value, SuccessfullyDeletePhoto);
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(FailedDeletePhoto);
                }
            }
        }
    }
}
