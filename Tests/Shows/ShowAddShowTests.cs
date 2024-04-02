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
    using static Application.Shows.ShowAddShow;

    public class ShowAddOrEditShowTests
    {
        private Mock<IRepository> repositoryMock;
        private ShowAddShowHandler handler;
        private IEnumerable<Genre> genres;
        private IEnumerable<FilmingLocation> filmingLocations;
        private IEnumerable<Language> languages;
        private IEnumerable<CountryOfOrigin> countriesOfOrigin;
        private ShowAddShowQuery movieQuery;
        private ShowAddShowQuery tvSeriesQuery;
        private ShowAddShowQuery episodeQuery;
        private Show tvSeries;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IRepository>();
            this.handler = new ShowAddShowHandler(this.repositoryMock.Object);

            this.genres = new List<Genre>
            {
                new Genre
                {
                    GenreId = 1,
                    Name = "Action"
                },
                new Genre
                {
                    GenreId = 2,
                    Name = "Romance"
                },
                new Genre
                {
                    GenreId = 3,
                    Name = "Comedy"
                }
            };
            this.filmingLocations = new List<FilmingLocation>
            {
                new FilmingLocation
                {
                    FilmingLocationId = 1,
                    Name = "Singapore"
                },
                new FilmingLocation
                {
                    FilmingLocationId = 2,
                    Name = "USA"
                }
            };
            this.languages = new List<Language>
            {
                new Language
                {
                    LanguageId = 1,
                    Name = "English"
                },
                new Language
                {
                    LanguageId = 2,
                    Name = "Chinese"
                },
                new Language
                {
                    LanguageId = 3,
                    Name = "French"
                },
                new Language
                {
                    LanguageId = 4,
                    Name = "Spanish"
                }
            };
            this.countriesOfOrigin = new List<CountryOfOrigin>
            {
                new CountryOfOrigin
                {
                    CountryOfOriginId = 1,
                    Name = "USA"
                },
                new CountryOfOrigin
                {
                    CountryOfOriginId = 2,
                    Name = "France"
                }
            };

            this.movieQuery = new ShowAddShowQuery
            {
                ShowType = ShowType.Movie
            };

            this.tvSeriesQuery = new ShowAddShowQuery
            {
                ShowType = ShowType.TVSeries
            };

            this.episodeQuery = new ShowAddShowQuery
            {
                ShowType = ShowType.Episode,
                TVSeriesId = "b9099dc4-ff09-4b49-86b8-ac23572486fb"
            };

            this.tvSeries = new Show
            {
                ShowId = Guid.Parse("b9099dc4-ff09-4b49-86b8-ac23572486fb"),
                Title = "TV Series Title"
            };
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfTvSeriesDoesNotExist()
        {
            //Arrange
            SetUpReturningTvShow(null!);

            //Act
            Result<ShowAddShowDto> result = await this.handler.Handle(this.episodeQuery, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("The show is not found"));
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfShowTypeIsInvalid()
        {
            //Arrange
            ShowAddShowQuery query = new ShowAddShowQuery
            {
                ShowType = 0
            };

            //Act
            Result<ShowAddShowDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("The show type is invalid"));
        }

        //Movie
        [Test]
        public async Task Handle_ShouldReturnDto_ForShowingAddShowPageForMovie()
        {
            //Arrange
            SetUpReturningCollection(this.genres);
            SetUpReturningCollection(this.filmingLocations);
            SetUpReturningCollection(this.languages);
            SetUpReturningCollection(this.countriesOfOrigin);

            //Act
            Result<ShowAddShowDto> result = await this.handler.Handle(this.movieQuery, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.ShowType.Id, Is.EqualTo(1));
            Assert.That(result.Data!.ShowType.Name, Is.EqualTo("Movie"));

            Assert.That(result.Data!.Series, Is.EqualTo(null));

            CollectionAssert.AreEqual(result.Data!.Genres!.Select(g => g.Name).ToList(), this.genres.Select(g => g.Name).ToList());
            CollectionAssert.AreEqual(result.Data!.Genres!.Select(g => g.GenreId).ToList(), this.genres.Select(g => g.GenreId).ToList());

            CollectionAssert.AreEqual(result.Data!.FilmingLocations!.Select(fl => fl.Name).ToList(), this.filmingLocations.Select(fl => fl.Name).ToList());
            CollectionAssert.AreEqual(result.Data!.FilmingLocations!.Select(fl => fl.FilmingLocationId).ToList(), this.filmingLocations.Select(fl => fl.FilmingLocationId).ToList());

            CollectionAssert.AreEqual(result.Data!.CountriesOfOrigin!.Select(coo => coo.Name).ToList(), this.countriesOfOrigin.Select(coo => coo.Name).ToList());
            CollectionAssert.AreEqual(result.Data!.CountriesOfOrigin!.Select(coo => coo.CountryOfOriginId).ToList(), this.countriesOfOrigin.Select(coo => coo.CountryOfOriginId).ToList());

            CollectionAssert.AreEqual(result.Data!.Languages!.Select(l => l.Name).ToList(), this.languages.Select(l => l.Name).ToList());
            CollectionAssert.AreEqual(result.Data!.Languages!.Select(l => l.LanguageId).ToList(), this.languages.Select(l => l.LanguageId).ToList());
        }

        //TV Series
        [Test]
        public async Task Handle_ShouldReturnDto_ForShowingAddShowPageForTVSeries()
        {
            //Arrange
            SetUpReturningCollection(this.genres);
            SetUpReturningCollection(this.filmingLocations);
            SetUpReturningCollection(this.languages);
            SetUpReturningCollection(this.countriesOfOrigin);

            //Act
            Result<ShowAddShowDto> result = await this.handler.Handle(this.tvSeriesQuery, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.ShowType.Id, Is.EqualTo(2));
            Assert.That(result.Data!.ShowType.Name, Is.EqualTo("TVSeries"));

            Assert.That(result.Data!.Series, Is.EqualTo(null));

            CollectionAssert.AreEqual(result.Data!.Genres!.Select(g => g.Name).ToList(), this.genres.Select(g => g.Name).ToList());
            CollectionAssert.AreEqual(result.Data!.Genres!.Select(g => g.GenreId).ToList(), this.genres.Select(g => g.GenreId).ToList());

            CollectionAssert.AreEqual(result.Data!.FilmingLocations!.Select(fl => fl.Name).ToList(), this.filmingLocations.Select(fl => fl.Name).ToList());
            CollectionAssert.AreEqual(result.Data!.FilmingLocations!.Select(fl => fl.FilmingLocationId).ToList(), this.filmingLocations.Select(fl => fl.FilmingLocationId).ToList());

            CollectionAssert.AreEqual(result.Data!.CountriesOfOrigin!.Select(coo => coo.Name).ToList(), this.countriesOfOrigin.Select(coo => coo.Name).ToList());
            CollectionAssert.AreEqual(result.Data!.CountriesOfOrigin!.Select(coo => coo.CountryOfOriginId).ToList(), this.countriesOfOrigin.Select(coo => coo.CountryOfOriginId).ToList());

            CollectionAssert.AreEqual(result.Data!.Languages!.Select(l => l.Name).ToList(), this.languages.Select(l => l.Name).ToList());
            CollectionAssert.AreEqual(result.Data!.Languages!.Select(l => l.LanguageId).ToList(), this.languages.Select(l => l.LanguageId).ToList());
        }

        //Episode
        [Test]
        public async Task Handle_ShouldReturnDto_ForShowingAddShowPageForEpisode()
        {
            //Arrange
            SetUpReturningTvShow(this.tvSeries);

            //Act
            Result<ShowAddShowDto> result = await this.handler.Handle(this.episodeQuery, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.ShowType.Id, Is.EqualTo(3));
            Assert.That(result.Data!.ShowType.Name, Is.EqualTo("Episode"));

            Assert.That(result.Data!.Series!.Id, Is.EqualTo("b9099dc4-ff09-4b49-86b8-ac23572486fb"));
            Assert.That(result.Data!.Series!.Title, Is.EqualTo("TV Series Title"));
        }

        private void SetUpReturningCollection<T>(IEnumerable<T> collection) where T : class 
        {
            TestAsyncEnumerableEfCore<T> queryable = new TestAsyncEnumerableEfCore<T>(collection.AsQueryable());

            this.repositoryMock
                .Setup(r => r.All<T>())
                .Returns(queryable);
        }

        private void SetUpReturningTvShow(Show? show)
        {
            this.repositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Show, bool>>>()))
                .ReturnsAsync(show);
        }
    }
}
