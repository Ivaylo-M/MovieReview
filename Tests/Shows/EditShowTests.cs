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
    using Tests.Comparers.Shows;
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
                Genres =
                [
                    new ShowGenre
                    {
                        GenreId = 2
                    },
                    new ShowGenre
                    {
                        GenreId = 4
                    }
                ],
                FilmingLocations =
                [
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
                ],
                Languages =
                [
                    new ShowLanguage
                    {
                        LanguageId = 1
                    },
                    new ShowLanguage
                    {
                        LanguageId = 3
                    }
                ],
                CountriesOfOrigin =
                [
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 1
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 2
                    }
                ]
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
                Genres =
                [
                    new ShowGenre
                    {
                        GenreId = 2
                    },
                    new ShowGenre
                    {
                        GenreId = 4
                    }
                ],
                FilmingLocations =
                [
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
                ],
                Languages =
                [
                    new ShowLanguage
                    {
                        LanguageId = 1
                    },
                    new ShowLanguage
                    {
                        LanguageId = 3
                    }
                ],
                CountriesOfOrigin =
                [
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 1
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 2
                    }
                ]
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
                    Genres = [2, 4],
                    FilmingLocations = [1, 3, 4],
                    Languages = [1, 3],
                    CountriesOfOrigin = [1, 2]
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
                    Genres = [2, 4],
                    FilmingLocations = [1, 3, 4],
                    Languages = [1, 3],
                    CountriesOfOrigin = [1, 2]
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
            EditShowCommand command = new();

            SetUpReturningShow(null);

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("This show does not exist! Please select an existing one"));
            });
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
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("Failed to edit show - Movie Title"));
            });
        }

        //Movie
        [Test]
        public async Task Handle_ShouldNotEditMovie_IfDataIsTheSame()
        {
            //Arrange
            SetUpReturningShow(this.movie);

            Show expectedMovie = new()
            {
                ShowId = Guid.Parse("CD9B2F47-67D3-48C0-9E45-F55476F19ADB"),
                Title = "Movie Title",
                Description = "Movie Description",
                Duration = 123,
                ReleaseDate = new DateTime(2022, 3, 4),
                ShowType = ShowType.Movie,
                PhotoId = "photoId",
                Genres =
                [
                    new ShowGenre
                    {
                        GenreId = 2
                    },
                    new ShowGenre
                    {
                        GenreId = 4
                    }
                ],
                FilmingLocations =
                [
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
                ],
                Languages =
                [
                    new ShowLanguage
                    {
                        LanguageId = 1
                    },
                    new ShowLanguage
                    {
                        LanguageId = 3
                    }
                ],
                CountriesOfOrigin =
                [
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 1
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 2
                    }
                ]
            };

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            { 
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie, Is.EqualTo(expectedMovie).Using(new ShowComparer()));
            });
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
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title1"));
                Assert.That(this.movie.Title, Is.EqualTo(expectedShowTitle));
            });
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
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.Description, Is.EqualTo(expectedShowDescription));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfReleaseDateIsDifferent()
        {
            //Arrange
            DateTime expectedShowReleaseDate = new(2022, 4, 5);
            this.movieCommand.Dto.ReleaseDate = expectedShowReleaseDate;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.ReleaseDate, Is.EqualTo(expectedShowReleaseDate));
            });
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
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.Duration, Is.EqualTo(expectedDuration));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfTheGenresAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedGenres = [1, 3];
            this.movieCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfSomeOfTheGenresAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedGenres = [2, 3];
            this.movieCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfAddedMoreGenresThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedGenres = [2, 3, 4];
            this.movieCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfGenresAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedGenres = [2];
            this.movieCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfTheFilmingLocationsAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = [5, 6, 7];
            this.movieCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfSomeOfTheFilmingLocationsAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = [1, 3, 5];
            this.movieCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfAddedMoreFilmingLocationsThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = [1, 4, 5, 6];
            this.movieCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfFilmingLocationsAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = [5];
            this.movieCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfTheLanguagesAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = [2, 4];
            this.movieCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfSomeOfTheLanguagesAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = [1, 2];
            this.movieCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfAddedMoreLanguagesThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = [4, 5, 6];
            this.movieCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfLanguagesAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = [2];
            this.movieCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfTheCountriesOfOriginAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = [3, 4];
            this.movieCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfSomeOfTheCountriesOfOriginAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = [1, 3];
            this.movieCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfAddedMoreCountriesOfOriginThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = [3, 4, 5];
            this.movieCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
            });
        }

        [Test]
        public async Task Handle_ShouldEditMovie_IfCountriesOfOriginAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = [6];
            this.movieCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(this.movieCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Movie Title"));
                Assert.That(this.movie.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
            });
        }

        //TV Series
        [Test]
        public async Task Handle_ShouldNotEditTVSeries_IfDataIsTheSame()
        {
            //Arrange
            SetUpReturningShow(this.tvSeries);

            Show expectedTVSeries = new()
            {
                ShowId = Guid.Parse("CD9B2F47-67D3-48C0-9E45-F55476F19ADB"),
                Title = "TV Series Title",
                Description = "TV Series Description",
                ReleaseDate = new DateTime(2020, 4, 5),
                EndDate = new DateTime(2022, 3, 4),
                ShowType = ShowType.TVSeries,
                PhotoId = "photoId",
                Genres =
                [
                    new ShowGenre
                    {
                        GenreId = 2
                    },
                    new ShowGenre
                    {
                        GenreId = 4
                    }
                ],
                FilmingLocations =
                [
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
                ],
                Languages =
                [
                    new ShowLanguage
                    {
                        LanguageId = 1
                    },
                    new ShowLanguage
                    {
                        LanguageId = 3
                    }
                ],
                CountriesOfOrigin =
                [
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 1
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 2
                    }
                ]
            };

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries, Is.EqualTo(expectedTVSeries).Using(new ShowComparer()));
            });
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
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title1"));
                Assert.That(this.tvSeries.Title, Is.EqualTo(expectedShowTitle));
            });
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
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.Description, Is.EqualTo(expectedShowDescription));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfReleaseDateIsDifferent()
        {
            //Arrange
            DateTime expectedShowReleaseDate = new(2020, 4, 5);
            this.tvSeriesCommand.Dto.ReleaseDate = expectedShowReleaseDate;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.ReleaseDate, Is.EqualTo(expectedShowReleaseDate));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfEndDateIsDifferent()
        {
            //Arrange
            DateTime expectedEndDate = new(2023, 5, 6);
            this.tvSeriesCommand.Dto.EndDate = expectedEndDate;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.EndDate, Is.EqualTo(expectedEndDate));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfTheGenresAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedGenres = [1, 3];
            this.tvSeriesCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfSomeOfTheGenresAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedGenres = [2, 3];
            this.tvSeriesCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfAddedMoreGenresThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedGenres = [2, 3, 4];
            this.tvSeriesCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfGenresAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedGenres = [2];
            this.tvSeriesCommand.Dto.Genres = expectedGenres;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.Genres.Select(sg => sg.GenreId), Is.EqualTo(expectedGenres));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfTheFilmingLocationsAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = [5, 6, 7];
            this.tvSeriesCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfSomeOfTheFilmingLocationsAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = [1, 3, 5];
            this.tvSeriesCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfAddedMoreFilmingLocationsThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = [1, 4, 5, 6];
            this.tvSeriesCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfFilmingLocationsAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedFilmingLocations = [5];
            this.tvSeriesCommand.Dto.FilmingLocations = expectedFilmingLocations;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.FilmingLocations.Select(sfl => sfl.FilmingLocationId), Is.EqualTo(expectedFilmingLocations));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfTheLanguagesAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = [2, 4];
            this.tvSeriesCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfSomeOfTheLanguagesAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = [1, 2];
            this.tvSeriesCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfAddedMoreLanguagesThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = [4, 5, 6];
            this.tvSeriesCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfLanguagesAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedLanguages = [2];
            this.tvSeriesCommand.Dto.Languages = expectedLanguages;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.Languages.Select(sl => sl.LanguageId), Is.EqualTo(expectedLanguages));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfTheCountriesOfOriginAreAllDifferent()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = [3, 4];
            this.tvSeriesCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfSomeOfTheCountriesOfOriginAreDifferent()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = [1, 3];
            this.tvSeriesCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfAddedMoreCountriesOfOriginThenBefore()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = [3, 4, 5];
            this.tvSeriesCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
            });
        }

        [Test]
        public async Task Handle_ShouldEditTVSeries_IfCountriesOfOriginAreFewerThanBefore()
        {
            //Arrange
            IEnumerable<int> expectedCountriesOfOrigin = [6];
            this.tvSeriesCommand.Dto.CountriesOfOrigin = expectedCountriesOfOrigin;
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(this.tvSeriesCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited TV Series Title"));
                Assert.That(this.tvSeries.CountriesOfOrigin.Select(scoo => scoo.CountryOfOriginId), Is.EqualTo(expectedCountriesOfOrigin));
            });
        }

        //Episode
        [Test]
        public async Task Handle_ShouldNotEditEpisode_IfDataIsTheSame()
        {
            //Arrange
            SetUpReturningShow(this.episode);

            Show expectedEpisode = new()
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

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
                Assert.That(this.episode, Is.EqualTo(expectedEpisode).Using(new ShowComparer()));
            });
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
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title1"));
                Assert.That(this.episode.Title, Is.EqualTo(expectedShowTitle));
            });
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
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
                Assert.That(this.episode.Description, Is.EqualTo(expectedShowDescription));
            });
        }

        [Test]
        public async Task Handle_ShouldEditEpisode_IfReleaseDateIsDifferent()
        {
            //Arrange
            DateTime expectedShowReleaseDate = new(2022, 4, 5);
            this.episodeCommand.Dto.ReleaseDate = expectedShowReleaseDate;
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
                Assert.That(this.episode.ReleaseDate, Is.EqualTo(expectedShowReleaseDate));
            });
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
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
                Assert.That(this.episode.Season, Is.EqualTo(expectedSeason));
            });
        }

        [Test]
        public async Task Handle_ShouldNotEditEpisode_IfGenresAreNotNull()
        {
            //Arrange
            IEnumerable<ShowGenre> expectedGenres = [];
            this.episodeCommand.Dto.Genres = [1, 3];
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
                Assert.That(this.episode.Genres, Is.EqualTo(expectedGenres));
            });
        }

        [Test]
        public async Task Handle_ShouldNotEditEpisode_IfFilmingLocationsAreNotNull()
        {
            //Arrange
            IEnumerable<ShowFilmingLocation> expectedFilmingLocations = [];
            this.episodeCommand.Dto.FilmingLocations = [1, 3];
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
                Assert.That(this.episode.FilmingLocations, Is.EqualTo(expectedFilmingLocations));
            });
        }

        [Test]
        public async Task Handle_ShouldNotEditEpisode_IfCountriesOfOriginAreNotNull()
        {
            //Arrange
            IEnumerable<ShowCountryOfOrigin> expectedCountriesOfOrigin = [];
            this.episodeCommand.Dto.CountriesOfOrigin = [1, 3];
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
                Assert.That(this.episode.CountriesOfOrigin, Is.EqualTo(expectedCountriesOfOrigin));
            });
        }

        [Test]
        public async Task Handle_ShouldNotEditEpisode_IfLanguagesAreNotNull()
        {
            //Arrange
            IEnumerable<ShowLanguage> expectedLanguages = [];
            this.episodeCommand.Dto.Genres = [1, 3];
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(this.episodeCommand, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully edited Episode Title"));
                Assert.That(this.episode.Languages, Is.EqualTo(expectedLanguages));
            });
        }

        private void SetUpReturningShow(Show? show)
        {
            IQueryable<Show> shows = new List<Show> { show! }.AsQueryable();

            TestAsyncEnumerableEfCore<Show> queryable = new(shows);

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