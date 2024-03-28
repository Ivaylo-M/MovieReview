namespace Application.Shows
{
    using Application.DTOs.Shows;
    using Application.Response;
    using Domain.Enums;
    using MediatR;
    using Microsoft.Extensions.Caching.Memory;
    using System.Threading;
    using System.Threading.Tasks;
    using static Common.ExceptionMessages.MemoryCache;

    public class FilterShows
    {
        public class FilterShowsQuery : IRequest<Result<IEnumerable<AllShowsDto>>>
        {
            public IEnumerable<ShowType>? ShowTypes { get; set; }

            public IEnumerable<int>? Genres { get; set; }

            public int? MinReleaseYear { get; set; }

            public int? MaxReleaseYear { get; set; }
        }

        public class FilterShowsHandler : IRequestHandler<FilterShowsQuery, Result<IEnumerable<AllShowsDto>>>
        {
            private readonly IMemoryCache memoryCache;

            public FilterShowsHandler(IMemoryCache memoryCache)
            {
                this.memoryCache = memoryCache;
            }

            public async Task<Result<IEnumerable<AllShowsDto>>> Handle(FilterShowsQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<AllShowsDto>? shows = this.memoryCache.Get<IEnumerable<AllShowsDto>>("Shows");

                if (shows == null)
                {
                    return Result<IEnumerable<AllShowsDto>>.Failure(NullValue);
                }

                if (request.ShowTypes != null)
                {
                    shows = shows.Where(s => request.ShowTypes.Contains(s.ShowType));
                }

                if (request.Genres != null)
                {
                    shows = shows.Where(s => s.Genres.Any(g => request.Genres.Contains(g)));
                }

                if (request.MinReleaseYear.HasValue)
                {
                    shows = shows.Where(s => s.ReleaseYear >= request.MinReleaseYear);
                }

                if (request.MaxReleaseYear.HasValue)
                {
                    shows = shows.Where(s => s.ReleaseYear <= request.MaxReleaseYear);
                }

                return Result<IEnumerable<AllShowsDto>>.Success(shows);
            }
        }
    }
}
