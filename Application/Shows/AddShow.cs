namespace Application.Shows
{
    using Application.DTOs.Shows;
    using Application.Response;
    using MediatR;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    public class AddShow
    {
        public class AddShowCommand : IRequest<Result<Unit>>
        {
            public string UserId { get; set; } = null!;

            public AddShowDto MyProperty { get; set; } = null!;
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
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
