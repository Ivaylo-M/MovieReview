namespace Tests.Shows
{
    using Application.DTOs.CountriesOfOrigin;
    using Application.DTOs.FilmingLocations;
    using Application.DTOs.Genres;
    using Application.DTOs.Languages;
    using Application.DTOs.Shows;
    using Application.Response;
    using Domain;
    using Domain.Enums;
    using MockQueryable.EntityFrameworkCore;
    using Moq;
    using Persistence.Repositories;
    using System.Linq.Expressions;
    using Tests.Comparers;
    using static Application.Shows.ShowEditShow;

    public class ShowEditShowTests
    {
        private Mock<IRepository> repositoryMock;
        private ShowEditShowHandler handler;
        private Show movie;
        private Show tvSeries;
        private Show episode;
        private IEnumerable<Genre> genres;
        private IEnumerable<FilmingLocation> filmingLocations;
        private IEnumerable<CountryOfOrigin> countryOfOrigins;
        private IEnumerable<Language> languages;

        [SetUp] 
        public void SetUp()
        {
            this.repositoryMock = new Mock<IRepository>();
            this.handler = new ShowEditShowHandler(this.repositoryMock.Object);

            this.movie = new Show
            {
                ShowId = Guid.Parse("306ec54c-896c-4e13-8e04-d5023b50a05f"),
                Title = "Movie Title",
                ShowType = ShowType.Movie,
                ReleaseDate = new DateTime(2022, 3, 4),
                Description = "Movie Description",
                Duration = 123,
                PhotoId = "photoId1",
                Photo = new Photo
                {
                    PhotoId = "photoId1",
                    Url = "Photo Url1"
                },
                Genres = new List<ShowGenre>
                {
                    new ShowGenre
                    {
                        GenreId = 1
                    },
                    new ShowGenre
                    {
                        GenreId = 3
                    }
                },
                FilmingLocations = new List<ShowFilmingLocation>
                {
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 2
                    },
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 5
                    },
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 7
                    }
                },
                Languages = new List<ShowLanguage>
                {
                    new ShowLanguage
                    {
                        LanguageId = 2
                    },
                    new ShowLanguage
                    {
                        LanguageId = 4
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
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 3
                    }
                }
            };
            this.tvSeries = new Show
            {
                ShowId = Guid.Parse("e7e8a36f-f4e1-4d69-8346-45a5244cbded"),
                Title = "TV Series Title",
                Description = "TV Series Description",
                ShowType = ShowType.TVSeries,
                ReleaseDate = new DateTime(2020, 4, 5),
                EndDate = new DateTime(2022, 3, 4),
                PhotoId = "photoId2",
                Photo = new Photo
                {
                    PhotoId = "photoId2",
                    Url = "Photo Url2"
                },
                Genres = new List<ShowGenre>
                {
                    new ShowGenre
                    {
                        GenreId = 2
                    },
                    new ShowGenre
                    {
                        GenreId = 3
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
                        FilmingLocationId = 2
                    },
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 3
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
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 3
                    }
                }
            };
            this.episode = new Show
            {
                ShowId = Guid.Parse("69b947e9-17c6-4c0b-a4ab-b8fb26947b6f"),
                Title = "Episode Title",
                Description = "Episode Description",
                ShowType = ShowType.Episode,
                ReleaseDate = new DateTime(2021, 6, 7),
                Duration = 23,
                Season = 1,
                PhotoId = "photoId3",
                Photo = new Photo
                {
                    PhotoId = "photoId3",
                    Url = "Photo Url3"
                },
                SeriesId = Guid.Parse("e436ba43-8fb3-4562-af2a-3869f94fe290"),
                Series = new Show
                {
                    ShowId = Guid.Parse("e436ba43-8fb3-4562-af2a-3869f94fe290"),
                    Title = "Title of TV Series"
                }
            };

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
                    Name = "USA"
                },
                new FilmingLocation
                {
                    FilmingLocationId = 2,
                    Name = "France"
                },
                new FilmingLocation
                {
                    FilmingLocationId = 3,
                    Name = "Spain"
                },
                new FilmingLocation
                {
                    FilmingLocationId = 4,
                    Name = "Siberia"
                },
                new FilmingLocation
                {
                    FilmingLocationId = 5,
                    Name = "Bulgaria"
                },
                new FilmingLocation
                {
                    FilmingLocationId = 6,
                    Name = "Japan"
                },
                new FilmingLocation
                {
                    FilmingLocationId = 7,
                    Name = "Monaco"
                },
            };
            this.languages = new List<Language>
            {
                new Language
                {
                    LanguageId = 1,
                    Name = "Bulgarian"
                },
                new Language
                {
                    LanguageId = 2,
                    Name = "English"
                },
                new Language
                {
                    LanguageId = 3,
                    Name = "Chinese"
                },
                new Language
                {
                    LanguageId = 4,
                    Name = "French"
                }
            };
            this.countryOfOrigins = new List<CountryOfOrigin>
            {
                new CountryOfOrigin
                {
                    CountryOfOriginId = 1,
                    Name = "Spain"
                },
                new CountryOfOrigin
                {
                    CountryOfOriginId = 2,
                    Name = "France"
                },
                new CountryOfOrigin
                {
                    CountryOfOriginId = 3,
                    Name = "Bulgaria"
                },
                new CountryOfOrigin
                {
                    CountryOfOriginId = 4,
                    Name = "Russia"
                }
            };
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfTheShowDoesNotExist()
        {
            //Arrange
            SetUpReturningShow(null);
            ShowEditShowQuery query = new ShowEditShowQuery();

            //Act
            Result<ShowEditShowDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This show does not exist! Please select an existing one"));
        }

        //Movie
        [Test]
        public async Task Handle_ShouldReturnDto_IfTheMovieDataIsCorrent()
        {
            //Arrange
            SetUpReturningShow(this.movie);

            SetUpReturningCollection(this.genres);
            SetUpReturningCollection(this.filmingLocations);
            SetUpReturningCollection(this.languages);
            SetUpReturningCollection(this.countryOfOrigins);

            ShowEditShowQuery query = new ShowEditShowQuery
            {
                ShowId = "306ec54c-896c-4e13-8e04-d5023b50a05f"
            };

            IEnumerable<GenreDto> expectedGenres = new List<GenreDto>
            {
                new GenreDto
                {
                    GenreId = 1,
                    Name = "Action",
                    HasValue = true
                },
                new GenreDto
                {
                    GenreId = 2,
                    Name = "Romance",
                    HasValue = false
                },
                new GenreDto
                {
                    GenreId = 3,
                    Name = "Comedy",
                    HasValue = true
                }
            };
            IEnumerable<FilmingLocationDto> expectedFilmingLocation = new List<FilmingLocationDto>
            {
                new FilmingLocationDto
                {
                    FilmingLocationId = 1,
                    Name = "USA",
                    HasValue = false
                },
                new FilmingLocationDto
                {
                    FilmingLocationId = 2,
                    Name = "France",
                    HasValue = true
                },
                new FilmingLocationDto
                {
                    FilmingLocationId = 3,
                    Name = "Spain",
                    HasValue = false
                },
                new FilmingLocationDto
                {
                    FilmingLocationId = 4,
                    Name = "Siberia",
                    HasValue = false
                },
                new FilmingLocationDto
                {
                    FilmingLocationId = 5,
                    Name = "Bulgaria",
                    HasValue = true
                },
                new FilmingLocationDto
                {
                    FilmingLocationId = 6,
                    Name = "Japan",
                    HasValue = false
                },
                new FilmingLocationDto
                {
                    FilmingLocationId = 7,
                    Name = "Monaco",
                    HasValue = true
                }
            };
            IEnumerable<LanguageDto> expectedLanguages = new List<LanguageDto>
            {
                new LanguageDto
                {
                    LanguageId = 1,
                    Name = "Bulgarian",
                    HasValue = false
                },
                new LanguageDto
                {
                    LanguageId = 2,
                    Name = "English",
                    HasValue = true
                },
                new LanguageDto
                {
                    LanguageId = 3,
                    Name = "Chinese",
                    HasValue = false
                },
                new LanguageDto
                {
                    LanguageId = 4,
                    Name = "French",
                    HasValue = true
                }
            };
            IEnumerable<CountryOfOriginDto> expectedCountriesOfOrigin = new List<CountryOfOriginDto>
            {
                new CountryOfOriginDto
                {
                    CountryOfOriginId = 1,
                    Name = "Spain",
                    HasValue = true
                },
                new CountryOfOriginDto
                {
                    CountryOfOriginId = 2,
                    Name = "France",
                    HasValue = true
                },
                new CountryOfOriginDto
                {
                    CountryOfOriginId = 3,
                    Name = "Bulgaria",
                    HasValue = true
                },
                new CountryOfOriginDto
                {
                    CountryOfOriginId = 4,
                    Name = "Russia",
                    HasValue = false
                }
            };

            //Act
            Result<ShowEditShowDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Title, Is.EqualTo("Movie Title"));
            Assert.That(result.Data!.Description, Is.EqualTo("Movie Description"));
            Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2022, 3, 4)));
            Assert.That(result.Data!.Duration, Is.EqualTo(123));
            Assert.That(result.Data!.ShowType.Id, Is.EqualTo(1));
            Assert.That(result.Data!.ShowType.Name, Is.EqualTo("Movie"));
            Assert.That(result.Data!.EndDate, Is.EqualTo(null));
            Assert.That(result.Data!.Series, Is.EqualTo(null));
            Assert.That(result.Data!.Season, Is.EqualTo(null));
            Assert.That(result.Data!.Photo!.Id, Is.EqualTo("photoId1"));
            Assert.That(result.Data!.Photo!.Url, Is.EqualTo("Photo Url1"));

            Assert.That(result.Data!.Genres, Is.EquivalentTo(expectedGenres).Using(new GenreDtoComparer()));
            Assert.That(result.Data!.FilmingLocations, Is.EquivalentTo(expectedFilmingLocation).Using(new FilmingLocationDtoComparer()));
            Assert.That(result.Data!.CountriesOfOrigin, Is.EquivalentTo(expectedCountriesOfOrigin).Using(new CountryOfOriginDtoComparer()));
            Assert.That(result.Data!.Languages, Is.EquivalentTo(expectedLanguages).Using(new LanguageDtoComparer()));
        }

        //TV Series
        [Test]
        public async Task Handle_ShouldReturnDto_IfTheTVSeriesDataIsCorrect()
        {
            //Arrange
            SetUpReturningShow(this.tvSeries);

            SetUpReturningCollection(this.genres);
            SetUpReturningCollection(this.filmingLocations);
            SetUpReturningCollection(this.languages);
            SetUpReturningCollection(this.countryOfOrigins);

            ShowEditShowQuery query = new ShowEditShowQuery
            {
                ShowId = "e7e8a36f-f4e1-4d69-8346-45a5244cbded"
            };

            IEnumerable<GenreDto> expectedGenres = new List<GenreDto>
            {
                new GenreDto
                {
                    GenreId = 1,
                    Name = "Action",
                    HasValue = false
                },
                new GenreDto
                {
                    GenreId = 2,
                    Name = "Romance",
                    HasValue = true
                },
                new GenreDto
                {
                    GenreId = 3,
                    Name = "Comedy",
                    HasValue = true
                }
            };
            IEnumerable<FilmingLocationDto> expectedFilmingLocation = new List<FilmingLocationDto>
            {
                new FilmingLocationDto
                {
                    FilmingLocationId = 1,
                    Name = "USA",
                    HasValue = true
                },
                new FilmingLocationDto
                {
                    FilmingLocationId = 2,
                    Name = "France",
                    HasValue = true
                },
                new FilmingLocationDto
                {
                    FilmingLocationId = 3,
                    Name = "Spain",
                    HasValue = true
                },
                new FilmingLocationDto
                {
                    FilmingLocationId = 4,
                    Name = "Siberia",
                    HasValue = false
                },
                new FilmingLocationDto
                {
                    FilmingLocationId = 5,
                    Name = "Bulgaria",
                    HasValue = false
                },
                new FilmingLocationDto
                {
                    FilmingLocationId = 6,
                    Name = "Japan",
                    HasValue = false
                },
                new FilmingLocationDto
                {
                    FilmingLocationId = 7,
                    Name = "Monaco",
                    HasValue = false
                }
            };
            IEnumerable<LanguageDto> expectedLanguages = new List<LanguageDto>
            {
                new LanguageDto
                {
                    LanguageId = 1,
                    Name = "Bulgarian",
                    HasValue = true
                },
                new LanguageDto
                {
                    LanguageId = 2,
                    Name = "English",
                    HasValue = false
                },
                new LanguageDto
                {
                    LanguageId = 3,
                    Name = "Chinese",
                    HasValue = true
                },
                new LanguageDto
                {
                    LanguageId = 4,
                    Name = "French",
                    HasValue = false
                }
            };
            IEnumerable<CountryOfOriginDto> expectedCountriesOfOrigin = new List<CountryOfOriginDto>
            {
                new CountryOfOriginDto
                {
                    CountryOfOriginId = 1,
                    Name = "Spain",
                    HasValue = true
                },
                new CountryOfOriginDto
                {
                    CountryOfOriginId = 2,
                    Name = "France",
                    HasValue = true
                },
                new CountryOfOriginDto
                {
                    CountryOfOriginId = 3,
                    Name = "Bulgaria",
                    HasValue = true
                },
                new CountryOfOriginDto
                {
                    CountryOfOriginId = 4,
                    Name = "Russia",
                    HasValue = false
                }
            };

            //Act
            Result<ShowEditShowDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Title, Is.EqualTo("TV Series Title"));
            Assert.That(result.Data!.Description, Is.EqualTo("TV Series Description"));
            Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2020, 4, 5)));
            Assert.That(result.Data!.EndDate, Is.EqualTo(new DateTime(2022, 3, 4)));
            Assert.That(result.Data!.ShowType.Id, Is.EqualTo(2));
            Assert.That(result.Data!.ShowType.Name, Is.EqualTo("TVSeries"));
            Assert.That(result.Data!.Duration, Is.EqualTo(null));
            Assert.That(result.Data!.Series, Is.EqualTo(null));
            Assert.That(result.Data!.Season, Is.EqualTo(null));
            Assert.That(result.Data!.Photo!.Id, Is.EqualTo("photoId2"));
            Assert.That(result.Data!.Photo!.Url, Is.EqualTo("Photo Url2"));

            Assert.That(result.Data!.Genres, Is.EquivalentTo(expectedGenres).Using(new GenreDtoComparer()));
            Assert.That(result.Data!.FilmingLocations, Is.EquivalentTo(expectedFilmingLocation).Using(new FilmingLocationDtoComparer()));
            Assert.That(result.Data!.CountriesOfOrigin, Is.EquivalentTo(expectedCountriesOfOrigin).Using(new CountryOfOriginDtoComparer()));
            Assert.That(result.Data!.Languages, Is.EquivalentTo(expectedLanguages).Using(new LanguageDtoComparer()));
        }

        //Episode
        [Test]
        public async Task Handle_ShouldReturnDto_IfTheEpisodeDateIsCorrect()
        {
            //Arrange
            SetUpReturningShow(this.episode);

            ShowEditShowQuery query = new ShowEditShowQuery
            {
                ShowId = "69b947e9-17c6-4c0b-a4ab-b8fb26947b6f"
            };

            //Act
            Result<ShowEditShowDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.That(result.Data!.Title, Is.EqualTo("Episode Title"));
            Assert.That(result.Data!.Description, Is.EqualTo("Episode Description"));
            Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2021, 6, 7)));
            Assert.That(result.Data!.EndDate, Is.EqualTo(null));
            Assert.That(result.Data!.ShowType.Id, Is.EqualTo(3));
            Assert.That(result.Data!.ShowType.Name, Is.EqualTo("Episode"));
            Assert.That(result.Data!.Duration, Is.EqualTo(23));
            Assert.That(result.Data!.Series!.Id, Is.EqualTo("e436ba43-8fb3-4562-af2a-3869f94fe290"));
            Assert.That(result.Data!.Series!.Title, Is.EqualTo("Title of TV Series"));
            Assert.That(result.Data!.Season, Is.EqualTo(1));
            Assert.That(result.Data!.Photo!.Id, Is.EqualTo("photoId3"));
            Assert.That(result.Data!.Photo!.Url, Is.EqualTo("Photo Url3"));
        }

        private void SetUpReturningCollection<T>(IEnumerable<T> collection) where T : class
        {
            TestAsyncEnumerableEfCore<T> queryable = new TestAsyncEnumerableEfCore<T>(collection.AsQueryable());

            this.repositoryMock
                .Setup(r => r.All<T>())
                .Returns(queryable);
        }

        private void SetUpReturningShow(Show? show)
        {
            IQueryable<Show> shows = new List<Show> { show! }.AsQueryable();

            TestAsyncEnumerableEfCore<Show> queryable = new TestAsyncEnumerableEfCore<Show>(shows);

            this.repositoryMock
                .Setup(r => r.All(It.IsAny<Expression<Func<Show, bool>>>()))
                .Returns(queryable);
        }
    }
}