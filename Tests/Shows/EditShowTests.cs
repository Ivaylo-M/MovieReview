namespace Tests.Shows
{
    using Application.DTOs.Shows;
    using Application.Response;
    using Domain;
    using Domain.Enums;
    using MediatR;
    using MockQueryable.EntityFrameworkCore;
    using Moq;
    using Persistence.Repositories;
    using System.Linq.Expressions;
    using static Application.Shows.EditShow;

    public class EditShowTests
    {
        private Mock<IRepository> repositoryMock;
        private EditShowHandler handler;
        private Show movie;
        private Show tvSeries;
        private Show episode;
        private EditShowCommand movieCommand;
        private EditShowCommand tvSeriesCommand;
        private EditShowCommand episodeCommand;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IRepository>();
            this.handler = new EditShowHandler(this.repositoryMock.Object);

            this.movie = new Show
            {
                ShowId = Guid.Parse("CD9B2F47-67D3-48C0-9E45-F55476F19ADB"),
                Title = "Movie Title",
                Description = "Movie Description",
                Duration = 123,
                ReleaseDate = new DateTime(2022, 3, 4),
                ShowType = ShowType.Movie,
                PhotoId = "photoId",
                Genres = new List<ShowGenre>
                {
                    new ShowGenre
                    {
                        GenreId = 2
                    },
                    new ShowGenre
                    {
                        GenreId = 4
                    }
                },
                FilmingLocations = new List<ShowFilmingLocation>
                {
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 1
                    },
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 3
                    },
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 4
                    }
                },
                Languages = new List<ShowLanguage>
                {
                    new ShowLanguage
                    {
                        LanguageId = 1
                    },
                    new ShowLanguage
                    {
                        LanguageId = 3
                    }
                },
                CountriesOfOrigin = new List<ShowCountryOfOrigin>
                {
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 1
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 2
                    }
                }
            };

            this.tvSeries = new Show
            {
                ShowId = Guid.Parse("CD9B2F47-67D3-48C0-9E45-F55476F19ADB"),
                Title = "TV Series Title",
                Description = "TV Series Description",
                ReleaseDate = new DateTime(2020, 4, 5),
                EndDate = new DateTime(2022, 3, 4),
                ShowType = ShowType.TVSeries,
                PhotoId = "photoId",
                Genres = new List<ShowGenre>
                {
                    new ShowGenre
                    {
                        GenreId = 2
                    },
                    new ShowGenre
                    {
                        GenreId = 4
                    }
                },
                FilmingLocations = new List<ShowFilmingLocation>
                {
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 1
                    },
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 3
                    },
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 4
                    }
                },
                Languages = new List<ShowLanguage>
                {
                    new ShowLanguage
                    {
                        LanguageId = 1
                    },
                    new ShowLanguage
                    {
                        LanguageId = 3
                    }
                },
                CountriesOfOrigin = new List<ShowCountryOfOrigin>
                {
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 1
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 2
                    }
                }
            };

            this.episode = new Show
            {
                ShowId = Guid.Parse("5AE0C243-971A-4C51-9710-A87E1A45F4F0"),
                ShowType = ShowType.Episode,
                Title = "Episode Title",
                Description = "Episode Description",
                ReleaseDate = new DateTime(2021, 3, 5),
                Season = 2,
                SeriesId = Guid.Parse("CD9B2F47-67D3-48C0-9E45-F55476F19ADB"),
                Duration = 23,
                PhotoId = "photoId"
            };

            this.movieCommand = new EditShowCommand
            {
                ShowId = "CD9B2F47-67D3-48C0-9E45-F55476F19ADB",
                Dto = new EditShowDto
                {
                    ShowType = ShowType.Movie,
                    Title = "Movie Title",
                    Description = "Movie Description",
                    ReleaseDate = new DateTime(2022, 3, 4),
                    Duration = 123,
                    Genres = new List<int> { 2, 4 },
                    FilmingLocations = new List<int> { 1, 3, 4 },
                    Languages = new List<int> { 1, 3 },
                    CountriesOfOrigin = new List<int> { 1, 2 }
                }
            };

            this.tvSeriesCommand = new EditShowCommand
            {
                ShowId = "CD9B2F47-67D3-48C0-9E45-F55476F19ADB",
                Dto = new EditShowDto
                {
                    ShowType = ShowType.TVSeries,
                    Title = "TV Series Title",
                    Description = "TV Series Description",
                    ReleaseDate = new DateTime(2020, 4, 5),
                    EndDate = new DateTime(2022, 3, 4),
                    Genres = new List<int> { 2, 4 },
                    FilmingLocations = new List<int> { 1, 3, 4 },
                    Languages = new List<int> { 1, 3 },
                    CountriesOfOrigin = new List<int> { 1, 2 }
                }
            };

            this.episodeCommand = new EditShowCommand
            {
                ShowId = "5AE0C243-971A-4C51-9710-A87E1A45F4F0",
                Dto = new EditShowDto
                {
                    ShowType = ShowType.Episode,
                    Title = "Episode Title",
                    Description = "Episode Description",
                    ReleaseDate = new DateTime(2021, 3, 5),
                    Duration = 23,
                    Season = 2,
                    SeriesId = "CD9B2F47-67D3-48C0-9E45-F55476F19ADB"
                }
            };
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfTheShowDoesNotExist()
        {
            //Arrange
            EditShowCommand command = new EditShowCommand();

            SetUpReturningShow(null);

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This show does not exist! Please select an existing one"));
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfEditFails()
        {
            //Arrange
            SetUpReturningShow(this.movie);
            SetUpSaveChanges();

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            //Assert
            Assert.False(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Failed to edit show - Movie Title"));
        }

        //Movie
        [Test]
        public async Task Handle_ShouldNotEditMovie_IfDataIsTheSame()
        {
            //Arrange
            SetUpReturningShow(this.movie);
            IEnumerable<int> expectedGenres = new List<int> { 2, 4 };
            IEnumerable<int> expectedFilmingLocations = new List<int> { 1, 3, 4 };
            IEnumerable<int> expectedLanguages = new List<int> { 1, 3 };
            IEnumerable<int> expectedCountriesOfOrigin = new List<int> { 1, 2 };

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.Title, Is.EqualTo("Movie Title"));
            Assert.That(this.movie.Description, Is.EqualTo("Movie Description"));
            Assert.That(this.movie.ReleaseDate, Is.EqualTo(new DateTime(2022, 3, 4)));
            Assert.That(this.movie.Duration, Is.EqualTo(123));
            Assert.That(this.movie.ShowType, Is.EqualTo(ShowType.Movie));
            Assert.That(this.movie.PhotoId, Is.EqualTo("photoId"));
            Assert.That(this.movie.EndDate, Is.EqualTo(null));
            Assert.That(this.movie.Season, Is.EqualTo(null));
            Assert.That(this.movie.SeriesId, Is.EqualTo(null));
            CollectionAssert.AreEqual(expectedGenres, this.movie.Genres.Select(sg => sg.GenreId));
            CollectionAssert.AreEqual(expectedFilmingLocations, this.movie.FilmingLocations.Select(sfl => sfl.FilmingLocationId));
            CollectionAssert.AreEqual(expectedLanguages, this.movie.Languages.Select(sl => sl.LanguageId));
            CollectionAssert.AreEqual(expectedCountriesOfOrigin, this.movie.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfTheTitleIsDifferent()
        {
            //Arrange
            string expectedShowTitle = "Movie Title1"; 
            this.movieCommand.Dto.Title = expectedShowTitle;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title1"));
            Assert.That(this.movie.Title, Is.EqualTo(expectedShowTitle));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfTheDescriptionIsDifferent()
        {
            //Arrange
            string expectedShowDescription = "Movie Description1";
            this.movieCommand.Dto.Description = expectedShowDescription;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.Description, Is.EqualTo(expectedShowDescription));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfReleaseDateIsDifferent()
        {
            //Arrange
            DateTime expectedShowReleaseDate = new DateTime(2022, 4, 5);
            this.movieCommand.Dto.ReleaseDate = expectedShowReleaseDate;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.ReleaseDate, Is.EqualTo(expectedShowReleaseDate));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfDurationIsDifferent()
        {
            //Arrange
            int expectedDuration = 100;
            this.movieCommand.Dto.Duration = expectedDuration;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.Duration, Is.EqualTo(expectedDuration));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfTheGenresAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedGenres = new List<int> { 1, 3 };
            this.movieCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
        }

        [Test]
        public async Task Handle_ShoudlEditMovie_IfSomeOfTheGenresAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedGenres = new List<int> { 2, 3 };
            this.movieCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfAddedMoreGenresThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedGenres = new List<int> { 2, 3, 4 };
            this.movieCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfGenresAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedGenres = new List<int> { 2 };
            this.movieCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfTheFilmingLocationsAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = new List<int> { 5, 6, 7 };
            this.movieCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
        }

        [Test]
        public async Task Handle_ShoudlEditMovie_IfSomeOfTheFilmingLocationsAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = new List<int> { 1, 3, 5 };
            this.movieCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfAddedMoreFilmingLocationsThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = new List<int> { 1, 4, 5, 6 };
            this.movieCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfFilmingLocationsAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = new List<int> { 5 };
            this.movieCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfTheLanguagesAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = new List<int> { 2, 4 };
            this.movieCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
        }

        [Test]
        public async Task Handle_ShoudlEditMovie_IfSomeOfTheLanguagesAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = new List<int> { 1, 2 };
            this.movieCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfAddedMoreLanguagesThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = new List<int> { 4, 5, 6 };
            this.movieCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfLanguagesAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = new List<int> { 2 };
            this.movieCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfTheCountriesOfOriginAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = new List<int> { 3, 4 };
            this.movieCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
        }

        [Test]
        public async Task Handle_ShoudlEditMovie_IfSomeOfTheCountriesOfOriginAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = new List<int> { 1, 3 };
            this.movieCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfAddedMoreCountriesOfOriginThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = new List<int> { 3, 4, 5 };
            this.movieCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfCountriesOfOriginAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = new List<int> { 6 };
            this.movieCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
            Assert.That(this.movie.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
        }

        //TV Series
        [Test]
        public async Task Handle_ShouldNotEditTVSeries_IfDataIsTheSame()
        {
            //Arrange
            SetUpReturningShow(this.tvSeries);
            IEnumerable<int> expectedGenres = new List<int> { 2, 4 };
            IEnumerable<int> expectedFilmingLocations = new List<int> { 1, 3, 4 };
            IEnumerable<int> expectedLanguages = new List<int> { 1, 3 };
            IEnumerable<int> expectedCountriesOfOrigin = new List<int> { 1, 2 };

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.Title, Is.EqualTo("TV Series Title"));
            Assert.That(this.tvSeries.Description, Is.EqualTo("TV Series Description"));
            Assert.That(this.tvSeries.ReleaseDate, Is.EqualTo(new DateTime(2020, 4, 5)));
            Assert.That(this.tvSeries.EndDate, Is.EqualTo(new DateTime(2022, 3, 4)));
            Assert.That(this.tvSeries.ShowType, Is.EqualTo(ShowType.TVSeries));
            Assert.That(this.tvSeries.SeriesId, Is.EqualTo(null));
            Assert.That(this.tvSeries.Season, Is.EqualTo(null));
            Assert.That(this.tvSeries.Duration, Is.EqualTo(null));
            CollectionAssert.AreEqual(expectedGenres, this.tvSeries.Genres.Select(sg => sg.GenreId));
            CollectionAssert.AreEqual(expectedFilmingLocations, this.tvSeries.FilmingLocations.Select(sfl => sfl.FilmingLocationId));
            CollectionAssert.AreEqual(expectedLanguages, this.tvSeries.Languages.Select(sl => sl.LanguageId));
            CollectionAssert.AreEqual(expectedCountriesOfOrigin, this.tvSeries.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfTheTitleIsDifferent()
        {
            //Arrange
            string expectedShowTitle = "TV Series Title1";
            this.tvSeriesCommand.Dto.Title = expectedShowTitle;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title1"));
            Assert.That(this.tvSeries.Title, Is.EqualTo(expectedShowTitle));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfTheDescriptionIsDifferent()
        {
            //Arrange
            string expectedShowDescription = "TV Series Description1";
            this.tvSeriesCommand.Dto.Description = expectedShowDescription;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.Description, Is.EqualTo(expectedShowDescription));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfReleaseDateIsDifferent()
        {
            //Arrange
            DateTime expectedShowReleaseDate = new DateTime(2020, 4, 5);
            this.tvSeriesCommand.Dto.ReleaseDate = expectedShowReleaseDate;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.ReleaseDate, Is.EqualTo(expectedShowReleaseDate));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfEndDateIsDifferent()
        {
            //Arrange
            DateTime expectedEndDate = new DateTime(2023, 5, 6);
            this.tvSeriesCommand.Dto.EndDate = expectedEndDate;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.EndDate, Is.EqualTo(expectedEndDate));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfTheGenresAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedGenres = new List<int> { 1, 3 };
            this.tvSeriesCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
        }

        [Test]
        public async Task Handle_ShoudlEditTVSeries_IfSomeOfTheGenresAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedGenres = new List<int> { 2, 3 };
            this.tvSeriesCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfAddedMoreGenresThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedGenres = new List<int> { 2, 3, 4 };
            this.tvSeriesCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfGenresAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedGenres = new List<int> { 2 };
            this.tvSeriesCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfTheFilmingLocationsAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = new List<int> { 5, 6, 7 };
            this.tvSeriesCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
        }

        [Test]
        public async Task Handle_ShoudlEditTVSeries_IfSomeOfTheFilmingLocationsAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = new List<int> { 1, 3, 5 };
            this.tvSeriesCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfAddedMoreFilmingLocationsThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = new List<int> { 1, 4, 5, 6 };
            this.tvSeriesCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfFilmingLocationsAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = new List<int> { 5 };
            this.tvSeriesCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfTheLanguagesAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = new List<int> { 2, 4 };
            this.tvSeriesCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
        }

        [Test]
        public async Task Handle_ShoudlEditTVSeries_IfSomeOfTheLanguagesAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = new List<int> { 1, 2 };
            this.tvSeriesCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfAddedMoreLanguagesThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = new List<int> { 4, 5, 6 };
            this.tvSeriesCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfLanguagesAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = new List<int> { 2 };
            this.tvSeriesCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfTheCountriesOfOriginAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = new List<int> { 3, 4 };
            this.tvSeriesCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
        }

        [Test]
        public async Task Handle_ShoudlEditTVSeries_IfSomeOfTheCountriesOfOriginAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = new List<int> { 1, 3 };
            this.tvSeriesCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfAddedMoreCountriesOfOriginThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = new List<int> { 3, 4, 5 };
            this.tvSeriesCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfCountriesOfOriginAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = new List<int> { 6 };
            this.tvSeriesCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
            Assert.That(this.tvSeries.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
        }

        //Episode
        [Test]
        public async Task Handle_ShouldNotEditEpisode_IfDataIsTheSame()
        {
            //Arrange
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
            Assert.That(this.episode.Title, Is.EqualTo("Episode Title"));
            Assert.That(this.episode.Description, Is.EqualTo("Episode Description"));
            Assert.That(this.episode.ShowType, Is.EqualTo(ShowType.Episode));
            Assert.That(this.episode.ReleaseDate, Is.EqualTo(new DateTime(2021, 3, 5)));
            Assert.That(this.episode.EndDate, Is.EqualTo(null));
            Assert.That(this.episode.PhotoId, Is.EqualTo("photoId"));
            Assert.That(this.episode.Season, Is.EqualTo(2));
            Assert.That(this.episode.SeriesId.ToString(), Is.EqualTo("CD9B2F47-67D3-48C0-9E45-F55476F19ADB".ToLower()));
            Assert.That(this.episode.Genres, Is.EqualTo(new List<int>()));
            Assert.That(this.episode.FilmingLocations, Is.EqualTo(new List<int>()));
            Assert.That(this.episode.Languages, Is.EqualTo(new List<int>()));
            Assert.That(this.episode.CountriesOfOrigin, Is.EqualTo(new List<int>()));
        }

        [Test]
        public async Task Handle_ShouldEditEpisode_IfTheTitleIsDifferent()
        {
            //Arrange
            string expectedShowTitle = "Episode Title1";
            this.episodeCommand.Dto.Title = expectedShowTitle;
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title1"));
            Assert.That(this.episode.Title, Is.EqualTo(expectedShowTitle));
        }

        [Test]
        public async Task Handle_ShouldEditEpisode_IfTheDescriptionIsDifferent()
        {
            //Arrange
            string expectedShowDescription = "Episode Description1";
            this.episodeCommand.Dto.Description = expectedShowDescription;
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
            Assert.That(this.episode.Description, Is.EqualTo(expectedShowDescription));
        }

        [Test]
        public async Task Handle_ShouldEditEpisode_IfReleaseDateIsDifferent()
        {
            //Arrange
            DateTime expectedShowReleaseDate = new DateTime(2022, 4, 5);
            this.episodeCommand.Dto.ReleaseDate = expectedShowReleaseDate;
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
            Assert.That(this.episode.ReleaseDate, Is.EqualTo(expectedShowReleaseDate));
        }

        [Test]
        public async Task Handle_ShouldEditEpisode_IfSeasonIsDifferent()
        {
            //Arrange
            int expectedSeason = 1;
            this.episodeCommand.Dto.Season = expectedSeason;
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
            Assert.That(this.episode.Season, Is.EqualTo(expectedSeason));
        }

        [Test]
        public async Task Handle_ShouldNotEditEpisode_IfGenresAreNotNull()
        {
            //Arrange
            IEnumerable<ShowGenre> expectedGenres = new List<ShowGenre>();
            this.episodeCommand.Dto.Genres = new List<int> { 1, 3 };
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
            CollectionAssert.AreEqual(expectedGenres, this.episode.Genres);
        }

        [Test]
        public async Task Handle_ShouldNotEditEpisode_IfFilmingLocationsAreNotNull()
        {
            //Arrange
            IEnumerable<ShowFilmingLocation> expectedFilmingLocations = new List<ShowFilmingLocation>();
            this.episodeCommand.Dto.FilmingLocations = new List<int> { 1, 3 };
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
            CollectionAssert.AreEqual(expectedFilmingLocations, this.episode.FilmingLocations);
        }

        [Test]
        public async Task Handle_ShouldNotEditEpisode_IfCountriesOfOriginAreNotNull()
        {
            //Arrange
            IEnumerable<ShowCountryOfOrigin> expectedCountriesOfOrigin = new List<ShowCountryOfOrigin>();
            this.episodeCommand.Dto.CountriesOfOrigin = new List<int> { 1, 3 };
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
            CollectionAssert.AreEqual(expectedCountriesOfOrigin, this.episode.Genres);
        }

        [Test]
        public async Task Handle_ShouldNotEditEpisode_IfLanguagesAreNotNull()
        {
            //Arrange
            IEnumerable<ShowLanguage> expectedLanguages = new List<ShowLanguage>();
            this.episodeCommand.Dto.Genres = new List<int> { 1, 3 };
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
            CollectionAssert.AreEqual(expectedLanguages, this.episode.Genres);
        }

        private void SetUpReturningShow(Show? show)
        {
            IQueryable<Show> shows = new List<Show> { show! }.AsQueryable();

            TestAsyncEnumerableEfCore<Show> queryable = new TestAsyncEnumerableEfCore<Show>(shows);

            this.repositoryMock
                .Setup(r => r.All(It.IsAny<Expression<Func<Show, bool>>>())).Returns(queryable);
        }

        private void SetUpSaveChanges()
        {
            this.repositoryMock
                .Setup(r => r.SaveChangesAsync()).Throws(new InvalidOperationException("Save Changes fails"));
        }
    }
}