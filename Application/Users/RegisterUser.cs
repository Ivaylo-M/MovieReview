﻿namespace Application.Users
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Identity;

    using Domain;
    using Application.Response;
    using Application.Services.Contracts;

    using static Common.FailMessages.User;
    using Application.DTOs.Users;

    public class RegisterUser
    {
        public class RegisterUserCommand : IRequest<Result<UserDto>> 
        {
            public string Email { get; set; } = null!;

            public string Password { get; set; } = null!;

            public string Name { get; set; } = null!;
        }

        public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<UserDto>>
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

            public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                User user = new User
                {
                    Email = request.Email,
                    UserName = request.Name
                };

                try
                {
                    IdentityResult result =
                        await userManager.CreateAsync(user, request.Password);

                    if (!result.Succeeded)
                    {
                        return Result<UserDto>.Failure(FailedRegister);
                    }

                    await this.signInManager.SignInAsync(user, false);

                    UserDto userDto = new UserDto
                    {
                        Name = user.UserName,
                        Token = this.tokenService.CreateToken(user)
                    };

                    return Result<UserDto>.Success(userDto);
                }
                catch (Exception)
                {
                    return Result<UserDto>.Failure(FailedRegister);
                }

            }
        }
    }
}
