namespace Tests.Shows
{
    using Application.Response;
    using Domain;
    using Domain.Enums;
    using MediatR;
    using MockQueryable.EntityFrameworkCore;
    using Moq;
    using Persistence.Repositories;
    using System.Linq.Expressions;
    using static Application.Shows.DeleteShow;

    public class DeleteShowTests
    {
        private Mock<IRepository> repositoryMock;
        private DeleteShowHandler handler;
        private Show movie;
        private Show tvSeries;
        private Show episode;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IRepository>();
            this.handler = new DeleteShowHandler(this.repositoryMock.Object);
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
                ],
                UserReviews =
                [
                    new Review
                    {
                        ReviewId = Guid.NewGuid(),
                        UserId = Guid.NewGuid(),
                        Heading = "Movie Heading 1",
                        Content = "Movie Content 1",
                        CreatedAt = new DateTime(2023, 6, 5)
                    },
                    new Review
                    {
                        ReviewId = Guid.NewGuid(),
                        UserId = Guid.NewGuid(),
                        Heading = "Movie Heading 2",
                        Content = "Movie Content 2",
                        CreatedAt = new DateTime(2023, 6, 6)
                    }
                ],
                UserRatings =
                [
                    new Rating
                    {
                        UserId = Guid.NewGuid(),
                        Stars = 5
                    },
                    new Rating
                    {
                        UserId = Guid.NewGuid(),
                        Stars = 7
                    },
                    new Rating
                    {
                        UserId = Guid.NewGuid(),
                        Stars = 4
                    }
                ],
                WatchListItems =
                [
                    new WatchListItem
                    {
                        UserId = Guid.NewGuid()
                    },
                    new WatchListItem
                    {
                        UserId = Guid.NewGuid()
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
                ],
                UserReviews =
                [
                    new Review
                    {
                        ReviewId = Guid.NewGuid(),
                        UserId = Guid.NewGuid(),
                        Heading = "TV Series Heading 1",
                        Content = "TV Series Content 1",
                        CreatedAt = new DateTime(2023, 6, 5)
                    },
                    new Review
                    {
                        ReviewId = Guid.NewGuid(),
                        UserId = Guid.NewGuid(),
                        Heading = "Movie Heading 2",
                        Content = "Movie Content 2",
                        CreatedAt = new DateTime(2023, 6, 6)
                    }
                ],
                UserRatings =
                [
                    new Rating
                    {
                        UserId = Guid.NewGuid(),
                        Stars = 5
                    },
                    new Rating
                    {
                        UserId = Guid.NewGuid(),
                        Stars = 7
                    },
                    new Rating
                    {
                        UserId = Guid.NewGuid(),
                        Stars = 4
                    }
                ],
                WatchListItems =
                [
                    new WatchListItem
                    {
                        UserId = Guid.NewGuid()
                    },
                    new WatchListItem
                    {
                        UserId = Guid.NewGuid()
                    }
                ],
                Episodes =
                [
                    new Show
                    {
                        ShowId = Guid.NewGuid(),
                        ShowType = ShowType.Episode,
                    },
                    new Show
                    {
                        ShowId = Guid.NewGuid(),
                        ShowType = ShowType.Episode
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
                PhotoId = "photoId",
                UserReviews =
                [
                    new Review
                    {
                        ReviewId = Guid.NewGuid(),
                        UserId = Guid.NewGuid(),
                        Heading = "TV Series Heading 1",
                        Content = "TV Series Content 1",
                        CreatedAt = new DateTime(2023, 6, 5)
                    },
                    new Review
                    {
                        ReviewId = Guid.NewGuid(),
                        UserId = Guid.NewGuid(),
                        Heading = "Movie Heading 2",
                        Content = "Movie Content 2",
                        CreatedAt = new DateTime(2023, 6, 6)
                    }
                ],
                UserRatings =
                [
                    new Rating
                    {
                        UserId = Guid.NewGuid(),
                        Stars = 5
                    },
                    new Rating
                    {
                        UserId = Guid.NewGuid(),
                        Stars = 7
                    },
                    new Rating
                    {
                        UserId = Guid.NewGuid(),
                        Stars = 4
                    }
                ],
                WatchListItems =
                [
                    new WatchListItem
                    {
                        UserId = Guid.NewGuid()
                    },
                    new WatchListItem
                    {
                        UserId = Guid.NewGuid()
                    }
                ]
            };
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfShowDoesNotExist()
        {
            //Arrange
            SetUpReturningShow(null);
            DeleteShowCommand command = new();

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
        public async Task Handle_ShouldReturnError_IsSaveChangesFails()
        {
            //Arrange
            DeleteShowCommand command = new()
            {
                ShowId = "CD9B2F47-67D3-48C0-9E45-F55476F19ADB"
            };
            SetUpReturningShow(this.movie);
            SetUpSaveChanges();

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("Failed to delete show - Movie Title"));
            });
        }

        //Movie
        [Test]
        public async Task Handle_ShouldDeleteMovie_IfDataIsCorrect()
        {
            //Arrange
            DeleteShowCommand command = new()
            {
                ShowId = "CD9B2F47-67D3-48C0-9E45-F55476F19ADB"
            };
            SetUpReturningShow(this.movie);

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully deleted Movie Title"));
            });

            this.repositoryMock.Verify(r => r.Delete(this.movie), Times.Once);
        }

        //TV Series
        [Test]
        public async Task Handle_ShouldDeleteTVSeries_IfDataIsCorrect()
        {
            //Arrange
            DeleteShowCommand command = new()
            {
                ShowId = "CD9B2F47-67D3-48C0-9E45-F55476F19ADB"
            };
            SetUpReturningShow(this.tvSeries);

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully deleted TV Series Title"));
            });

            this.repositoryMock.Verify(r => r.Delete(this.tvSeries), Times.Once);
        }

        //Episode
        [Test]
        public async Task Handle_ShouldDeleteEpisode_IfDataIsCorrect()
        {
            DeleteShowCommand command = new()
            {
                ShowId = "5AE0C243-971A-4C51-9710-A87E1A45F4F0"
            };
            SetUpReturningShow(this.episode);

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully deleted Episode Title"));
            });

            this.repositoryMock.Verify(r => r.Delete(this.episode), Times.Once);
        }

        private void SetUpReturningShow(Show? show)
        {
            IQueryable<Show> shows = new List<Show> { show! }.AsQueryable();

            TestAsyncEnumerableEfCore<Show> queryable = new(shows);

            this.repositoryMock
                .Setup(r => r.All(It.IsAny<Expression<Func<Show, bool>>>()))
                .Returns(queryable);
        }

        private void SetUpSaveChanges()
        {
            this.repositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Throws(new InvalidOperationException("Save Changes fails"));
        }
    }
}
