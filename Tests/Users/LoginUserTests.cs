namespace Tests.User
{
    using System.Linq.Expressions;

    using Moq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MockQueryable.EntityFrameworkCore;

    using Domain;
    using Persistence.Repositories;
    using Application.Response;
    using Application.DTOs.Users;
    using Application.Services.Contracts;

    using static Application.Users.LoginUser;

    public class LoginUserTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<ITokenService> tokenServiceMock;
        private Mock<SignInManager<User>> signInManagerMock;
        private LoginUserHandler handler;
        private User user;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();
            tokenServiceMock = new Mock<ITokenService>();

            var userManagerMock = new Mock<UserManager<User>>(
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
            signInManagerMock = new Mock<SignInManager<User>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null!, null!, null!, null!);

            handler = new LoginUserHandler(
                signInManagerMock.Object,
                repositoryMock.Object,
                tokenServiceMock.Object);
            user = new User()
            {
                UserName = "Test",
            };
        }

        [Test]
        public async Task Handle_ValidCommand_LoginUser()
        {
            //Arrange
            var queryable = new List<User> { user }.AsQueryable();
            SetUpReturningUser(repositoryMock, queryable);
            SetUpSignInManager(signInManagerMock, SignInResult.Success);

            //Act
            var result = await handler.
                Handle(new LoginUserCommand(), CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.Data!.Name, Is.EqualTo(user.UserName));
            VerifySignInManager(user, result, signInManagerMock);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenUserIsNotFound()
        {
            //Arrange
            var queryable = new List<User> { }.AsQueryable();
            SetUpReturningUser(repositoryMock, queryable);

            //Act
            var result = await handler.
                Handle(new LoginUserCommand(), CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("The user is not found"));
        }

        [Test]
        public async Task Handle_ReturnsError_WhenSignInManager_ReturnsError()
        {
            //Arrange
            var queryable = new List<User> { user }.AsQueryable();
            SetUpReturningUser(repositoryMock, queryable);
            SetUpSignInManager(signInManagerMock, SignInResult.Failed);

            //Act
            var result = await handler.
                Handle(new LoginUserCommand(), CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Invalid email or password"));
            VerifySignInManager(user, result, signInManagerMock);
        }

        [Test]
        public async Task Handle_ReturnsError_WhenSignInManager_ThrowsError()
        {
            //Arrange
            var queryable = new List<User> { user }.AsQueryable();
            SetUpReturningUser(repositoryMock, queryable);
            signInManagerMock.
                Setup(sm => sm.PasswordSignInAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>())).
                    ThrowsAsync(new Exception());

            //Act
            var result = await handler.
                Handle(new LoginUserCommand(), CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Invalid email or password"));
            VerifySignInManager(user, result, signInManagerMock);
        }

        private static void SetUpReturningUser(
             Mock<IRepository> repositoryMock,
             IQueryable<User> queryable)
        {
            var asyncEnumerable =
                new TestAsyncEnumerableEfCore<User>(queryable);

            repositoryMock.
                Setup(r => r.All(It.IsAny<Expression<Func<User, bool>>>())).
                Returns(asyncEnumerable);
        }

        private static void SetUpSignInManager(
            Mock<SignInManager<User>> signInManagerMock,
            SignInResult result)
        {
            signInManagerMock.
                Setup(sm => sm.PasswordSignInAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>())).
                    ReturnsAsync(result);
        }

        private static void VerifySignInManager(
            User user,
            Result<UserDto> result,
            Mock<SignInManager<User>> signInManagerMock)
        {
            signInManagerMock.
                Verify(sm => sm.PasswordSignInAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>()),
                    Times.Once());
        }
    }
}
