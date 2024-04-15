namespace Tests.Shows
{
    using System.Linq.Expressions;
    using Application.DTOs.Shows;
    using Application.Response;
    using Domain;
    using MockQueryable.EntityFrameworkCore;
    using Moq;
    using Persistence.Repositories;
    using static Application.Shows.ShowDetails;
    
    public class ShowDetailsTests
    {
        private Mock<IRepository> repositoryMock;
        private ShowDetailsHandler handler;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IRepository>();
            this.handler = new ShowDetailsHandler(this.repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfTheShowDoesNotExist()
        {
            //Arrange
            SetUpReturningShow(null);
            ShowDetailsQuery query = new ShowDetailsQuery();

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("This show does not exist! Please select an existing one"));
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfTheUserDoesNotExist()
        {
            //Arrange
            SetUpReturningShow(null);
            SetUpCheckingUser(false);
            ShowDetailsQuery query = new ShowDetailsQuery
            {
                ShowId = ""
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("The user is not found"));
        }

        private void SetUpReturningShow(Show? show)
        {
            IQueryable<Show> shows = new List<Show> { show! }.AsQueryable();

            TestAsyncEnumerableEfCore<Show> queryable = new TestAsyncEnumerableEfCore<Show>(shows);

            this.repositoryMock
                .Setup(r => r.All(It.IsAny<Expression<Func<Show, bool>>>()))
                .Returns(queryable);
        }

        private void SetUpCheckingUser(bool value)
        {
            this.repositoryMock
                .Setup(r => r.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(value);
        }
    }
}
