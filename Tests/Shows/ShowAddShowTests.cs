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
                    Name = "Singapore"
                },
                new FilmingLocation
                {
                    FilmingLocationId = 2,
                    Name = "USA"
                }
            ];
            this.languages =
            [
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
            ];
            this.countriesOfOrigin =
            [
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
            ];

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
            Result<ShowAddOrEditShowDto> result = await this.handler.Handle(this.episodeQuery, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("This show does not exist! Please select an existing one"));
            });
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfShowTypeIsInvalid()
        {
            //Arrange
            ShowAddShowQuery query = new()
            {
                ShowType = 0
            };

            //Act
            Result<ShowAddOrEditShowDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("The show type is invalid"));
            });
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

            ShowAddOrEditShowDto expectedDto = new()
            {
                ShowType = new()
                {
                    Id = 1,
                    Name = "Movie"
                },
                Series = null,
                Genres =
                [
                    new GenreDto
                    {
                        GenreId = 1,
                        Name = "Action"
                    },
                    new GenreDto
                    {
                        GenreId = 2,
                        Name = "Romance"
                    },
                    new GenreDto
                    {
                        GenreId = 3,
                        Name = "Comedy"
                    }
                ],
                Languages =
                [
                    new LanguageDto
                    {
                        LanguageId = 1,
                        Name = "English"
                    },
                    new LanguageDto
                    {
                        LanguageId = 2,
                        Name = "Chinese"
                    },
                    new LanguageDto
                    {
                        LanguageId = 3,
                        Name = "French"
                    },
                    new LanguageDto
                    {
                        LanguageId = 4,
                        Name = "Spanish"
                    }
                ],
                FilmingLocations =
                [
                    new FilmingLocationDto
                    {
                        FilmingLocationId = 1,
                        Name = "Singapore"
                    },
                    new FilmingLocationDto
                    {
                        FilmingLocationId = 2,
                        Name = "USA"
                    }
                ],
                CountriesOfOrigin =
                [
                    new CountryOfOriginDto
                    {
                        CountryOfOriginId = 1,
                        Name = "USA"
                    },
                    new CountryOfOriginDto
                    {
                        CountryOfOriginId = 2,
                        Name = "France"
                    }
                ]
            };

            //Act
            Result<ShowAddOrEditShowDto> result = await this.handler.Handle(this.movieQuery, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data, Is.EqualTo(expectedDto).Using(new ShowAddOrEditDtoComparer()));
            });
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

            ShowAddOrEditShowDto expectedDto = new()
            {
                ShowType = new()
                {
                    Id = 2,
                    Name = "TVSeries"
                },
                Series = null,
                Genres =
                [
                    new GenreDto
                    {
                        GenreId = 1,
                        Name = "Action"
                    },
                    new GenreDto
                    {
                        GenreId = 2,
                        Name = "Romance"
                    },
                    new GenreDto
                    {
                        GenreId = 3,
                        Name = "Comedy"
                    }
                ],
                Languages =
                [
                    new LanguageDto
                    {
                        LanguageId = 1,
                        Name = "English"
                    },
                    new LanguageDto
                    {
                        LanguageId = 2,
                        Name = "Chinese"
                    },
                    new LanguageDto
                    {
                        LanguageId = 3,
                        Name = "French"
                    },
                    new LanguageDto
                    {
                        LanguageId = 4,
                        Name = "Spanish"
                    }
                ],
                FilmingLocations =
                [
                    new FilmingLocationDto
                    {
                        FilmingLocationId = 1,
                        Name = "Singapore"
                    },
                    new FilmingLocationDto
                    {
                        FilmingLocationId = 2,
                     Name = "USA"
                    }
                ],
                CountriesOfOrigin =
                [
                    new CountryOfOriginDto
                    {
                        CountryOfOriginId = 1,
                        Name = "USA"
                    },
                    new CountryOfOriginDto
                    {
                        CountryOfOriginId = 2,
                        Name = "France"
                    }
                ]
            };

            //Act
            Result<ShowAddOrEditShowDto> result = await this.handler.Handle(this.tvSeriesQuery, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data, Is.EqualTo(expectedDto).Using(new ShowAddOrEditDtoComparer()));
            });
        }

        //Episode
        [Test]
        public async Task Handle_ShouldReturnDto_ForShowingAddShowPageForEpisode()
        {
            //Arrange
            SetUpReturningTvShow(this.tvSeries);

            ShowAddOrEditShowDto expectedDto = new()
            {
                ShowType = new()
                {
                    Id = 3,
                    Name = "Episode"
                },
                Series = new()
                {
                    Id = "b9099dc4-ff09-4b49-86b8-ac23572486fb",
                    Title = "TV Series Title"
                }
            };

            //Act
            Result<ShowAddOrEditShowDto> result = await this.handler.Handle(this.episodeQuery, CancellationToken.None);

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

        private void SetUpReturningTvShow(Show? show)
        {
            this.repositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Show, bool>>>()))
                .ReturnsAsync(show);
        }
    }
}