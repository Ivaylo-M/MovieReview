namespace Application.Shows
{
    using Application.DTOs.Reviews;
    using Application.DTOs.Shows;
    using Application.Response;
    using Domain;
    using Domain.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using static Common.ExceptionMessages.Show;
    using static Common.ExceptionMessages.User;

    public class ShowDetails
    {
        public class ShowDetailsQuery : IRequest<Result<ShowDetailsDto>>
        {
            public string ShowId { get; set; } = null!;

            public string UserId { get; set; } = null!;
        }

        public class ShowDetailsHandler : IRequestHandler<ShowDetailsQuery, Result<ShowDetailsDto>>
        {
            private readonly IRepository repository;

            public ShowDetailsHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<ShowDetailsDto>> Handle(ShowDetailsQuery request, CancellationToken cancellationToken)
            {
                Show? show = await this.repository.All<Show>(s => s.ShowId.ToString().Equals(request.ShowId.ToLower()))
                    .Include(s => s.Genres)
                        .ThenInclude(g => g.Genre)
                    .Include(s => s.CountriesOfOrigin)
                        .ThenInclude(coo => coo.CountryOfOrigin)
                    .Include(s => s.Languages)
                        .ThenInclude(l => l.Language)
                    .Include(s => s.FilmingLocations)
                        .ThenInclude(fl => fl.FilmingLocation)
                    .Include(s => s.UserRatings)
                    .Include(s => s.UserReviews)
                    .Include(s => s.Series)
                    .Include(s => s.Series!.CountriesOfOrigin)
                    .Include(s => s.Series!.FilmingLocations)
                    .Include(s => s.Series!.Languages)
                    .Include(s => s.Series!.Genres)
                    .Include(s => s.Series!.Episodes)
                    .Include(s => s.Photo)
                    .Include(s => s.Episodes)
                    .FirstOrDefaultAsync(CancellationToken.None);

                if (show == null)
                {
                    return Result<ShowDetailsDto>.Failure(ShowNotFound);
                }

                if (await this.repository.AnyAsync<User>(u => u.Id.ToString().Equals(request.UserId.ToLower())) == false)
                {
                    return Result<ShowDetailsDto>.Failure(UserNotFound);
                }

                ShowDetailsDto showDetailsDto = new()
                {
                    ShowId = request.ShowId,
                    ShowType = show.ShowType,
                    Title = show.Title,
                    Description = show.Description,
                    ReleaseDate = show.ReleaseDate,
                    EndDate = show.EndDate,
                    PhotoUrl = show.Photo!.Url
                };

                if (show.UserRatings != null)
                {
                    showDetailsDto.AverageRating = show.UserRatings.Count == 0 
                        ? 0f
                        : (float)Math.Round(show.UserRatings.Average(ur => ur.Stars), 1);

                    showDetailsDto.MyRating = show.UserRatings.FirstOrDefault(ur => ur.UserId.ToString().Equals(request.UserId.ToLower()))?.Stars;
                    showDetailsDto.NumberOfRatings = show.UserRatings.Count;
                }

                if (show.UserReviews != null && show.UserReviews.Count > 0)
                {
                    showDetailsDto.LastReview = show.UserReviews
                        .OrderByDescending(ur => ur.CreatedAt)
                        .Select(ur => new LastReviewDto
                            {
                                ReviewId = ur.ReviewId.ToString(),
                                Heading = ur.Heading,
                                Content = ur.Content,
                                CreatedAt = ur.CreatedAt,
                                IsMine = ur.UserId.ToString().Equals(request.UserId.ToLower())
                            })
                        .FirstOrDefault();
                }

                if (show.ShowType == ShowType.TVSeries)
                {
                    showDetailsDto.Duration = show.Episodes!.Count == 0
                        ? 0
                        : (int)show.Episodes!.Average(e => e.Duration)!;
                }
                else
                {
                    showDetailsDto.Duration = (int)show.Duration!;
                }

                Show showWithCollections = show;
                if (show.ShowType == ShowType.Episode)
                {
                    showDetailsDto.EpisodeNumber = GetEpisodeNumber(show);
                    showDetailsDto.Season = show.Season;
                    showDetailsDto.TVSeries = new TVSeriesDto
                        {
                            Id = show.Series!.ShowId.ToString(),
                            Title = show.Series.Title,
                        };
                    showWithCollections = show.Series;
                }

                showDetailsDto.CountriesOfOrigin = showWithCollections.CountriesOfOrigin.Select(coo => coo.CountryOfOrigin.Name);
                showDetailsDto.FilmingLocations = showWithCollections.FilmingLocations.Select(fl => fl.FilmingLocation.Name);
                showDetailsDto.Languages = showWithCollections.Languages.Select(l => l.Language.Name);
                showDetailsDto.Genres = showWithCollections.Genres.Select(g => g.Genre.Name);

                return Result<ShowDetailsDto>.Success(showDetailsDto);
            }

            private static int GetEpisodeNumber(Show episode)
            {
                int episodeNumber = episode.Series!.Episodes!
                    .Count(s => s.Season == episode.Season && s.ReleaseDate < episode.ReleaseDate) + 1;

                return episodeNumber;
            }
        }
    }
}
