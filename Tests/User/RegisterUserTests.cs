namespace Tests.User
{
    using Moq;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    
    using Domain;
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
    }
}
