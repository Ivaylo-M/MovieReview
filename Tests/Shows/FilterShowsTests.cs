namespace Tests.Shows
{
    using Microsoft.Extensions.Caching.Memory;
    
    using Domain.Enums;
    using Application.Response;
    using Application.DTOs.Shows;
    
    using static Application.Shows.FilterShows;

    [TestFixture]
    public class FilterShowTests
    {
        private FilterShowsHandler handler;

        [SetUp]
        public void SetUp()
        {
            MemoryCacheOptions options = new MemoryCacheOptions();
            IMemoryCache memoryCache = new MemoryCache(options);

            IEnumerable<AllShowsDto> shows = new List<AllShowsDto>
            {
                new AllShowsDto
                {
                    ShowId = "71E18E44-9E18-4D2C-ADF2-43EDC4136290",
                    ShowType = ShowType.Movie,
                    Title = "Test1",
                    ReleaseYear = 2020,
                    PhotoUrl = "id",
                    Description = "Description1",
                    Genres = new List<int> {1, 2, 4},
                    EndYear = null,
                    AverageRating = 6f,
                    MyRating = 5
                },
                new AllShowsDto
                {
                    ShowId = "21B9BA7B-3A98-4F21-9C08-AB49488DCE13",
                    ShowType = ShowType.Movie,
                    Title = "Test2",
                    ReleaseYear = 1978,
                    Description = "Description2",
                    Genres = new List<int> { 1, 3 },
                    EndYear = null,
                    AverageRating = 0,
                    MyRating = null,
                    PhotoUrl = null
                },
                new AllShowsDto
                {
                    ShowId = "26381A34-FCDB-4018-8100-1D22AB7F4B42",
                    ShowType = ShowType.Movie,
                    Title = "Test3",
                    ReleaseYear = 2000,
                    Description = "Description3",
                    Genres = new List<int> { 1 },
                    PhotoUrl = null,
                    EndYear = null,
                    AverageRating = 0,
                    MyRating = null
                },
                new AllShowsDto
                {
                    ShowId = "E0B1C3D5-A09E-4442-94CC-DA6C2E626686",
                    ShowType = ShowType.TVSeries,
                    Title = "Test4",
                    ReleaseYear = 1994,
                    Description = "Description4",
                    EndYear = 2004,
                    Genres = new List<int> { 3, 5 },
                    PhotoUrl = null,
                    AverageRating = 0,
                    MyRating = null
                },
                new AllShowsDto
                {
                    ShowId = "CB70A8BA-F4B6-402B-B6D7-5CABD5651C88",
                    ShowType = ShowType.TVSeries,
                    Title = "Test5",
                    ReleaseYear = 1995,
                    Description = "Description5",
                    EndYear = 2010,
                    Genres = new List<int> { 2, 6 },
                    PhotoUrl = null,
                    AverageRating = 0,
                    MyRating = null
                }
            };

            memoryCache.Set("Shows", shows);

            this.handler = new FilterShowsHandler(memoryCache);
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithShowTypeMovie()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                ShowTypes = new List<ShowType> { ShowType.Movie }
            };

            //Act 
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(3));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test1"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test2"));
            Assert.That(result.Data!.Skip(2).First().Title, Is.EqualTo("Test3"));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithShowTypeTVSeries()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                ShowTypes = new List<ShowType> { ShowType.TVSeries }
            };

            //Act 
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(2));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test4"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test5"));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithShowTypesMovieAnTVSeries()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                ShowTypes = new List<ShowType> { ShowType.Movie, ShowType.TVSeries }
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
        public async Task Handle_ShouldReturnEmptyCollection_WithOneRecordWithOneGenre()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                Genres = new List<int> { 7 }
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_ShouldReturnAllShows_WithThreeRecordWithManyGenres()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                Genres = new List<int> { 1, 7 }
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(3));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test1"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test2"));
            Assert.That(result.Data!.Skip(2).First().Title, Is.EqualTo("Test3"));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithGenreComedy()
        {
            //Arrange   
            FilterShowsQuery query = new FilterShowsQuery
            {
                Genres = new List<int> { 1 }
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(3));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test1"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test2"));
            Assert.That(result.Data!.Skip(2).First().Title, Is.EqualTo("Test3"));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithGenresSciFiAndAction()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                Genres = new List<int> { 3, 5 }
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(2));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test2"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test4"));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithGenresRomanceComedyAndDrama()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                Genres = new List<int> { 1, 2, 4 }
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(4));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test1"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test2"));
            Assert.That(result.Data!.Skip(2).First().Title, Is.EqualTo("Test3"));
            Assert.That(result.Data!.Skip(3).First().Title, Is.EqualTo("Test5"));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMinReleaseDate1970()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                MinReleaseYear = 1970
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
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMinReleaseDate1995()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                MinReleaseYear = 1995
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(3));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test1"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test3"));
            Assert.That(result.Data!.Skip(2).First().Title, Is.EqualTo("Test5"));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyCollection_WithMinReleaseDate2030()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                MinReleaseYear = 2030
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMaxReleaseDate2025()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                MaxReleaseYear = 2025
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
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMaxReleaseDate1995()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                MaxReleaseYear = 1995
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(3));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test2"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test4"));
            Assert.That(result.Data!.Skip(2).First().Title, Is.EqualTo("Test5"));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyCollection_WithMaxReleaseDate1960()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                MaxReleaseYear = 1960
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMinReleaseDate1970AndMaxReleaseDate2025()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                MinReleaseYear = 1970,
                MaxReleaseYear = 2025
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
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMinReleaseDate2000AndMaxReleaseDate2020()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                MinReleaseYear = 2000,
                MaxReleaseYear = 2020
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(2));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test1"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test3"));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyCollection_WithMinReleaseDate2025AndMaxReleaseDate2029()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                MinReleaseYear = 2025,
                MaxReleaseYear = 2029
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyCollection_WithMinReleaseDate1920AndMaxReleaseDate1940()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                MinReleaseYear = 1920,
                MaxReleaseYear = 1940
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMultipleFilters()
        {
            //Arrange
            FilterShowsQuery query = new FilterShowsQuery
            {
                ShowTypes = new List<ShowType> { ShowType.TVSeries, ShowType.Movie },
                Genres = new List<int> { 3 },
                MinReleaseYear = 1970,
                MaxReleaseYear = 2025
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Count(), Is.EqualTo(2));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test2"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test4"));
        }
    }
}
