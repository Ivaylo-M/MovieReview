namespace Tests.Shows
{
    using Application.DTOs.Shows;
    using Application.Response;
    using Domain.Enums;
    using Microsoft.Extensions.Caching.Memory;
    using static Application.Shows.FilterShows;

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

        [Test]
        //Title
        [TestCase("Test", null, null, null, null, new[] { "Test1", "Test2", "Test3", "Test4", "Test5" })]
        [TestCase("Test5", null, null, null, null, new[] { "Test5" })]
        [TestCase("TEST5", null, null, null, null, new[] { "Test5" })]
        [TestCase("test5", null, null, null, null, new[] { "Test5" })]
        [TestCase("Past", null, null, null, null, new string[0])]

        //ShowTypes
        [TestCase(null, new[] { ShowType.Movie }, null, null, null, new[] { "Test1", "Test2", "Test3" })]
        [TestCase(null, new[] { ShowType.TVSeries }, null, null, null, new[] { "Test4", "Test5" })]
        [TestCase(null, new[] { ShowType.Movie, ShowType.TVSeries }, null, null, null, new[] { "Test1", "Test2", "Test3", "Test4", "Test5" })]

        //Genres
        [TestCase(null, null, new[] { 7 }, null, null, new string[0])]
        [TestCase(null, null, new[] { 1, 7 }, null, null, new[] { "Test1", "Test2", "Test3" })]
        [TestCase(null, null, new[] { 1 }, null, null, new[] { "Test1", "Test2", "Test3" })]
        [TestCase(null, null, new[] { 3, 5 }, null, null, new[] { "Test2", "Test4" })]
        [TestCase(null, null, new[] { 1, 2, 4 }, null, null, new[] { "Test1", "Test2", "Test3", "Test5" })]

        //MinReleaseYear
        [TestCase(null, null, null, 1970, null, new[] { "Test1", "Test2", "Test3", "Test4", "Test5" })]
        [TestCase(null, null, null, 1995, null, new[] { "Test1", "Test3", "Test5" })]
        [TestCase(null, null, null, 2030, null, new string[0])]

        //MaxReleaseYear
        [TestCase(null, null, null, null, 2025, new[] { "Test1", "Test2", "Test3", "Test4", "Test5" })]
        [TestCase(null, null, null, null, 1995, new[] { "Test2", "Test4", "Test5" })]
        [TestCase(null, null, null, null, 1960, new string[0])]

        //MinReleaseYear, MaxReleaseYear
        [TestCase(null, null, null, 1970, 2025, new[] { "Test1", "Test2", "Test3", "Test4", "Test5" })]
        [TestCase(null, null, null, 2000, 2020, new[] { "Test1", "Test3" })]
        [TestCase(null, null, null, 2025, 2029, new string[0])]
        [TestCase(null, null, null, 1920, 1940, new string[0])]

        //Title, ShowTypes, Genres, MinReleaseYear, MaxReleaseYear
        [TestCase(null, new[] { ShowType.Movie, ShowType.TVSeries }, new[] { 3 }, 1970, 2025, new[] { "Test2", "Test4" } )]
        [TestCase("Test2", new[] { ShowType.Movie, ShowType.TVSeries }, new[] { 3 }, 1970, 2025, new[] { "Test2" } )]
        public async Task Handle_ShouldReturnAllFilterShowsQuery_WithExpectedFilters
            (string? expectedTitle, ShowType[]? expectedShowTypes, int[]? expectedGenres, int? expectedMinReleaseYear, int? expectedMaxReleaseYear, string[] expectedTitles)
        {
            //Arrange
            FilterShowsQuery query = new()
            {
                Title = expectedTitle,
                ShowTypes = expectedShowTypes,
                Genres = expectedGenres,
                MinReleaseYear = expectedMinReleaseYear,
                MaxReleaseYear = expectedMaxReleaseYear
            };

            //Act
            Result<IEnumerable<AllShowsDto>> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.Select(s => s.Title), Is.EqualTo(expectedTitles));
            });
        }
    }
}