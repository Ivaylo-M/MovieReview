﻿namespace Application.Users
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    
    using Domain;
    using Persistence.Repositories;
    using Application.Response;
    using Application.DTOs.Users;
    using Application.Services.Contracts;

    using static Common.FailMessages.User;
    using static Common.ExceptionMessages.User;

    public class LoginUser
    {
        public class LoginUserCommand : IRequest<Result<UserDto>>
        {
            public string Email { get; set; } = null!;

            public string Password { get; set; } = null!;

            public bool RememberMe { get; set; }
        }

        public class LoginUserHandler : IRequestHandler<LoginUserCommand, Result<UserDto>>
        {
            private readonly SignInManager<User> signInManager;
            private readonly IRepository repository;
            private readonly ITokenService tokenService;

            public LoginUserHandler(
                SignInManager<User> signInManager,
                IRepository repository,
                ITokenService tokenService)
            {
                this.repository = repository;
                this.signInManager = signInManager;
                this.tokenService = tokenService;
            }

            public async Task<Result<UserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await this.repository
                    .All<User>(u => u.Email == request.Email)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Result<UserDto>.Failure(UserNotFound);
                }

                try
                {
                    SignInResult result = await signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);

                    if (!result.Succeeded)
                    {
                        return Result<UserDto>.Failure(FailedLogin);
                    }

                    UserDto userDto = new UserDto
                    {
                        Name = user.UserName!,
                        Token = this.tokenService.CreateToken(user),

                    };

                    return Result<UserDto>.Success(userDto);
                }
                catch (Exception)
                {
                    return Result<UserDto>.Failure(FailedLogin);
                }
            }
        }
    }
}
