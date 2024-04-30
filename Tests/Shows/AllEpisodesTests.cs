namespace Tests.Shows
{
    using Application.DTOs.Shows;
    using Application.Response;
    using Domain;
    using Domain.Enums;
    using MockQueryable.EntityFrameworkCore;
    using Moq;
    using Persistence.Repositories;
    using System.Linq.Expressions;
    using Tests.Comparers;
    using Tests.Comparers.Shows;
    using static Application.Shows.AllEpisodes;

    public class AllEpisodesTests
    {
        private Mock<IRepository> repositoryMock;
        private AllEpisodesHandler handler;
        private Show tvSeries;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IRepository>();
            this.handler = new AllEpisodesHandler(this.repositoryMock.Object);

            this.tvSeries = new Show
            {
                ShowId = Guid.Parse("0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6"),
                Title = "TV Series Title",
                Episodes =
                [
                    new Show
                    {
                        ShowId = Guid.Parse("3483F9F3-F068-4039-8E81-596BC6905B21"),
                        Duration = 23,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2020, 5, 4),
                        Season = 1
                    },
                    new Show
                    {
                        ShowId = Guid.Parse("D6EFF920-435F-441F-BBEF-DECF94E920D5"),
                        Duration = 22,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2020, 7, 8),
                        Season = 1
                    },
                    new Show
                    {
                        ShowId = Guid.Parse("D6EFF920-435F-441F-BBEF-DECF94E920D5"),
                        Duration = 22,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2020, 7, 8),
                        Season = 1
                    },
                    new Show
                    {
                        ShowId = Guid.Parse("D6EFF920-435F-441F-BBEF-DECF94E920D5"),
                        Duration = 22,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2020, 7, 8),
                        Season = 1
                    },
                    new Show
                    {
                        ShowId = Guid.Parse("D6EFF920-435F-441F-BBEF-DECF94E920D5"),
                        Duration = 22,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2020, 7, 8),
                        Season = 1
                    },
                    new Show
                    {
                        ShowId = Guid.Parse("36F69834-6457-4DC6-90A2-FADFFA2073EB"),
                        Duration = 22,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2021, 1, 7),
                        Season = 2
                    },
                    new Show
                    {
                        ShowId = Guid.Parse("36F69834-6457-4DC6-90A2-FADFFA2073EB"),
                        Duration = 22,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2021, 1, 7),
                        Season = 2
                    },
                    new Show
                    {
                        ShowId = Guid.Parse("36F69834-6457-4DC6-90A2-FADFFA2073EB"),
                        Duration = 22,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2021, 1, 7),
                        Season = 2
                    },
                    new Show
                    {
                        ShowId = Guid.Parse("36F69834-6457-4DC6-90A2-FADFFA2073EB"),
                        Duration = 22,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2021, 1, 7),
                        Season = 2
                    },
                    new Show
                    {
                        ShowId = Guid.Parse("1E866DF4-BD4B-42E4-89E8-B23775BAB089"),
                        Duration = 22,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2021, 2, 7),
                        Season = 2
                    }
                ]
            };
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfTheShowDoesNotExist()
        {
            //Arrange
            SetUpReturningTVSeries(null);

            AllEpisodesQuery query = new();

            //Act
            Result<AllEpisodesDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("This show does not exist! Please select an existing one"));
            });
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfTheUserDoesNotExist()
        {
            //Arrange
            SetUpReturningTVSeries(this.tvSeries);
            SetUpCheckingUser(false);

            AllEpisodesQuery query = new()
            {
                ShowId = "0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6",
                UserId = "userId"
            };

            //Act
            Result<AllEpisodesDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("The user is not found"));
            });
        }

        [Test]
        public async Task Handle_ShouldReturnDto_IfTheDataIsCorrect()
        {
            //Arrange
            SetUpReturningTVSeries(this.tvSeries);

            AllEpisodesQuery query = new()
            {
                ShowId = "0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6"
            };

            TVSeriesDto expectedTVSeriesDto = new();

            IEnumerable<EpisodeDto> expectedEpisodes = [];

            //Act
            Result<AllEpisodesDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.Series, Is.EqualTo(expectedTVSeriesDto).Using(new TVSeriesDtoComparer()));
                Assert.That(result.Data!.Episodes, Is.EquivalentTo(expectedEpisodes).Using(new EpisodeDtoComparer()));
            });
        }

        private void SetUpCheckingUser(bool value)
        {
            this.repositoryMock
                .Setup(r => r.AnyAsync(It.IsAny<Expression<Func<Show, bool>>>()))
                .ReturnsAsync(value);
        }

        private void SetUpReturningTVSeries(Show? show)
        {
            IQueryable<Show> shows = new List<Show> { show! }.AsQueryable();

            TestAsyncEnumerableEfCore<Show> queryable = new(shows);

            this.repositoryMock
                .Setup(r => r.All(It.IsAny<Expression<Func<Show, bool>>>()))
                .Returns(queryable);
        }
    }
}