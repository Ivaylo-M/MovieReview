namespace Application.Users
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    
    using Domain;
    using Application.Response;
    using Application.Services.Contracts;

    public class RegisterUser
    {
        public class RegisterUserCommand : IRequest<Result<Unit>> { }

        public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<Unit>>
        {
            private readonly UserManager<User> userManager;
            private readonly SignInManager<User> signInManager;
            private readonly ITokenService tokenService;

            public RegisterUserHandler(
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                ITokenService tokenService)
            {
                this.userManager = userManager;
                this.signInManager = signInManager;
                this.tokenService = tokenService;
            }

            public async Task<Result<Unit>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
