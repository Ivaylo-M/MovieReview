namespace Tests.Shows
{
    using Application.DTOs.Shows;
    using Application.Response;
    using Domain;
    using Domain.Enums;
    using MediatR;
    using Moq;
    using Persistence.Repositories;
    using System.Linq.Expressions;
    using static Application.Shows.AddShow;

    public class AddShowTests
    {
        private Mock<IRepository> repositoryMock;
        private AddShowHandler handler;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IRepository>();

            this.handler = new AddShowHandler(this.repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfSav3ChangesFails()
        {
            //Arrange
            AddShowCommand command = new AddShowCommand
            {
                Dto = new AddShowDto
                {
                    Title = "Test1",
                    ShowType = ShowType.Movie,
                    Description = "This is the description of Test1",
                    Duration = 145,
                    ReleaseDate = new DateTime(2022, 3, 20),
                    Genres = new List<int> { 2, 3 },
                    FilmingLocations = new List<int> { 1, 4 },
                    CountriesOfOrigin = new List<int> { 5, 6 },
                    Languages = new List<int> { 1 }
                }
            };

            this.repositoryMock
                .Setup(r => r.SaveChangesAsync()).Throws(new Exception("Simulating save failure"));

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo($"Failed to create show - {command.Dto.Title}"));
            this.repositoryMock.Verify(r => r.AddAsync(It.IsAny<Show>()), Times.Once);
            this.repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldAddMovie_WithGivenCorrectData()
        {
            //Arrange
            AddShowCommand command = new AddShowCommand
            {
                Dto = new AddShowDto
                {
                    Title = "Test1",
                    ShowType = ShowType.Movie,
                    Description = "This is the description of Test1",
                    Duration = 145,
                    ReleaseDate = new DateTime(2022, 3, 20),
                    Genres = new List<int> { 2, 3 },
                    FilmingLocations = new List<int> { 1, 4 },
                    CountriesOfOrigin = new List<int> { 5, 6 },
                    Languages = new List<int> { 1 }
                }
            };

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo($"Successfully added Test1"));
            this.repositoryMock.Verify(r => r.AddAsync(It.IsAny<Show>()), Times.Once);
            this.repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldAddTvSeries_WithGivenCorrectData()
        {
            //Arrange
            AddShowCommand command = new AddShowCommand
            {
                Dto = new AddShowDto
                {
                    Title = "Test2",
                    ShowType = ShowType.TVSeries,
                    Description = "This is the description of Test2",
                    ReleaseDate = new DateTime(2022, 3, 20),
                    EndDate = new DateTime(2024, 5, 6),
                    Genres = new List<int> { 2, 3 },
                    FilmingLocations = new List<int> { 1, 4 },
                    CountriesOfOrigin = new List<int> { 5, 6 },
                    Languages = new List<int> { 1 }
                }
            };

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo($"Successfully added Test2"));
            this.repositoryMock.Verify(r => r.AddAsync(It.IsAny<Show>()), Times.Once);
            this.repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldAddTvSeriesEpisode_WithGivenCorrectData()
        {
            //Arrange
            AddShowCommand command = new AddShowCommand
            {
                Dto = new AddShowDto
                {
                    Title = "Test3",
                    ShowType = ShowType.Episode,
                    Description = "This is the description of Test3",
                    ReleaseDate = new DateTime(2022, 3, 20),
                    Duration = 23,
                    Season = 1,
                    SeriesId = "3A2FDAAC-2AD0-45E1-A9E4-260C001BFA57"
                }
            };

            this.repositoryMock
                .Setup(r => r.AnyAsync(It.IsAny<Expression<Func<Show, bool>>>()))
                .ReturnsAsync(true);

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo($"Successfully added Test3"));
            this.repositoryMock.Verify(r => r.AddAsync(It.IsAny<Show>()), Times.Once);
            this.repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenSeriesIdIsInvalid()
        {
            //Arrange
            AddShowCommand command = new AddShowCommand
            {
                Dto = new AddShowDto
                {
                    Title = "Test3",
                    ShowType = ShowType.Episode,
                    Description = "This is the description of Test3",
                    ReleaseDate = new DateTime(2022, 3, 20),
                    Duration = 23,
                    Season = 1,
                    SeriesId = "seriesId"
                }
            };

            this.repositoryMock
                .Setup(r => r.AnyAsync(It.IsAny<Expression<Func<Show, bool>>>()))
                .ReturnsAsync(false);

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo($"This show does not exist! Please select an existing one"));
        }
    }
}
