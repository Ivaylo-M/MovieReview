namespace Application.Shows
{
    using Application.DTOs.Shows;
    using Application.Response;
    using Domain;
    using Domain.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Repositories;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using static Common.ExceptionMessages.Show;
    using static Common.FailMessages.Show;
    using static Common.SuccessMessages.Show;

    public class EditShow
    {
        public class EditShowCommand : IRequest<Result<Unit>>
        {
            public string ShowId { get; set; } = null!;

            public EditShowDto Dto { get; set; } = null!;
        }

        public class EditShowHandler : IRequestHandler<EditShowCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public EditShowHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(EditShowCommand request, CancellationToken cancellationToken)
            {
                Show? show = await this.repository.All<Show>(s => s.ShowId.ToString().Equals(request.ShowId.ToLower()))
                    .Include(s => s.CountriesOfOrigin)
                    .Include(s => s.FilmingLocations)
                    .Include(s => s.Genres)
                    .Include(s => s.Languages)
                    .FirstOrDefaultAsync();

                if (show == null)
                {
                    return Result<Unit>.Failure(ShowNotFound);
                }

                show.Title = request.Dto.Title;
                show.Description = request.Dto.Description;
                show.Duration = request.Dto.Duration;
                show.ReleaseDate = request.Dto.ReleaseDate;
                show.EndDate = request.Dto.EndDate;
                show.Season = request.Dto.Season;

                if (show.ShowType != ShowType.Episode)
                {
                    if (request.Dto.CountriesOfOrigin != null)
                    {
                        show.CountriesOfOrigin = request.Dto.CountriesOfOrigin
                            .Select(countryOfOriginId => new ShowCountryOfOrigin
                            {
                                CountryOfOriginId = countryOfOriginId
                            })
                            .ToArray();
                    }

                    if (request.Dto.Languages != null)
                    {
                        show.Languages = request.Dto.Languages
                            .Select(languageId => new ShowLanguage
                            {
                                LanguageId = languageId
                            })
                            .ToArray();
                    }

                    if (request.Dto.Genres != null)
                    {
                        show.Genres = request.Dto.Genres
                            .Select(genreId => new ShowGenre
                            {
                                GenreId = genreId
                            })
                            .ToArray();
                    }

                    if (request.Dto.FilmingLocations != null)
                    {
                        show.FilmingLocations = request.Dto.FilmingLocations
                            .Select(filmingLocationId => new ShowFilmingLocation
                            {
                                FilmingLocationId = filmingLocationId
                            })
                            .ToArray();
                    }
                }

                try
                {
                    await this.repository.SaveChangesAsync();

                    return Result<Unit>.Success(Unit.Value, String.Format(SuccessfullyEditedShow, show.Title));
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(String.Format(FailedEditShow, show.Title));
                }
            }
        }
    }
}