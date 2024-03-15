namespace Tests.User
{
    using Moq;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Domain;
    using Application.Response;
    using Application.DTOs.Users;
    using Application.Services.Contracts;

    using static Application.Users.RegisterUser;

    [TestFixture]
    public class RegisterUserTests
    {
        private Mock<ITokenService> tokenServiceMock;
        private Mock<UserManager<User>> userManagerMock;
        private Mock<SignInManager<User>> userSignInManagerMock;
        private RegisterUserHandler handler;

        [SetUp]
        public void SetUp()
        {
            this.tokenServiceMock = new Mock<ITokenService>();

            this.userManagerMock = new Mock<UserManager<User>>(
                new Mock<IUserStore<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<User>>().Object,
                new IUserValidator<User>[0],
                new IPasswordValidator<User>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<User>>>().Object
            );

            this.userSignInManagerMock = new Mock<SignInManager<User>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null!, null!, null!, null!);

            handler = new RegisterUserHandler(
                this.userManagerMock.Object,
                this.userSignInManagerMock.Object,
                this.tokenServiceMock.Object);
        }

        [Test]
        public async Task Handle_InvalidCommand_WhenUserManagerThrowsError()
        {
            var identityError = new IdentityError
            {
                Code = "ErrorCode",
                Description = "Error description"
            };

            this.userManagerMock.
                Setup(um => um.
                CreateAsync(It.IsAny<User>(),
                It.IsAny<string>())).
                ReturnsAsync(IdentityResult.Failed(identityError));

            var result = await handler.
                Handle(new RegisterUserCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("User registration failed"));
        }

        [Test]
        public async Task Handle_InvalidCommand_WhenSignInManagerThrowsError()
        {
            this.userSignInManagerMock
                .Setup(sm => sm.SignInAsync(It.IsAny<User>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Throws(new Exception());

            Result<UserDto> result = await this.handler.Handle(new RegisterUserCommand(), CancellationToken.None);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("User registration failed"));
        }

        [Test]
        public async Task Handle_ValidCommand_RegistersUser()
        {
            RegisterUserCommand registerUserCommand = new RegisterUserCommand
            {
                Name = "Test name"
            };

            this.userManagerMock
                .Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            Result<UserDto> result = await this.handler.Handle(registerUserCommand, CancellationToken.None);

            Assert.IsTrue(result.IsSuccess);
            Assert.That(registerUserCommand.Name, Is.EqualTo(result.Data!.Name));
        }
    }
}
