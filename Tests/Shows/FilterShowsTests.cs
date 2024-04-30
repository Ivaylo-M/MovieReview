namespace Tests.Shows
{
    using Microsoft.Extensions.Caching.Memory;
    
    using Domain.Enums;
    using Application.Response;
    using Application.DTOs.Shows;
    
    using static Application.Shows.FilterShows;
    using Tests.Comparers.Shows;
    using Azure.Core;

    [TestFixture]
    public class FilterShowTests
    {
        private FilterShowsHandler handler;
        private IEnumerable<AllShowsDto> shows;

        [SetUp]
        public void SetUp()
        {
            MemoryCacheOptions options = new();
            IMemoryCache memoryCache = new MemoryCache(options);

            this.shows =
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

            memoryCache.Set("Shows", this.shows);

            this.handler = new FilterShowsHandler(memoryCache);
        }

        //By Title
        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithTitleContainingTest() 
        {
            //Arrange
            string expectedTitle = "Test";
            FilterShowsQuery query = new()
            {
                Title = expectedTitle
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByTitle(expectedTitle);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data!, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyCollection_WithDifferentTitleName() 
        {
            //Arrange
            FilterShowsQuery query = new()
            {
                Title = "Past"
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data!.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_ShouldReturnCollectionWithOneRecord_WithTitleNameTest5()
        {
            //Arrange
            string expectedTitle = "Test5";
            FilterShowsQuery query = new()
            {
                Title = expectedTitle
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByTitle(expectedTitle);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnCollectionWithOneRecord_WithTitleNameTEST5()
        {
            //Arrange
            string expectedTitle = "TEST5";
            FilterShowsQuery query = new()
            {
                Title = expectedTitle
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByTitle(expectedTitle);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnCollectionWithOneRecord_WithTitleNametest5()
        {
            //Arrange
            string expectedTitle = "test5";
            FilterShowsQuery query = new()
            {
                Title = expectedTitle
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByTitle(expectedTitle);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        //By ShowType
        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithShowTypeMovie()
        {
            //Arrange
            IEnumerable<ShowType> expectedShowTypes = [ShowType.Movie];
            FilterShowsQuery query = new()
            {
                ShowTypes = expectedShowTypes
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByShowTypes(expectedShowTypes);

            //Act 
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithShowTypeTVSeries()
        {
            //Arrange
            IEnumerable<ShowType> expectedShowTypes = [ShowType.TVSeries];
            FilterShowsQuery query = new()
            {
                ShowTypes = expectedShowTypes
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByShowTypes(expectedShowTypes);

            //Act 
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithShowTypesMovieAnTVSeries()
        {
            //Arrange
            IEnumerable<ShowType> expectedShowTypes = [ShowType.Movie, ShowType.TVSeries];
            FilterShowsQuery query = new()
            {
                ShowTypes = expectedShowTypes
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByShowTypes(expectedShowTypes);

            //Act 
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyCollection_WithOneRecordWithOneGenre()
        {
            //Arrange
            FilterShowsQuery query = new()
            {
                Genres = [7]
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data!.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_ShouldReturnAllShows_WithThreeRecordWithManyGenres()
        {
            //Arrange
            IEnumerable<int> expectedGenres = [1, 7];
            FilterShowsQuery query = new()
            {
                Genres = expectedGenres
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByGenres(expectedGenres);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithGenreComedy()
        {
            //Arrange
            IEnumerable<int> expectedGenres = [1];
            FilterShowsQuery query = new()
            {
                Genres = expectedGenres
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByGenres(expectedGenres);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithGenresSciFiAndAction()
        {
            //Arrange
            IEnumerable<int> expectedGenres = [3, 5];
            FilterShowsQuery query = new()
            {
                Genres = expectedGenres
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByGenres(expectedGenres);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithGenresRomanceComedyAndDrama()
        {
            //Arrange
            IEnumerable<int> expectedGenres = [1, 2, 4];
            FilterShowsQuery query = new()
            {
                Genres = expectedGenres
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByGenres(expectedGenres);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        //MinReleaseYear
        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMinReleaseDate1970()
        {
            //Arrange
            int expectedMinReleaseYear = 1970;
            FilterShowsQuery query = new()
            {
                MinReleaseYear = expectedMinReleaseYear
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByMinReleaseYear(expectedMinReleaseYear);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMinReleaseDate1995()
        {
            //Arrange
            int expectedMinReleaseYear = 1995;
            FilterShowsQuery query = new()
            {
                MinReleaseYear = expectedMinReleaseYear
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByMinReleaseYear(expectedMinReleaseYear);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyCollection_WithMinReleaseDate2030()
        {
            //Arrange
            FilterShowsQuery query = new()
            {
                MinReleaseYear = 2030
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data!.Count(), Is.EqualTo(0));
        }

        //MaxReleaseYear
        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMaxReleaseDate2025()
        {
            //Arrange
            int expectedMaxReleaseYear = 2025;
            FilterShowsQuery query = new()
            {
                MaxReleaseYear = expectedMaxReleaseYear
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByMaxReleaseYear(expectedMaxReleaseYear);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMaxReleaseDate1995()
        {
            //Arrange
            int expectedMaxReleaseYear = 1995;
            FilterShowsQuery query = new()
            {
                MaxReleaseYear = expectedMaxReleaseYear
            };

            IEnumerable<AllShowsDto> expectedShows = GetShowsByMaxReleaseYear(expectedMaxReleaseYear);

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data, Is.EqualTo(expectedShows).Using(new AllShowsDtoComparer()));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyCollection_WithMaxReleaseDate1960()
        {
            //Arrange
            FilterShowsQuery query = new()
            {
                MaxReleaseYear = 1960
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data!.Count(), Is.EqualTo(0));
        }

        //MinReleaseYear & MaxReleaseYear
        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMinReleaseDate1970AndMaxReleaseDate2025()
        {
            //Arrange
            FilterShowsQuery query = new()
            {
                MinReleaseYear = 1970,
                MaxReleaseYear = 2025
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
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
            FilterShowsQuery query = new()
            {
                MinReleaseYear = 2000,
                MaxReleaseYear = 2020
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data!.Count(), Is.EqualTo(2));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test1"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test3"));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyCollection_WithMinReleaseDate2025AndMaxReleaseDate2029()
        {
            //Arrange
            FilterShowsQuery query = new()
            {
                MinReleaseYear = 2025,
                MaxReleaseYear = 2029
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data!.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_ShouldReturnEmptyCollection_WithMinReleaseDate1920AndMaxReleaseDate1940()
        {
            //Arrange
            FilterShowsQuery query = new()
            {
                MinReleaseYear = 1920,
                MaxReleaseYear = 1940
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data!.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithMultipleFilters()
        {
            //Arrange
            FilterShowsQuery query = new()
            {
                ShowTypes = [ShowType.TVSeries, ShowType.Movie],
                Genres = [3],
                MinReleaseYear = 1970,
                MaxReleaseYear = 2025
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Data!.Count(), Is.EqualTo(2));
            Assert.That(result.Data!.First().Title, Is.EqualTo("Test2"));
            Assert.That(result.Data!.Skip(1).First().Title, Is.EqualTo("Test4"));
        }

        private IEnumerable<AllShowsDto> GetShowsByTitle(string title)
        {
            return this.shows.Where(s => s.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        private IEnumerable<AllShowsDto> GetShowsByShowTypes(IEnumerable<ShowType> showTypes)
        {
            return this.shows.Where(s => showTypes.Contains(s.ShowType));
        }

        private IEnumerable<AllShowsDto> GetShowsByGenres(IEnumerable<int> genres)
        {
            return this.shows.Where(s => s.Genres.Any(g => genres.Contains(g)));
        }

        private IEnumerable<AllShowsDto> GetShowsByMinReleaseYear(int minReleaseYear)
        {
            return this.shows.Where(s => s.ReleaseYear >= minReleaseYear);
        }

        private IEnumerable<AllShowsDto> GetShowsByMaxReleaseYear(int maxReleaseYear)
        {
            return this.shows.Where(s => s.ReleaseYear <= maxReleaseYear);
        }
    }
}
