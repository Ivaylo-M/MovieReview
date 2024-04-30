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
    using Tests.Comparers.Shows;

    [TestFixture]
    public class AllShowsTests
    {
        private Mock<IRepository> repositoryMock;
        private AllShowsHandler handler;
        private IMemoryCache memoryCache;
        private IEnumerable<AllShowsDto> expectedShows;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IRepository>();

            MemoryCacheOptions cacheOptions = new();

            this.memoryCache = new MemoryCache(cacheOptions);

            this.handler = new AllShowsHandler(this.repositoryMock.Object, this.memoryCache);

            Dictionary<int, Genre> genres = new()
            {
                { 1, new Genre { GenreId = 1, Name = "Comedy" } },
                { 2, new Genre { GenreId = 2, Name = "Romance" } },
                { 3, new Genre { GenreId = 3, Name = "Action" } },
                { 4, new Genre { GenreId = 4, Name = "Drama" } },
                { 5, new Genre { GenreId = 5, Name = "Sci-fi" } },
                { 6, new Genre { GenreId = 6, Name = "Fantasy" } },
                { 7, new Genre { GenreId = 7, Name = "Western"} }
            };

            IEnumerable<Show> shows =
            [
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
                    Genres =
                    [
                        new ShowGenre
                        {
                            GenreId = 1,
                            Genre = genres[1]
                        },
                        new ShowGenre
                        {
                            GenreId = 2,
                            Genre = genres[2]
                        },
                        new ShowGenre
                        {
                            GenreId = 4,
                            Genre = genres[4]
                        }
                    ],
                    UserRatings =
                    [
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
                    ]
                },
                new Show
                {
                    ShowId = Guid.Parse("21B9BA7B-3A98-4F21-9C08-AB49488DCE13"),
                    ShowType = ShowType.Movie,
                    Title = "Test2",
                    Duration = 150,
                    ReleaseDate = new DateTime(1978, 5, 6),
                    Description = "Description2",
                    PhotoId = "id",
                    Photo = new Photo
                    {
                        PhotoId = "id",
                        Url = "url"
                    },
                    Genres =
                    [
                        new ShowGenre
                        {
                            GenreId = 1,
                            Genre = genres[1]
                        },
                        new ShowGenre
                        {
                            GenreId = 3,
                            Genre = genres[3]
                        }
                    ],
                    UserRatings =
                    [
                        new Rating
                        {
                            Stars = 4,
                            UserId = Guid.Parse("CEE8FD67-06C0-4AC9-85D6-69DA71122315")
                        },
                        new Rating
                        {
                            Stars = 8,
                            UserId = Guid.Parse("A09FEF96-285B-4CA3-9324-58BCD7B27FAC")
                        },
                        new Rating
                        {
                            Stars = 10,
                            UserId = Guid.Parse("F3E39F69-E828-4017-A753-8E029512A13D")
                        }
                    ]
                },
                new Show
                {
                    ShowId = Guid.Parse("26381A34-FCDB-4018-8100-1D22AB7F4B42"),
                    ShowType = ShowType.Movie,
                    Duration = 90,
                    Title = "Test3",
                    ReleaseDate = new DateTime(2000, 4, 10),
                    PhotoId = "id",
                    Photo = new Photo
                    {
                        PhotoId = "id",
                        Url = "url"
                    },
                    Description = "Description3",
                    Genres =
                    [
                        new ShowGenre
                        {
                            GenreId = 1,
                            Genre = genres[1]
                        }
                    ],
                    UserRatings =
                    [
                        new Rating
                        {
                            Stars = 4,
                            UserId = Guid.Parse("CEE8FD67-06C0-4AC9-85D6-69DA71122315")
                        },
                        new Rating
                        {
                            Stars = 8,
                            UserId = Guid.Parse("A09FEF96-285B-4CA3-9324-58BCD7B27FAC")
                        },
                        new Rating
                        {
                            Stars = 10,
                            UserId = Guid.Parse("F3E39F69-E828-4017-A753-8E029512A13D")
                        }
                    ]
                },
                new Show
                {
                    ShowId = Guid.Parse("E0B1C3D5-A09E-4442-94CC-DA6C2E626686"),
                    ShowType = ShowType.TVSeries,
                    Title = "Test4",
                    ReleaseDate = new DateTime(1994, 5, 25),
                    PhotoId = "id",
                    Photo = new Photo
                    {
                        PhotoId = "id",
                        Url = "url"
                    },
                    Description = "Description4",
                    EndDate = new DateTime(2004, 9, 10),
                    Genres =
                    [
                        new ShowGenre
                        {
                            GenreId = 3,
                            Genre = genres[3]
                        },
                        new ShowGenre
                        {
                            GenreId = 5,
                            Genre = genres[5]
                        }
                    ],
                    UserRatings =
                    [
                        new Rating
                        {
                            Stars = 4,
                            UserId = Guid.Parse("CEE8FD67-06C0-4AC9-85D6-69DA71122315")
                        },
                        new Rating
                        {
                            Stars = 8,
                            UserId = Guid.Parse("A09FEF96-285B-4CA3-9324-58BCD7B27FAC")
                        },
                        new Rating
                        {
                            Stars = 10,
                            UserId = Guid.Parse("F3E39F69-E828-4017-A753-8E029512A13D")
                        }
                    ]
                },
                new Show
                {
                    ShowId = Guid.Parse("CB70A8BA-F4B6-402B-B6D7-5CABD5651C88"),
                    ShowType = ShowType.TVSeries,
                    Title = "Test5",
                    ReleaseDate = new DateTime(1995, 2, 19),
                    PhotoId = "id",
                    Photo = new Photo
                    {
                        PhotoId = "id",
                        Url = "url"
                    },
                    Description = "Description5",
                    EndDate = new DateTime(2010, 11, 13),
                    Genres =
                    [
                        new ShowGenre
                        {
                            GenreId = 2,
                            Genre = genres[2]
                        },
                        new ShowGenre
                        {
                            GenreId = 6,
                            Genre = genres[6]
                        }
                    ],
                    UserRatings =
                    [
                        new Rating
                        {
                            Stars = 4,
                            UserId = Guid.Parse("CEE8FD67-06C0-4AC9-85D6-69DA71122315")
                        },
                        new Rating
                        {
                            Stars = 8,
                            UserId = Guid.Parse("A09FEF96-285B-4CA3-9324-58BCD7B27FAC")
                        },
                        new Rating
                        {
                            Stars = 10,
                            UserId = Guid.Parse("F3E39F69-E828-4017-A753-8E029512A13D")
                        }
                    ]
                }
            ];

            this.expectedShows = 
            [
                new AllShowsDto
                {
                    ShowId = "71E18E44-9E18-4D2C-ADF2-43EDC4136290",
                    ShowType = ShowType.Movie,
                    Title = "Test1",
                    Duration = 98,
                    ReleaseYear = 2020,
                    EndYear = null,
                    PhotoUrl = "url",
                    Description = "Description1",
                    Genres = [1, 2, 4],
                    NumberOfRatings = 2,
                    AverageRating = 6f,
                    MyRating = 5
                },
                new AllShowsDto
                {
                    ShowId = "21B9BA7B-3A98-4F21-9C08-AB49488DCE13",
                    ShowType = ShowType.Movie,
                    Title = "Test2",
                    Duration = 150,
                    ReleaseYear = 1978,
                    EndYear = null,
                    Description = "Description2",
                    PhotoUrl= "url",
                    Genres = [1, 3],
                    NumberOfRatings = 3,
                    AverageRating = 7.3f,
                    MyRating = 4
                },
                new AllShowsDto
                {
                    ShowId = "26381A34-FCDB-4018-8100-1D22AB7F4B42",
                    ShowType = ShowType.Movie,
                    Duration = 90,
                    Title = "Test3",
                    ReleaseYear = 2000,
                    EndYear = null,
                    PhotoUrl = "url",
                    Description = "Description3",
                    Genres = [1],
                    AverageRating = 7.3f,
                    NumberOfRatings = 3,
                    MyRating = 4
                },
                new AllShowsDto
                {
                    ShowId = "E0B1C3D5-A09E-4442-94CC-DA6C2E626686",
                    ShowType = ShowType.TVSeries,
                    Title = "Test4",
                    ReleaseYear = 1994,
                    PhotoUrl = "url",
                    Description = "Description4",
                    EndYear = 2004,
                    Genres = [3, 5],
                    Duration = null,
                    AverageRating = 7.3f,
                    NumberOfRatings = 3,
                    MyRating = 4
                },
                new AllShowsDto
                {
                    ShowId = "CB70A8BA-F4B6-402B-B6D7-5CABD5651C88",
                    ShowType = ShowType.TVSeries,
                    Title = "Test5",
                    ReleaseYear = 1995,
                    PhotoUrl = "url",
                    Description = "Description5",
                    EndYear = 2010,
                    Genres = [2, 6],
                    Duration = null,
                    AverageRating = 7.3f,
                    NumberOfRatings = 3,
                    MyRating = 4
                }
            ];

            SetUpAllShows(shows);
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenTheUserIdIsInvalid()
        {
            //Arrange
            AllShowsQuery query = new()
            {
                UserId = "userId1"
            };

            SetUpCheckingUser(false);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("The user is not found"));
            });
        }

        [Test]
        public async Task Handle_ShouldReturnAllShows_WithoutMyRating()
        {
            //Arrange
            SetUpMyRatingWithNull();
            AllShowsQuery query = new();

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!, Is.EqualTo(this.expectedShows).Using(new AllShowsDtoComparer()));
            });
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyCollection()
        {
            //Arrange
            AllShowsQuery query = new();

            IEnumerable<Show> shows = [];

            SetUpAllShows(shows.AsQueryable());

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.Count(), Is.EqualTo(0));
            });
        }

        [Test]
        public async Task Handle_ShouldReturnAllShows_WithMyRating()
        {
            //Arrange
            SetUpCheckingUser(true);

            AllShowsQuery query = new()
            {
                UserId = "CEE8FD67-06C0-4AC9-85D6-69DA71122315"
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!, Is.EqualTo(this.expectedShows).Using(new AllShowsDtoComparer()));
            });
        }

        [Test]
        public async Task Handle_ShouldReturnTheCollection_WhenStoredInTheCacheWithoutMyRating()
        {
            //Arrange
            SetUpMyRatingWithNull();
            AllShowsQuery query = new();

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            IEnumerable<AllShowsDto> shows = this.memoryCache.Get<IEnumerable<AllShowsDto>>("Shows")!;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!, Is.EqualTo(this.expectedShows).Using(new AllShowsDtoComparer()));
            });
        }

        [Test]
        public async Task Handle_ShouldReturnTheCollection_WhenStoredInTheCacheWithMyRating()
        {
            //Arrange
            SetUpCheckingUser(true);

            AllShowsQuery query = new()
            {
                UserId = "CEE8FD67-06C0-4AC9-85D6-69DA71122315"
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            IEnumerable<AllShowsDto> shows = this.memoryCache.Get<IEnumerable<AllShowsDto>>("Shows")!;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!, Is.EqualTo(this.expectedShows).Using(new AllShowsDtoComparer()));
            });
        }

        private void SetUpCheckingUser(bool value)
        {
            this.repositoryMock
                .Setup(r => r.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(value);
        }

        private void SetUpAllShows(IEnumerable<Show> collection)
        {
            IQueryable<Show> shows = collection.AsQueryable();

            TestAsyncEnumerableEfCore<Show> queryable = new(shows);

            this.repositoryMock
                .Setup(r => r.All<Show>())
                .Returns(queryable);
        }

        private void SetUpMyRatingWithNull()
        {
            this.expectedShows = this.expectedShows.Select(s =>
            {
                s.MyRating = null;

                return s;
            });
        }
    }
}