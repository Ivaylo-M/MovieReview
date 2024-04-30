namespace Application.Shows
{
    using Application.DTOs.Shows;
    using Application.Response;
    using MediatR;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    public class AllEpisodes
    {
        public class AllEpisodesQuery : IRequest<Result<AllEpisodesDto>>
        {
            public string ShowId { get; set; } = null!;

            public string UserId { get; set; } = null!;
        }

        public class AllEpisodesHandler : IRequestHandler<AllEpisodesQuery, Result<AllEpisodesDto>>
        {
            private readonly IRepository repository;

            public AllEpisodesHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public Task<Result<AllEpisodesDto>> Handle(AllEpisodesQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
