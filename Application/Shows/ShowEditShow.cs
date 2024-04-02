namespace Application.Shows
{
    using Application.DTOs.Shows;
    using Application.Response;
    using Domain;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

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

            public Task<Result<ShowEditShowDto>> Handle(ShowEditShowQuery request, CancellationToken cancellationToken)
            {
                //Show? show = await this.repository.All<Show>(s => s.ShowId.ToString().Equals(request.ShowId.ToLower()))
                //    .Include(s => s.Series)
                //    .Include(s => s.Genres)
                //    .Include(s => s.FilmingLocations)
                //    .Include(s => s.CountriesOfOrigin)
                //    .Include(s => s.Languages)
                //    .Include(s => s.Photo)
                //    .FirstOrDefaultAsync();

                throw new NotImplementedException();
            }
        }
    }
}