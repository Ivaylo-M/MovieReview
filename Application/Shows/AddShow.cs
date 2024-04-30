namespace Application.Shows
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    
    using Domain;
    using Domain.Enums;
    using Persistence.Repositories;
    using Application.Response;
    using Application.DTOs.Shows;
    
    using static Common.ExceptionMessages.Show;
    using static Common.SuccessMessages.Show;
    using static Common.FailMessages.Show;

    public class AddShow
    {
        public class AddShowCommand : IRequest<Result<Unit>>
        {
            public AddShowDto Dto { get; set; } = null!;
        }

        public class AddShowHandler : IRequestHandler<AddShowCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public AddShowHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(AddShowCommand request, CancellationToken cancellationToken)
            {
                if (request.Dto.ShowType == ShowType.Episode)
                {
                    if (await this.repository.AnyAsync<Show>(s => s.ShowId.ToString().Equals(request.Dto.SeriesId!.ToLower())) == false)
                    {
                        return Result<Unit>.Failure(ShowNotFound);
                    }
                }

                Show show = new()
                {
                    Title = request.Dto.Title,
                    ShowType = request.Dto.ShowType,
                    Description = request.Dto.Description,
                    ReleaseDate = request.Dto.ReleaseDate,
                    EndDate = request.Dto.EndDate,
                    Season = request.Dto.Season,
                    SeriesId = request.Dto.SeriesId != null ? Guid.Parse(request.Dto.SeriesId) : null,
                    Duration = request.Dto.Duration
                };

                if (request.Dto.ShowType != ShowType.Episode)
                {
                    show.CountriesOfOrigin = request.Dto.CountriesOfOrigin!
                        .Select(countryOfOriginId => new ShowCountryOfOrigin
                        {
                            CountryOfOriginId = countryOfOriginId
                        })
                        .ToArray();

                    show.Languages = request.Dto.Languages!
                        .Select(languageId => new ShowLanguage
                        {
                            LanguageId = languageId
                        })
                        .ToArray();

                    show.Genres = request.Dto.Genres!
                        .Select(genreId => new ShowGenre
                        {
                            GenreId = genreId
                        })
                        .ToArray();

                    show.FilmingLocations = request.Dto.FilmingLocations!
                        .Select(filmingLocationId => new ShowFilmingLocation
                        {
                            FilmingLocationId = filmingLocationId
                        })
                        .ToArray();
                }

                try
                {
                    await this.repository.AddAsync(show);
                    await this.repository.SaveChangesAsync();

                    return Result<Unit>.Success(Unit.Value, String.Format(SuccessfullyAddedShow, request.Dto.Title));
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(String.Format(String.Format(FailedAddShow, request.Dto.Title)));
                }
            }
        }
    }
}
