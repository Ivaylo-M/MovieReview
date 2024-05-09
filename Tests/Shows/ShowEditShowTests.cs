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
    using Tests.Comparers.Shows;
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
                PhotoId = "PhotoId1",
                Photo = new Photo
                {
                    PhotoId = "PhotoId1",
                    Url = "Photo Url1"
                },
                Genres =
                [
                    new ShowGenre
                    {
                        GenreId = 1
                    },
                    new ShowGenre
                    {
                        GenreId = 3
                    }
                ],
                FilmingLocations =
                [
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
                ],
                Languages =
                [
                    new ShowLanguage
                    {
                        LanguageId = 2
                    },
                    new ShowLanguage
                    {
                        LanguageId = 4
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
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 3
                    }
                ]
            };
            this.tvSeries = new Show
            {
                ShowId = Guid.Parse("e7e8a36f-f4e1-4d69-8346-45a5244cbded"),
                Title = "TV Series Title",
                Description = "TV Series Description",
                ShowType = ShowType.TVSeries,
                ReleaseDate = new DateTime(2020, 4, 5),
                EndDate = new DateTime(2022, 3, 4),
                PhotoId = "PhotoId2",
                Photo = new Photo
                {
                    PhotoId = "PhotoId2",
                    Url = "Photo Url2"
                },
                Genres =
                [
                    new ShowGenre
                    {
                        GenreId = 2
                    },
                    new ShowGenre
                    {
                        GenreId = 3
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
                        FilmingLocationId = 2
                    },
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 3
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
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 3
                    }
                ]
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
                PhotoId = "PhotoId3",
                Photo = new Photo
                {
                    PhotoId = "PhotoId3",
                    Url = "Photo Url3"
                },
                SeriesId = Guid.Parse("e436ba43-8fb3-4562-af2a-3869f94fe290"),
                Series = new Show
                {
                    ShowId = Guid.Parse("e436ba43-8fb3-4562-af2a-3869f94fe290"),
                    Title = "Title of TV Series"
                }
            };

            this.genres =
            [
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
            ];
            this.filmingLocations =
            [
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
            ];
            this.languages =
            [
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
            ];
            this.countryOfOrigins =
            [
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
            ];
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfTheShowDoesNotExist()
        {
            //Arrange
            SetUpReturningShow(null);
            ShowEditShowQuery query = new();

            //Act
            Result<ShowAddOrEditShowDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("This show does not exist! Please select an existing one"));
            });
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

            ShowEditShowQuery query = new()
            {
                ShowId = "306ec54c-896c-4e13-8e04-d5023b50a05f"
            };

            ShowAddOrEditShowDto expectedDto = new()
            {
                Title = "Movie Title",
                Description = "Movie Description",
                ReleaseDate = new DateTime(2022, 3, 4),
                Duration = 123,
                ShowType = new()
                {
                    Id = 1,
                    Name = "Movie"
                },
                Photo = new()
                {
                    Id = "PhotoId1",
                    Url = "Photo Url1"
                },
                Genres = 
                [
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
                ],
                FilmingLocations =
                [
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
                ],
                Languages =
                [
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
                ],
                CountriesOfOrigin =
                [
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
                ]
            };

            //Act
            Result<ShowAddOrEditShowDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data, Is.EqualTo(expectedDto).Using(new ShowAddOrEditDtoComparer()));
            });
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

            ShowEditShowQuery query = new()
            {
                ShowId = "e7e8a36f-f4e1-4d69-8346-45a5244cbded"
            };

            ShowAddOrEditShowDto expectedDto = new()
            {
                Title = "TV Series Title",
                Description = "TV Series Description",
                ReleaseDate = new DateTime(2020, 4, 5),
                EndDate = new DateTime(2022, 3, 4),
                ShowType = new()
                {
                    Id = 2,
                    Name = "TVSeries"
                },
                Photo = new()
                {
                    Id = "PhotoId2",
                    Url = "Photo Url2"
                },
                Genres =
                [
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
                ],
                FilmingLocations =
                [
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
                ],
                Languages =
                [
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
                ],
                CountriesOfOrigin =
                [
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
                ]
            };

            //Act
            Result<ShowAddOrEditShowDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data, Is.EqualTo(expectedDto).Using(new ShowAddOrEditDtoComparer()));
            });
        }

        //Episode
        [Test]
        public async Task Handle_ShouldReturnDto_IfTheEpisodeDateIsCorrect()
        {
            //Arrange
            SetUpReturningShow(this.episode);

            ShowEditShowQuery query = new()
            {
                ShowId = "69b947e9-17c6-4c0b-a4ab-b8fb26947b6f"
            };

            ShowAddOrEditShowDto expectedDto = new()
            {
                Title = "Episode Title",
                Description = "Episode Description",
                ReleaseDate = new DateTime(2021, 6, 7),
                Duration = 23,
                ShowType = new()
                {
                    Id = 3,
                    Name = "Episode"
                },
                Photo = new()
                {
                    Id = "PhotoId3",
                    Url = "Photo Url3"
                },
                Season = 1,
                Series = new()
                {
                    Id = "e436ba43-8fb3-4562-af2a-3869f94fe290",
                    Title = "Title of TV Series"
                }
            };

            //Act
            Result<ShowAddOrEditShowDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data, Is.EqualTo(expectedDto).Using(new ShowAddOrEditDtoComparer()));
            });
        }

        private void SetUpReturningCollection<T>(IEnumerable<T> collection) where T : class
        {
            TestAsyncEnumerableEfCore<T> queryable = new(collection.AsQueryable());

            this.repositoryMock
                .Setup(r => r.All<T>())
                .Returns(queryable);
        }

        private void SetUpReturningShow(Show? show)
        {
            IQueryable<Show> shows = new List<Show> { show! }.AsQueryable();

            TestAsyncEnumerableEfCore<Show> queryable = new(shows);

            this.repositoryMock
                .Setup(r => r.All(It.IsAny<Expression<Func<Show, bool>>>()))
                .Returns(queryable);
        }
    }
}