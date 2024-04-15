namespace Application.Shows
{
    using Application.DTOs.Shows;
    using Application.Response;
    using MediatR;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

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

            public Task<Result<ShowDetailsDto>> Handle(ShowDetailsQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
