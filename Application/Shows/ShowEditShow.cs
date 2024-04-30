namespace Application.Shows
{
    using Application.DTOs.CountriesOfOrigin;
    using Application.DTOs.FilmingLocations;
    using Application.DTOs.Genres;
    using Application.DTOs.Languages;
    using Application.DTOs.Photos;
    using Application.DTOs.Shows;
    using Application.DTOs.ShowTypes;
    using Application.Response;
    using Domain;
    using Domain.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using static Common.ExceptionMessages.Show;

    public class ShowEditShow
    {
        public class ShowEditShowQuery : IRequest<Result<ShowEditShowDto>>
        {
            public string ShowId { get; set; } = null!;
        }

        public class ShowEditShowHandler : IRequestHandler<ShowEditShowQuery, Result<ShowEditShowDto>>
        {
            private readonly IRepository repository;

            public ShowEditShowHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<ShowEditShowDto>> Handle(ShowEditShowQuery request, CancellationToken cancellationToken)
            {
                Show? show = await this.repository.All<Show>(s => s.ShowId.ToString().Equals(request.ShowId.ToLower()))
                    .Include(s => s.Series)
                    .Include(s => s.Genres)
                    .Include(s => s.FilmingLocations)
                    .Include(s => s.CountriesOfOrigin)
                    .Include(s => s.Languages)
                    .Include(s => s.Photo)
                    .FirstOrDefaultAsync(CancellationToken.None);

                if (show == null)
                {
                    return Result<ShowEditShowDto>.Failure(ShowNotFound);
                }

                ShowEditShowDto showEditShowDto = new()
                {
                    Title = show.Title,
                    Description = show.Description,
                    ReleaseDate = show.ReleaseDate,
                    EndDate = show.EndDate,
                    Season = show.Season,
                    Duration = show.Duration,
                    ShowType = new ShowTypeDto
                    {
                        Id = (int)show.ShowType,
                        Name = show.ShowType.ToString()
                    }
                };

                if (show.PhotoId != null)
                {
                    showEditShowDto.Photo = new PhotoDto
                    {
                        Id = show.PhotoId,
                        Url = show.Photo!.Url
                    };
                }

                if (show.ShowType != ShowType.Episode)
                {
                    showEditShowDto.Genres = this.repository.All<Genre>()
                        .Select(g => new GenreDto
                        {
                            GenreId = g.GenreId,
                            Name = g.Name,
                            HasValue = show.Genres.Any(sg => sg.GenreId == g.GenreId)
                        });

                    showEditShowDto.FilmingLocations = this.repository.All<FilmingLocation>()
                        .Select(fl => new FilmingLocationDto
                        {
                            FilmingLocationId = fl.FilmingLocationId,
                            Name = fl.Name,
                            HasValue = show.FilmingLocations.Any(sfl => sfl.FilmingLocationId == fl.FilmingLocationId)
                        });

                    showEditShowDto.Languages = this.repository.All<Language>()
                        .Select(l => new LanguageDto
                        {
                            LanguageId = l.LanguageId,
                            Name = l.Name,
                            HasValue = show.Languages.Any(sl => sl.LanguageId == l.LanguageId)
                        });

                    showEditShowDto.CountriesOfOrigin = this.repository.All<CountryOfOrigin>()
                        .Select(coo => new CountryOfOriginDto
                        {
                            CountryOfOriginId = coo.CountryOfOriginId,
                            Name = coo.Name,
                            HasValue = show.CountriesOfOrigin.Any(scoo => scoo.CountryOfOriginId == coo.CountryOfOriginId)
                        });
                }
                else
                {
                    showEditShowDto.Series = new TVSeriesDto
                    {
                        Id = show.SeriesId.ToString()!,
                        Title = show.Series!.Title,
                    };
                }

                return Result<ShowEditShowDto>.Success(showEditShowDto);
            }
        }
    }
}