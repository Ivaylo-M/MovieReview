namespace Application.Shows
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    
    using Domain;
    using Persistence.Repositories;
    using Application.Response;
    using Application.DTOs.Shows;
    
    using static Common.ExceptionMessages.User;

    public class AllShows
    {
        public class AllShowsQuery : IRequest<Result<IEnumerable<AllShowsDto>>>
        {
            public string UserId { get; set; } = null!;
        }

        public class AllShowsHandler : IRequestHandler<AllShowsQuery, Result<IEnumerable<AllShowsDto>>>
        {
            private readonly IRepository repository;
            private readonly IMemoryCache memoryCache;

            public AllShowsHandler(IRepository repository, IMemoryCache memoryCache)
            {
                this.repository = repository;
                this.memoryCache = memoryCache;
            }

            public async Task<Result<IEnumerable<AllShowsDto>>> Handle(AllShowsQuery request, CancellationToken cancellationToken)
            {
                if (await this.repository.AnyAsync<User>(u => u.Id.ToString().Equals(request.UserId.ToLower())) == false)
                {
                    return Result<IEnumerable<AllShowsDto>>.Failure(UserNotFound);
                }

                IEnumerable<AllShowsDto> shows = await this.repository.All<Show>()
                    .Include(s => s.Genres)
                    .Include(s => s.Photo)
                    .Select(s => new AllShowsDto
                    {
                        ShowId = s.ShowId.ToString(),
                        Title = s.Title,
                        ShowType = s.ShowType,
                        PhotoUrl = s.Photo != null ? s.Photo.Url : null,
                        ReleaseYear = s.ReleaseDate.Year,
                        EndYear = s.EndDate.HasValue ? s.EndDate.Value.Year : null,
                        Duration = s.Duration,
                        AverageRating = s.UserRatings.Count > 0 ? (float)s.UserRatings.Average(ur => ur.Stars) : 0f,
                        NumberOfRatings = s.UserRatings.Count,
                        MyRating = GetUserRating(s.UserRatings, request.UserId),
                        Description = s.Description,
                        Genres = s.Genres.Select(sg => sg.GenreId)
                    })
                    .ToListAsync();

                this.memoryCache.Set("Shows", shows);

                return Result<IEnumerable<AllShowsDto>>.Success(shows);
            }

            private static int? GetUserRating(ICollection<Rating> ratings, string userId)
            {
                Rating? rating = ratings.FirstOrDefault(ur => ur.UserId.ToString().ToLower().Equals(userId.ToLower()));

                if (rating == null)
                {
                    return null;
                }

                return rating.Stars;
            }
        }
    }
}
