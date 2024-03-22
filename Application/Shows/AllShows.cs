namespace Application.Shows
{
    using Application.DTOs.Shows;
    using Application.Response;
    using MediatR;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    public class AllShows
    {
        public class AllShowsQuery : IRequest<Result<IEnumerable<AllShowsDto>>>
        {

        }

        public class AllShowsHandler : IRequestHandler<AllShowsQuery, Result<IEnumerable<AllShowsDto>>>
        {
            private readonly IRepository repository;

            public AllShowsHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public Task<Result<IEnumerable<AllShowsDto>>> Handle(AllShowsQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
