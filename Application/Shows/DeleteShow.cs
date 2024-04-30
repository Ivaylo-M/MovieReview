namespace Application.Shows
{
    using Application.Response;
    using Domain;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using static Common.ExceptionMessages.Show;
    using static Common.SuccessMessages.Show;
    using static Common.FailMessages.Show;

    public class DeleteShow
    {
        public class DeleteShowCommand : IRequest<Result<Unit>>
        {
            public string ShowId { get; set; } = null!;
        }

        public class DeleteShowHandler : IRequestHandler<DeleteShowCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public DeleteShowHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(DeleteShowCommand request, CancellationToken cancellationToken)
            {
                Show? show = await this.repository.All<Show>(s => s.ShowId.ToString().Equals(request.ShowId.ToLower()))
                    .Include(s => s.Genres)
                    .Include(s => s.UserReviews)
                    .Include(s => s.CountriesOfOrigin)
                    .Include(s => s.FilmingLocations)
                    .Include(s => s.Languages)
                    .Include(s => s.UserRatings)
                    .Include(s => s.WatchListItems)
                    .Include(s => s.Episodes)
                    .FirstOrDefaultAsync(CancellationToken.None);

                if (show == null)
                {
                    return Result<Unit>.Failure(ShowNotFound);
                }

                try
                {
                    this.repository.Delete(show);
                    await this.repository.SaveChangesAsync();

                    return Result<Unit>.Success(Unit.Value, String.Format(SuccessfullyDeletedShow, show.Title));
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(String.Format(FailedDeleteShow, show.Title));
                }
            }
        }
    }
}
