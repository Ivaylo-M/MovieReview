namespace Tests.Shows
{
    using System.Linq.Expressions;
    
    using Moq;
    using MockQueryable.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    
    using Domain;
    using Domain.Enums;
    using Persistence.Repositories;
    using Application.Response;
    using Application.DTOs.Shows;
    
    using static Application.Shows.AllShows;

    [TestFixture]
    public class AllShowsTests
    {
        private Mock<IRepository> repositoryMock;
        private AllShowsHandler handler;
        private IMemoryCache memoryCache;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IRepository>();

            MemoryCacheOptions cacheOptions = new MemoryCacheOptions();

            this.memoryCache = new MemoryCache(cacheOptions);

            this.handler = new AllShowsHandler(this.repositoryMock.Object, this.memoryCache);

            Genre[] genres = 
            {
                new Genre { GenreId = 1, Name = "Comedy" },
                new Genre { GenreId = 2, Name = "Romance" },
                new Genre { GenreId = 3, Name = "Action" },
                new Genre { GenreId = 4, Name = "Drama" },
                new Genre { GenreId = 5, Name = "Sci-fi" },
                new Genre { GenreId = 6, Name = "Fantasy" },
                new Genre { GenreId = 7, Name = "Western"}
            };

            IQueryable<Show> shows = new List<Show>
            {
                new Show
                {
                    ShowId = Guid.Parse("71E18E44-9E18-4D2C-ADF2-43EDC4136290"),
                    ShowType = ShowType.Movie,
                    Title = "Test1",
                    Duration = 98,
                    ReleaseDate = new DateTime(2020, 1, 6),
                    PhotoId = "id",
                    Description = "Description1",
                    Photo = new Photo
                    {
                        PhotoId = "id",
                        Url = "url"
                    },
                    Genres = new List<ShowGenre>
                    {
                        new ShowGenre
                        {
                            GenreId = 1,
                            Genre = genres[0]
                        },
                        new ShowGenre
                        {
                            GenreId = 2,
                            Genre = genres[1]
                        },
                        new ShowGenre
                        {
                            GenreId = 4,
                            Genre = genres[3]
                        }
                    },
                    UserRatings = new List<Rating>
                    {
                        new Rating
                        {
                            Stars = 5,
                            UserId = Guid.Parse("CEE8FD67-06C0-4AC9-85D6-69DA71122315")
                        },
                        new Rating
                        {
                            Stars = 7,
                            UserId = Guid.Parse("A09FEF96-285B-4CA3-9324-58BCD7B27FAC")
                        }
                    }
                },
                new Show
                {
                    ShowId = Guid.Parse("21B9BA7B-3A98-4F21-9C08-AB49488DCE13"),
                    ShowType = ShowType.Movie,
                    Title = "Test2",
                    Duration = 150,
                    ReleaseDate = new DateTime(1978, 5, 6),
                    Description = "Description2",
                    Genres = new List<ShowGenre>
                    {
                        new ShowGenre
                        {
                            GenreId = 1,
                            Genre = genres[0]
                        },
                        new ShowGenre
                        {
                            GenreId = 3,
                            Genre = genres[2]
                        }
                    }
                },
                new Show
                {
                    ShowId = Guid.Parse("26381A34-FCDB-4018-8100-1D22AB7F4B42"),
                    ShowType = ShowType.Movie,
                    Duration = 90,
                    Title = "Test3",
                    ReleaseDate = new DateTime(2000, 4, 10),
                    Description = "Description3",
                    Genres = new List<ShowGenre>
                    {
                        new ShowGenre
                        {
                            GenreId = 1,
                            Genre = genres[0]
                        }
                    }
                },
                new Show
                {
                    ShowId = Guid.Parse("E0B1C3D5-A09E-4442-94CC-DA6C2E626686"),
                    ShowType = ShowType.TVSeries,
                    Title = "Test4",
                    ReleaseDate = new DateTime(1994, 5, 25),
                    Description = "Description4",
                    EndDate = new DateTime(2004, 9, 10),
                    Genres = new List<ShowGenre>
                    {
                        new ShowGenre
                        {
                            GenreId = 5,
                            Genre = genres[4]
                        },
                        new ShowGenre
                        {
                            GenreId = 3,
                            Genre = genres[2]
                        }
                    }
                },
                new Show
                {
                    ShowId = Guid.Parse("CB70A8BA-F4B6-402B-B6D7-5CABD5651C88"),
                    ShowType = ShowType.TVSeries,
                    Title = "Test5",
                    ReleaseDate = new DateTime(1995, 2, 19),
                    Description = "Description5",
                    EndDate = new DateTime(2010, 11, 13),
                    Genres = new List<ShowGenre>
                    {
                        new ShowGenre
                        {
                            GenreId = 6,
                            Genre = genres[5]
                        },
                        new ShowGenre
                        {
                            GenreId = 2,
                            Genre = genres[1]
                        }
                    }
                }
            }
            .AsQueryable();

            SetUpAllShows(shows);
            SetUpUser(true);
        }

        [Test]
        public async Task Handle_ShouldReturnAllShows()
        {
            //Arrange
            AllShowsQuery query = new AllShowsQuery
            {
                UserId = "CEE8FD67-06C0-4AC9-85D6-69DA71122315"
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(5));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test1"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test2"));
            Assert.That(result.Data!.Skip(2).First().Title, Is.EqualTo("Test3"));
            Assert.That(result.Data!.Skip(3).First().Title, Is.EqualTo("Test4"));
            Assert.That(result.Data!.Skip(4).First().Title, Is.EqualTo("Test5"));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyCollection()
        {
            //Arrange
            AllShowsQuery query = new AllShowsQuery();

            IEnumerable<Show> shows = new List<Show>();

            SetUpAllShows(shows.AsQueryable());

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenTheUserIdIsInvalid()
        {
            //Arrange
            AllShowsQuery query = new AllShowsQuery
            {
                UserId = "userId1"
            };

            SetUpUser();

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("The user is not found"));
        }

        [Test]
        public async Task Handle_ShouldReturnAllShows_WithTheCorrectProjection()
        {
            //Arrange
            AllShowsQuery query = new AllShowsQuery
            {
                UserId = "CEE8FD67-06C0-4AC9-85D6-69DA71122315"
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            AllShowsDto actualShow = result.Data!.First();

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(5));
            Assert.That(actualShow.Title, Is.EqualTo("Test1"));
            Assert.That(actualShow.ShowId, Is.EqualTo("71E18E44-9E18-4D2C-ADF2-43EDC4136290".ToLower()));
            Assert.That(actualShow.ShowType, Is.EqualTo(ShowType.Movie));
            Assert.That(actualShow.PhotoUrl, Is.EqualTo("url"));
            Assert.That(actualShow.ReleaseYear, Is.EqualTo(2020));
            Assert.That(actualShow.EndYear, Is.EqualTo(null));
            Assert.That(actualShow.AverageRating, Is.EqualTo(6f));
            Assert.That(actualShow.MyRating, Is.EqualTo(5));
            Assert.That(actualShow.Description, Is.EqualTo("Description1"));
            CollectionAssert.AreEqual(new List<int> { 1, 2, 4 }, actualShow.Genres);
        }

        [Test]
        public async Task Handle_ShouldReturnTheCollection_WhenStoredInTheCache()
        {
            //Arrange
            AllShowsQuery query = new AllShowsQuery
            {
                UserId = "CEE8FD67-06C0-4AC9-85D6-69DA71122315"
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            IEnumerable<AllShowsDto> shows = this.memoryCache.Get<IEnumerable<AllShowsDto>>("Shows")!;

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(shows.Count(), Is.EqualTo(5));
            Assert.That(shows.First().Title, Is.EqualTo("Test1"));
            Assert.That(shows.Skip(1).First().Title, Is.EqualTo("Test2"));
            Assert.That(shows.Skip(2).First().Title, Is.EqualTo("Test3"));
            Assert.That(shows.Skip(3).First().Title, Is.EqualTo("Test4"));
            Assert.That(shows.Skip(4).First().Title, Is.EqualTo("Test5"));
        }

        private void SetUpUser(bool value = false)
        {
            this.repositoryMock
                .Setup(r => r.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(value);
        }

        private void SetUpAllShows(IQueryable<Show> shows)
        {
            TestAsyncEnumerableEfCore<Show> queryable = new TestAsyncEnumerableEfCore<Show>(shows);

            this.repositoryMock
                .Setup(r => r.All<Show>())
                .Returns(queryable);
        }
    }
}