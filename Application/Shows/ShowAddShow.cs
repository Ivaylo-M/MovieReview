namespace Application.Shows
{
    using Application.DTOs.CountriesOfOrigin;
    using Application.DTOs.FilmingLocations;
    using Application.DTOs.Genres;
    using Application.DTOs.Languages;
    using Application.DTOs.Shows;
    using Application.DTOs.ShowTypes;
    using Application.Response;
    using Domain;
    using Domain.Enums;
    using MediatR;
    using Persistence.Repositories;
    using static Common.ExceptionMessages.Show;
    using static Common.ExceptionMessages.ShowType;

    public class ShowAddShow
    {
        public class ShowAddShowQuery : IRequest<Result<ShowAddShowDto>>
        {
            public string? TVSeriesId { get; set; }

            public ShowType ShowType { get; set; }
        }

        public class ShowAddShowHandler : IRequestHandler<ShowAddShowQuery, Result<ShowAddShowDto>>
        {
            private readonly IRepository repository;

            public ShowAddShowHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<ShowAddShowDto>> Handle(ShowAddShowQuery request, CancellationToken cancellationToken)
            {
                if (request.ShowType == default)
                {
                    return Result<ShowAddShowDto>.Failure(InvalidShowType);
                }

                ShowAddShowDto showAddShowDto = new()
                {
                    ShowType = new ShowTypeDto
                    {
                        Id = (int)request.ShowType,
                        Name = request.ShowType.ToString()
                    }
                };

                if (request.ShowType == ShowType.Episode)
                {
                    Show? tvSeries = await this.repository.FirstOrDefaultAsync<Show>(s => s.ShowId.Equals(request.TVSeriesId!.ToLower()));

                    if (tvSeries == null)
                    {
                        return Result<ShowAddShowDto>.Failure(ShowNotFound);
                    }

                    showAddShowDto.Series = new TVSeriesDto
                    {
                        Id = request.TVSeriesId!,
                        Title = tvSeries.Title
                    };
                }

                if (request.ShowType != ShowType.Episode)
                {
                    showAddShowDto.Genres = this.repository.All<Genre>()
                        .Select(g => new GenreDto
                        {
                            GenreId = g.GenreId,
                            Name = g.Name
                        });

                    showAddShowDto.FilmingLocations = this.repository.All<FilmingLocation>()
                        .Select(fl => new FilmingLocationDto
                        {
                            FilmingLocationId = fl.FilmingLocationId,
                            Name = fl.Name
                        });

                    showAddShowDto.Languages = this.repository.All<Language>()
                        .Select(l => new LanguageDto
                        {
                            LanguageId = l.LanguageId,
                            Name = l.Name
                        });

                    showAddShowDto.CountriesOfOrigin = this.repository.All<CountryOfOrigin>()
                        .Select(coo => new CountryOfOriginDto
                        {
                            CountryOfOriginId = coo.CountryOfOriginId,
                            Name = coo.Name
                        });
                }

                return await Task.FromResult(Result<ShowAddShowDto>.Success(showAddShowDto));
            }
        }
    }
}
