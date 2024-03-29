﻿namespace Tests.User
{
    using Moq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Domain;

    using static Application.Users.LogoutUser;

    [TestFixture]
    public class LogoutUserTests
    {
        private Mock<SignInManager<User>> signInManagerMock;
        private Mock<UserManager<User>> userManagerMock;
        private LogoutHandler handler;
        private User user;

        [SetUp]
        public void Setup()
        {
            userManagerMock = new Mock<UserManager<User>>(
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
                null, null, null, null);
            handler = new LogoutHandler(
                signInManagerMock.Object, userManagerMock.Object);
            user = new User()
            {
                UserName = "Test user"
            };
        }

        [Test]
        public async Task Handle_ValidCommand_LogoutUser()
        {
            //Arrange
            SetUpReturningUser(userManagerMock, user);

            //Act
            var result = await handler.Handle(new LogoutUserCommand(), CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
            signInManagerMock.Verify(sm => sm.SignOutAsync(), Times.Once());
        }

        [Test]
        public async Task Handle_InvalidUser_ReturnsErrorResult()
        {
            //Arrange and Act
            var result = await handler.Handle(new LogoutUserCommand(), CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("The user is not authenticated"));
        }

        [Test]
        public async Task Handle_FailedToLogoutUser_ReturnsFailureResult()
        {
            //Arrange
            SetUpReturningUser(userManagerMock, user);
            signInManagerMock.
                Setup(sm => sm.SignOutAsync()).
                ThrowsAsync(new Exception());

            //Act
            var result = await handler.Handle(new LogoutUserCommand(), CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Failed to logout"));
        }

        private static void SetUpReturningUser(
            Mock<UserManager<User>> userManagerMock,
            User user)
        {
            userManagerMock.
                Setup(um => um.FindByIdAsync(It.IsAny<string>())).
                ReturnsAsync(user);
        }
    }
}
