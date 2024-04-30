namespace Tests.Shows
{
    using Application.DTOs.Reviews;
    using Application.DTOs.Shows;
    using Application.Response;
    using Domain;
    using Domain.Enums;
    using MockQueryable.EntityFrameworkCore;
    using Moq;
    using Persistence.Repositories;
    using System.Linq.Expressions;
    using Tests.Comparers;
    using Tests.Comparers.Reviews;
    using Tests.Comparers.Shows;
    using static Application.Shows.ShowDetails;

    public class ShowDetailsTests
    {
        private Mock<IRepository> repositoryMock;
        private ShowDetailsHandler handler;
        private Show movie;
        private Show tvSeries;
        private Show episode;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IRepository>();
            this.handler = new ShowDetailsHandler(this.repositoryMock.Object);

            Dictionary<int, Genre> genres = new()
            {
                { 1, new Genre { GenreId = 1, Name = "Comedy" } },
                { 2, new Genre { GenreId = 2, Name = "Romance" } },
                { 3, new Genre { GenreId = 3, Name = "Action" } },
                { 4, new Genre { GenreId = 4, Name = "Sci-fi" } },
                { 5, new Genre { GenreId = 5, Name = "Fantasy" }},
                { 6, new Genre { GenreId = 6, Name = "Drama" }} 
            };

            Dictionary<int, Language> languages = new()
            {
                { 1, new Language { LanguageId = 1, Name = "English" } },
                { 2, new Language { LanguageId = 1, Name = "Spanish" } },
                { 3, new Language { LanguageId = 1, Name = "Russian" } },
                { 4, new Language { LanguageId = 1, Name = "Mandarin" } },
                { 5, new Language { LanguageId = 1, Name = "French" } },
                { 6, new Language { LanguageId = 1, Name = "Romanian" } }
            };

            Dictionary<int, CountryOfOrigin> countriesOfOrigin = new()
            {
                { 1, new CountryOfOrigin { CountryOfOriginId = 1, Name = "Bulgaria" } },
                { 2, new CountryOfOrigin { CountryOfOriginId = 2, Name = "Spain" } },
                { 3, new CountryOfOrigin { CountryOfOriginId = 3, Name = "China" } },
                { 4, new CountryOfOrigin { CountryOfOriginId = 4, Name = "Argentina" } },
                { 5, new CountryOfOrigin { CountryOfOriginId = 5, Name = "USA" } },
                { 6, new CountryOfOrigin { CountryOfOriginId = 6, Name = "UK" } }
            };

            Dictionary<int, FilmingLocation> filmingLocations = new()
            {
                { 1, new FilmingLocation { FilmingLocationId = 1, Name = "Malibu" } },
                { 2, new FilmingLocation { FilmingLocationId = 2, Name = "Malta" } },
                { 3, new FilmingLocation { FilmingLocationId = 3, Name = "NDK" } },
                { 4, new FilmingLocation { FilmingLocationId = 4, Name = "LA" } },
                { 5, new FilmingLocation { FilmingLocationId = 5, Name = "Hogwarts" } },
                { 6, new FilmingLocation { FilmingLocationId = 6, Name = "Dallas" } }
            };

            this.movie = new Show
            {
                ShowId = Guid.Parse("70752D2F-C3AD-46D1-94BD-24610CA60529"),
                Title = "Movie Title",
                Description = "Movie Description",
                ReleaseDate = new DateTime(2022, 3, 4),
                Duration = 123,
                ShowType = ShowType.Movie,
                Photo = new Photo 
                {
                    PhotoId = "photoId",
                    Url = "photoUrl"
                },
                Genres =
                [
                    new ShowGenre
                    {
                        GenreId = 1,
                        Genre = genres[1]
                    },
                    new ShowGenre
                    {
                        GenreId = 2,
                        Genre = genres[2]
                    },
                    new ShowGenre 
                    {
                        GenreId = 6,
                        Genre = genres[6]
                    }
                ],
                Languages =
                [
                    new ShowLanguage
                    {
                        LanguageId = 1,
                        Language = languages[1]
                    },
                    new ShowLanguage
                    {
                        LanguageId = 2,
                        Language = languages[2]
                    },
                    new ShowLanguage
                    {
                        LanguageId = 3,
                        Language = languages[3]
                    }
                ],
                CountriesOfOrigin =
                [
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 1,
                        CountryOfOrigin = countriesOfOrigin[1]
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 2,
                        CountryOfOrigin = countriesOfOrigin[2]
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 3,
                        CountryOfOrigin = countriesOfOrigin[3]
                    }
                ],
                FilmingLocations =
                [
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 1,
                        FilmingLocation = filmingLocations[1]
                    },
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 2,
                        FilmingLocation = filmingLocations[2]
                    },
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 3,
                        FilmingLocation = filmingLocations[3]
                    }
                ],
                UserRatings =
                [
                    new Rating 
                    {
                        UserId = Guid.Parse("084CC46F-5456-4686-8067-9C919969A46E"),
                        Stars = 5
                    },
                    new Rating
                    {
                        UserId = Guid.Parse("94A36B0B-CBE3-4EE4-BCD4-C39F10051193"),
                        Stars = 8,
                    },
                    new Rating
                    {
                        UserId = Guid.Parse("F041A629-BD68-481D-8355-026B3F98FB69"),
                        Stars = 9
                    }
                ],
                UserReviews =
                [
                    new Review 
                    {
                        ReviewId = Guid.Parse("BD78A7F8-E1B3-413C-8419-0E147E076341"),
                        UserId = Guid.Parse("084CC46F-5456-4686-8067-9C919969A46E"),
                        Heading = "Review Heading1",
                        Content = "Review Content1",
                        CreatedAt = new DateTime(2022, 5, 6)
                    },
                    new Review 
                    {
                        ReviewId = Guid.Parse("BD78A7F8-E1B3-413C-8419-0E147E076341"),
                        UserId = Guid.Parse("94A36B0B-CBE3-4EE4-BCD4-C39F10051193"),
                        Heading = "Review Heading2",
                        Content = "Review Content2",
                        CreatedAt = new DateTime(2022, 6, 6)
                    },
                    new Review 
                    {
                        ReviewId = Guid.Parse("BD78A7F8-E1B3-413C-8419-0E147E076341"),
                        UserId = Guid.Parse("94A36B0B-CBE3-4EE4-BCD4-C39F10051193"),
                        Heading = "Review Heading3",
                        Content = "Review Content3",
                        CreatedAt = new DateTime(2022, 7, 6)
                    },

                ]
            };

            this.tvSeries = new Show
            {
                ShowId = Guid.Parse("0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6"),
                Title = "TV Series Title",
                Description = "TV Series Description",
                ReleaseDate = new DateTime(2020, 1, 2),
                EndDate = new DateTime(2021, 2, 3),
                ShowType = ShowType.TVSeries,
                Photo = new Photo
                {
                    PhotoId = "photoId",
                    Url = "photoUrl"
                },
                Genres =
                [
                    new ShowGenre
                    {
                        GenreId = 3,
                        Genre = genres[3]
                    },
                    new ShowGenre
                    {
                        GenreId = 4,
                        Genre = genres[4]
                    },
                    new ShowGenre
                    {
                        GenreId = 5,
                        Genre = genres[5]
                    }
                ],
                Languages =
                [
                    new ShowLanguage
                    {
                        LanguageId = 4,
                        Language = languages[4]
                    },
                    new ShowLanguage
                    {
                        LanguageId = 5,
                        Language = languages[5]
                    },
                    new ShowLanguage
                    {
                        LanguageId = 6,
                        Language = languages[6]
                    }
                ],
                CountriesOfOrigin =
                [
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 4,
                        CountryOfOrigin = countriesOfOrigin[4]
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 5,
                        CountryOfOrigin = countriesOfOrigin[5]
                    },
                    new ShowCountryOfOrigin
                    {
                        CountryOfOriginId = 6,
                        CountryOfOrigin = countriesOfOrigin[6]
                    }
                ],
                FilmingLocations =
                [
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 4,
                        FilmingLocation = filmingLocations[4]
                    },
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 5,
                        FilmingLocation = filmingLocations[5]
                    },
                    new ShowFilmingLocation
                    {
                        FilmingLocationId = 6,
                        FilmingLocation = filmingLocations[6]
                    }
                ],
                UserRatings =
                [
                    new Rating 
                    {
                        UserId = Guid.Parse("94A36B0B-CBE3-4EE4-BCD4-C39F10051193"),
                        Stars = 5
                    },
                    new Rating
                    {
                        UserId = Guid.Parse("F5741973-F252-4209-9F91-8C0A41B6CEA6"),
                        Stars = 8,
                    },
                    new Rating
                    {
                        UserId = Guid.Parse("F041A629-BD68-481D-8355-026B3F98FB69"),
                        Stars = 9
                    }
                ],
                UserReviews =
                [
                    new Review 
                    {
                        ReviewId = Guid.Parse("BD78A7F8-E1B3-413C-8419-0E147E076341"),
                        UserId = Guid.Parse("084CC46F-5456-4686-8067-9C919969A46E"),
                        Heading = "Review Heading1",
                        Content = "Review Content1",
                        CreatedAt = new DateTime(2022, 5, 6)
                    },
                    new Review 
                    {
                        ReviewId = Guid.Parse("BD78A7F8-E1B3-413C-8419-0E147E076341"),
                        UserId = Guid.Parse("084CC46F-5456-4686-8067-9C919969A46E"),
                        Heading = "Review Heading2",
                        Content = "Review Content2",
                        CreatedAt = new DateTime(2022, 6, 6)
                    },
                    new Review 
                    {
                        ReviewId = Guid.Parse("BD78A7F8-E1B3-413C-8419-0E147E076341"),
                        UserId = Guid.Parse("94A36B0B-CBE3-4EE4-BCD4-C39F10051193"),
                        Heading = "Review Heading3",
                        Content = "Review Content3",
                        CreatedAt = new DateTime(2022, 7, 6)
                    },
                ],
                Episodes =
                [
                    new Show
                    {
                        ShowId = Guid.Parse("3483F9F3-F068-4039-8E81-596BC6905B21"),
                        Duration = 23,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2020, 5, 4),
                        Season = 1
                    },
                    new Show
                    {
                        ShowId = Guid.Parse("D6EFF920-435F-441F-BBEF-DECF94E920D5"),
                        Duration = 22,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2020, 7, 8),
                        Season = 1
                    },
                    new Show
                    {
                        ShowId = Guid.Parse("36F69834-6457-4DC6-90A2-FADFFA2073EB"),
                        Duration = 22,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2021, 1, 7),
                        Season = 2
                    },
                    new Show
                    {
                        ShowId = Guid.Parse("1E866DF4-BD4B-42E4-89E8-B23775BAB089"),
                        Duration = 22,
                        ShowType = ShowType.Episode,
                        ReleaseDate = new DateTime(2021, 2, 7),
                        Season = 2
                    }
                ]
            };

            this.episode = new Show 
            {
                ShowId = Guid.Parse("6019FFB7-6E56-4AD8-8149-5B7541021C48"),
                Title = "Episode Title",
                Description = "Episode Description",
                ReleaseDate = new DateTime(2021, 1, 1),
                ShowType = ShowType.Episode,
                Duration = 22,
                Photo = new Photo
                {
                    PhotoId = "photoId",
                    Url = "photoUrl"
                },
                Season = 1,
                SeriesId = Guid.Parse("0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6"),
                Series = this.tvSeries,
                UserRatings =
                [
                    new Rating 
                    {
                        UserId = Guid.Parse("084CC46F-5456-4686-8067-9C919969A46E"),
                        Stars = 5
                    },
                    new Rating
                    {
                        UserId = Guid.Parse("94A36B0B-CBE3-4EE4-BCD4-C39F10051193"),
                        Stars = 8,
                    },
                    new Rating
                    {
                        UserId = Guid.Parse("F041A629-BD68-481D-8355-026B3F98FB69"),
                        Stars = 9
                    }
                ],
                UserReviews =
                [
                    new Review 
                    {
                        ReviewId = Guid.Parse("BD78A7F8-E1B3-413C-8419-0E147E076341"),
                        UserId = Guid.Parse("084CC46F-5456-4686-8067-9C919969A46E"),
                        Heading = "Review Heading1",
                        Content = "Review Content1",
                        CreatedAt = new DateTime(2022, 5, 6)
                    },
                    new Review 
                    {
                        ReviewId = Guid.Parse("BD78A7F8-E1B3-413C-8419-0E147E076341"),
                        UserId = Guid.Parse("084CC46F-5456-4686-8067-9C919969A46E"),
                        Heading = "Review Heading2",
                        Content = "Review Content2",
                        CreatedAt = new DateTime(2022, 6, 6)
                    },
                    new Review 
                    {
                        ReviewId = Guid.Parse("BD78A7F8-E1B3-413C-8419-0E147E076341"),
                        UserId = Guid.Parse("94A36B0B-CBE3-4EE4-BCD4-C39F10051193"),
                        Heading = "Review Heading3",
                        Content = "Review Content3",
                        CreatedAt = new DateTime(2022, 7, 6)
                    },
                ]
            };

            this.episode.Series!.Episodes!.Add(episode);
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfTheShowDoesNotExist()
        {
            //Arrange
            SetUpReturningShow(null);
            ShowDetailsQuery query = new();

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("This show does not exist! Please select an existing one"));
            });
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfTheUserDoesNotExist()
        {
            //Arrange
            SetUpReturningShow(this.movie);
            SetUpCheckingUser(false);
            ShowDetailsQuery query = new()
            {
                ShowId = "70752D2F-C3AD-46D1-94BD-24610CA60529"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("The user is not found"));
            });
        }

        //Movie
        [Test]
        public async Task Handle_ShouldReturnMovieDto_WithCorrectData() 
        {
            //Arrange
            this.movie.UserRatings = null!;
            this.movie.UserReviews = null!;

            SetUpReturningShow(this.movie);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Comedy", "Romance", "Drama"];
            IEnumerable<string> expectedLanguages = ["English", "Spanish", "Russian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Bulgaria", "Spain", "China"];
            IEnumerable<string> expectedFilmingLocations = ["Malibu", "Malta", "NDK"];


            ShowDetailsQuery query = new()
            {
                ShowId = "70752D2F-C3AD-46D1-94BD-24610CA60529",
                UserId = "084CC46F-5456-4686-8067-9C919969A46E"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("70752D2F-C3AD-46D1-94BD-24610CA60529"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.Movie));
                Assert.That(result.Data!.Title, Is.EqualTo("Movie Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("Movie Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2022, 3, 4)));
                Assert.That(result.Data!.Duration, Is.EqualTo(123));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(0f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(null));
                Assert.That(result.Data!.MyRating, Is.EqualTo(null));
                Assert.That(result.Data!.EndDate, Is.EqualTo(null));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(null));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(0));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(null));
                Assert.That(result.Data!.Season, Is.EqualTo(null));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        [Test]
        public async Task Handle_ShouldReturnMovieDto_WithCorrectDataWithUserRatingsWithoutMyRatingAndReviews() 
        {
            //Arrange
            this.movie.UserReviews = null!;

            SetUpReturningShow(this.movie);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Comedy", "Romance", "Drama"];
            IEnumerable<string> expectedLanguages = ["English", "Spanish", "Russian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Bulgaria", "Spain", "China"];
            IEnumerable<string> expectedFilmingLocations = ["Malibu", "Malta", "NDK"];


            ShowDetailsQuery query = new()
            {
                ShowId = "70752D2F-C3AD-46D1-94BD-24610CA60529",
                UserId = "9F99ECF9-346D-456B-B019-16A0B3200616"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("70752D2F-C3AD-46D1-94BD-24610CA60529"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.Movie));
                Assert.That(result.Data!.Title, Is.EqualTo("Movie Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("Movie Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2022, 3, 4)));
                Assert.That(result.Data!.Duration, Is.EqualTo(123));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(7.3f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(null));
                Assert.That(result.Data!.MyRating, Is.EqualTo(null));
                Assert.That(result.Data!.EndDate, Is.EqualTo(null));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(null));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(3));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(null));
                Assert.That(result.Data!.Season, Is.EqualTo(null));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        [Test]
        public async Task Handle_ShouldReturnMovieDto_WithCorrectDataWithUserRatingsAndMyRatingButWithoutReviews() 
        {
            //Arrange
            this.movie.UserReviews = null!;

            SetUpReturningShow(this.movie);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Comedy", "Romance", "Drama"];
            IEnumerable<string> expectedLanguages = ["English", "Spanish", "Russian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Bulgaria", "Spain", "China"];
            IEnumerable<string> expectedFilmingLocations = ["Malibu", "Malta", "NDK"];


            ShowDetailsQuery query = new()
            {
                ShowId = "70752D2F-C3AD-46D1-94BD-24610CA60529",
                UserId = "F041A629-BD68-481D-8355-026B3F98FB69"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("70752D2F-C3AD-46D1-94BD-24610CA60529"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.Movie));
                Assert.That(result.Data!.Title, Is.EqualTo("Movie Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("Movie Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2022, 3, 4)));
                Assert.That(result.Data!.Duration, Is.EqualTo(123));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(7.3f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(null));
                Assert.That(result.Data!.MyRating, Is.EqualTo(9));
                Assert.That(result.Data!.EndDate, Is.EqualTo(null));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(null));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(3));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(null));
                Assert.That(result.Data!.Season, Is.EqualTo(null));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        [Test]
        public async Task Handle_ShouldReturnMovieDto_WithCorrectDataWithUserRatingsAndMyRatingAndWithReviewsButIsNotMine()  
        {
            //Arrange
            SetUpReturningShow(this.movie);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Comedy", "Romance", "Drama"];
            IEnumerable<string> expectedLanguages = ["English", "Spanish", "Russian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Bulgaria", "Spain", "China"];
            IEnumerable<string> expectedFilmingLocations = ["Malibu", "Malta", "NDK"];
            LastReviewDto expectedLastReviewDto = new()
            {
                ReviewId = "BD78A7F8-E1B3-413C-8419-0E147E076341",
                Heading = "Review Heading3",
                Content = "Review Content3",
                CreatedAt = new DateTime(2022, 7, 6),
                IsMine = false
            };


            ShowDetailsQuery query = new()
            {
                ShowId = "70752D2F-C3AD-46D1-94BD-24610CA60529",
                UserId = "F041A629-BD68-481D-8355-026B3F98FB69"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("70752D2F-C3AD-46D1-94BD-24610CA60529"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.Movie));
                Assert.That(result.Data!.Title, Is.EqualTo("Movie Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("Movie Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2022, 3, 4)));
                Assert.That(result.Data!.Duration, Is.EqualTo(123));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(7.3f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(expectedLastReviewDto).Using(new LastReviewDtoComparer()));
                Assert.That(result.Data!.MyRating, Is.EqualTo(9));
                Assert.That(result.Data!.EndDate, Is.EqualTo(null));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(null));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(3));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(null));
                Assert.That(result.Data!.Season, Is.EqualTo(null));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        [Test]
        public async Task Handle_ShouldReturnMovieDto_WithCorrectDataWithUserRatingsAndMyRatingAndWithReviewsAndIsMine()  
        {
            //Arrange
            SetUpReturningShow(this.movie);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Comedy", "Romance", "Drama"];
            IEnumerable<string> expectedLanguages = ["English", "Spanish", "Russian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Bulgaria", "Spain", "China"];
            IEnumerable<string> expectedFilmingLocations = ["Malibu", "Malta", "NDK"];
            LastReviewDto expectedLastReviewDto = new()
            {
                ReviewId = "BD78A7F8-E1B3-413C-8419-0E147E076341",
                Heading = "Review Heading3",
                Content = "Review Content3",
                CreatedAt = new DateTime(2022, 7, 6),
                IsMine = true
            };


            ShowDetailsQuery query = new()
            {
                ShowId = "70752D2F-C3AD-46D1-94BD-24610CA60529",
                UserId = "94A36B0B-CBE3-4EE4-BCD4-C39F10051193"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("70752D2F-C3AD-46D1-94BD-24610CA60529"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.Movie));
                Assert.That(result.Data!.Title, Is.EqualTo("Movie Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("Movie Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2022, 3, 4)));
                Assert.That(result.Data!.Duration, Is.EqualTo(123));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(7.3f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(expectedLastReviewDto).Using(new LastReviewDtoComparer()));
                Assert.That(result.Data!.MyRating, Is.EqualTo(8));
                Assert.That(result.Data!.EndDate, Is.EqualTo(null));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(null));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(3));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(null));
                Assert.That(result.Data!.Season, Is.EqualTo(null));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        //TV Series
        [Test]
        public async Task Handle_ShouldReturnTVSeriesDto_WithCorrectData() 
        {
            //Arrange
            this.tvSeries.UserRatings = null!;
            this.tvSeries.UserReviews = null!;

            SetUpReturningShow(this.tvSeries);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Action", "Sci-fi", "Fantasy"];
            IEnumerable<string> expectedLanguages = ["Mandarin", "French", "Romanian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Argentina", "USA", "UK"];
            IEnumerable<string> expectedFilmingLocations = ["LA", "Hogwarts", "Dallas"];


            ShowDetailsQuery query = new()
            {
                ShowId = "0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6",
                UserId = "C4E9C8C3-773C-4578-834B-D1295B600653"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.TVSeries));
                Assert.That(result.Data!.Title, Is.EqualTo("TV Series Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("TV Series Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2020, 1, 2)));
                Assert.That(result.Data!.Duration, Is.EqualTo(22));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(0f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(null));
                Assert.That(result.Data!.MyRating, Is.EqualTo(null));
                Assert.That(result.Data!.EndDate, Is.EqualTo(new DateTime(2021, 2, 3)));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(null));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(0));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(null));
                Assert.That(result.Data!.Season, Is.EqualTo(null));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        [Test]
        public async Task Handle_ShouldReturnTVSeriesDto_WithCorrectDataWithUserRatingsWithoutMyRatingAndReviews() 
        {
            //Arrange
            this.tvSeries.UserReviews = null!;

            SetUpReturningShow(this.tvSeries);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Action", "Sci-fi", "Fantasy"];
            IEnumerable<string> expectedLanguages = ["Mandarin", "French", "Romanian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Argentina", "USA", "UK"];
            IEnumerable<string> expectedFilmingLocations = ["LA", "Hogwarts", "Dallas"];


            ShowDetailsQuery query = new()
            {
                ShowId = "0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6",
                UserId = "4BD68700-7030-4D37-B74C-9DE086C8482A"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.TVSeries));
                Assert.That(result.Data!.Title, Is.EqualTo("TV Series Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("TV Series Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2020, 1, 2)));
                Assert.That(result.Data!.Duration, Is.EqualTo(22));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(7.3f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(null));
                Assert.That(result.Data!.MyRating, Is.EqualTo(null));
                Assert.That(result.Data!.EndDate, Is.EqualTo(new DateTime(2021, 2, 3)));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(null));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(3));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(null));
                Assert.That(result.Data!.Season, Is.EqualTo(null));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        [Test]
        public async Task Handle_ShouldReturnTVSeriesDto_WithCorrectDataWithUserRatingsAndMyRatingButWithoutReviews() 
        {
            //Arrange
            this.tvSeries.UserReviews = null!;

            SetUpReturningShow(this.tvSeries);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Action", "Sci-fi", "Fantasy"];
            IEnumerable<string> expectedLanguages = ["Mandarin", "French", "Romanian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Argentina", "USA", "UK"];
            IEnumerable<string> expectedFilmingLocations = ["LA", "Hogwarts", "Dallas"];


            ShowDetailsQuery query = new()
            {
                ShowId = "0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6",
                UserId = "F041A629-BD68-481D-8355-026B3F98FB69"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.TVSeries));
                Assert.That(result.Data!.Title, Is.EqualTo("TV Series Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("TV Series Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2020, 1, 2)));
                Assert.That(result.Data!.Duration, Is.EqualTo(22));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(7.3f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(null));
                Assert.That(result.Data!.MyRating, Is.EqualTo(9));
                Assert.That(result.Data!.EndDate, Is.EqualTo(new DateTime(2021, 2, 3)));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(null));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(3));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(null));
                Assert.That(result.Data!.Season, Is.EqualTo(null));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        [Test]
        public async Task Handle_ShouldReturnTVSeriesDto_WithCorrectDataWithUserRatingsAndMyRatingAndWithReviewsButIsNotMine() 
        {
            //Arrange
            SetUpReturningShow(this.tvSeries);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Action", "Sci-fi", "Fantasy"];
            IEnumerable<string> expectedLanguages = ["Mandarin", "French", "Romanian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Argentina", "USA", "UK"];
            IEnumerable<string> expectedFilmingLocations = ["LA", "Hogwarts", "Dallas"];
            LastReviewDto expectedLastReviewDto = new()
            {
                ReviewId = "BD78A7F8-E1B3-413C-8419-0E147E076341",
                Heading = "Review Heading3",
                Content = "Review Content3",
                CreatedAt = new DateTime(2022, 7, 6),
                IsMine = false
            };

            ShowDetailsQuery query = new()
            {
                ShowId = "0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6",
                UserId = "F041A629-BD68-481D-8355-026B3F98FB69"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.TVSeries));
                Assert.That(result.Data!.Title, Is.EqualTo("TV Series Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("TV Series Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2020, 1, 2)));
                Assert.That(result.Data!.Duration, Is.EqualTo(22));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(7.3f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(expectedLastReviewDto).Using(new LastReviewDtoComparer()));
                Assert.That(result.Data!.MyRating, Is.EqualTo(9));
                Assert.That(result.Data!.EndDate, Is.EqualTo(new DateTime(2021, 2, 3)));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(null));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(3));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(null));
                Assert.That(result.Data!.Season, Is.EqualTo(null));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        [Test]
        public async Task Handle_ShouldReturnTVSeriesDto_WithCorrectDataWithUserRatingsAndMyRatingAndWithReviewsAndIsMine() 
        {
            //Arrange
            SetUpReturningShow(this.tvSeries);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Action", "Sci-fi", "Fantasy"];
            IEnumerable<string> expectedLanguages = ["Mandarin", "French", "Romanian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Argentina", "USA", "UK"];
            IEnumerable<string> expectedFilmingLocations = ["LA", "Hogwarts", "Dallas"];
            LastReviewDto expectedLastReviewDto = new()
            {
                ReviewId = "BD78A7F8-E1B3-413C-8419-0E147E076341",
                Heading = "Review Heading3",
                Content = "Review Content3",
                CreatedAt = new DateTime(2022, 7, 6),
                IsMine = true
            };

            ShowDetailsQuery query = new()
            {
                ShowId = "0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6",
                UserId = "94A36B0B-CBE3-4EE4-BCD4-C39F10051193"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.TVSeries));
                Assert.That(result.Data!.Title, Is.EqualTo("TV Series Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("TV Series Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2020, 1, 2)));
                Assert.That(result.Data!.Duration, Is.EqualTo(22));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(7.3f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(expectedLastReviewDto).Using(new LastReviewDtoComparer()));
                Assert.That(result.Data!.MyRating, Is.EqualTo(5));
                Assert.That(result.Data!.EndDate, Is.EqualTo(new DateTime(2021, 2, 3)));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(null));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(3));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(null));
                Assert.That(result.Data!.Season, Is.EqualTo(null));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        //Episode
        [Test]
        public async Task Handle_ShouldReturnEpisodeDto_WithCorrectData() 
        {
            //Arrange

            this.episode.UserRatings = null!;
            this.episode.UserReviews = null!;

            SetUpReturningShow(this.episode);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Action", "Sci-fi", "Fantasy"];
            IEnumerable<string> expectedLanguages = ["Mandarin", "French", "Romanian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Argentina", "USA", "UK"];
            IEnumerable<string> expectedFilmingLocations = ["LA", "Hogwarts", "Dallas"];
            TVSeriesDto expectedTVSeries = new()
            {
                Id = "0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6",
                Title = "TV Series Title"
            };

            ShowDetailsQuery query = new()
            {
                ShowId = "6019FFB7-6E56-4AD8-8149-5B7541021C48",
                UserId = "C4E9C8C3-773C-4578-834B-D1295B600653"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("6019FFB7-6E56-4AD8-8149-5B7541021C48"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.Episode));
                Assert.That(result.Data!.Title, Is.EqualTo("Episode Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("Episode Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2021, 1, 1)));
                Assert.That(result.Data!.Duration, Is.EqualTo(22));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(0f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(null));
                Assert.That(result.Data!.MyRating, Is.EqualTo(null));
                Assert.That(result.Data!.EndDate, Is.EqualTo(null));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(3));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(0));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(expectedTVSeries).Using(new TVSeriesDtoComparer()));
                Assert.That(result.Data!.Season, Is.EqualTo(1));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        [Test]
        public async Task Handle_ShouldReturnEpisodeDto_WithCorrectDataWithUserRatingsWithoutMyRatingAndReviews() 
        {
            //Arrange
            this.episode.UserReviews = null!;

            SetUpReturningShow(this.episode);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Action", "Sci-fi", "Fantasy"];
            IEnumerable<string> expectedLanguages = ["Mandarin", "French", "Romanian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Argentina", "USA", "UK"];
            IEnumerable<string> expectedFilmingLocations = ["LA", "Hogwarts", "Dallas"];
            TVSeriesDto expectedTVSeries = new()
            {
                Id = "0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6",
                Title = "TV Series Title"
            };

            ShowDetailsQuery query = new()
            {
                ShowId = "6019FFB7-6E56-4AD8-8149-5B7541021C48",
                UserId = "6FAE8217-170C-4B2C-AB5F-559A87688A59"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("6019FFB7-6E56-4AD8-8149-5B7541021C48"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.Episode));
                Assert.That(result.Data!.Title, Is.EqualTo("Episode Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("Episode Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2021, 1, 1)));
                Assert.That(result.Data!.Duration, Is.EqualTo(22));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(7.3f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(null));
                Assert.That(result.Data!.MyRating, Is.EqualTo(null));
                Assert.That(result.Data!.EndDate, Is.EqualTo(null));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(3));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(3));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(expectedTVSeries).Using(new TVSeriesDtoComparer()));
                Assert.That(result.Data!.Season, Is.EqualTo(1));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        [Test]
        public async Task Handle_ShouldReturnEpisodeDto_WithCorrectDataWithUserRatingsAndMyRatingButWithoutReviews() 
        {
            //Arrange
            this.episode.UserReviews = null!;

            SetUpReturningShow(this.episode);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Action", "Sci-fi", "Fantasy"];
            IEnumerable<string> expectedLanguages = ["Mandarin", "French", "Romanian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Argentina", "USA", "UK"];
            IEnumerable<string> expectedFilmingLocations = ["LA", "Hogwarts", "Dallas"];
            TVSeriesDto expectedTVSeries = new()
            {
                Id = "0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6",
                Title = "TV Series Title"
            };

            ShowDetailsQuery query = new()
            {
                ShowId = "6019FFB7-6E56-4AD8-8149-5B7541021C48",
                UserId = "084CC46F-5456-4686-8067-9C919969A46E"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("6019FFB7-6E56-4AD8-8149-5B7541021C48"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.Episode));
                Assert.That(result.Data!.Title, Is.EqualTo("Episode Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("Episode Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2021, 1, 1)));
                Assert.That(result.Data!.Duration, Is.EqualTo(22));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(7.3f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(null));
                Assert.That(result.Data!.MyRating, Is.EqualTo(5));
                Assert.That(result.Data!.EndDate, Is.EqualTo(null));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(3));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(3));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(expectedTVSeries).Using(new TVSeriesDtoComparer()));
                Assert.That(result.Data!.Season, Is.EqualTo(1));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        [Test]
        public async Task Handle_ShouldReturnEpisodeDto_WithCorrectDataWithUserRatingsAndMyRatingAndWithReviewsButIsNotMine() 
        {
            //Arrange
            SetUpReturningShow(this.episode);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Action", "Sci-fi", "Fantasy"];
            IEnumerable<string> expectedLanguages = ["Mandarin", "French", "Romanian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Argentina", "USA", "UK"];
            IEnumerable<string> expectedFilmingLocations = ["LA", "Hogwarts", "Dallas"];
            TVSeriesDto expectedTVSeries = new()
            {
                Id = "0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6",
                Title = "TV Series Title"
            };

            LastReviewDto expectedLastReviewDto = new()
            {
                ReviewId = "BD78A7F8-E1B3-413C-8419-0E147E076341",
                Heading = "Review Heading3",
                Content = "Review Content3",
                CreatedAt = new DateTime(2022, 7, 6),
                IsMine = false
            };

            ShowDetailsQuery query = new()
            {
                ShowId = "6019FFB7-6E56-4AD8-8149-5B7541021C48",
                UserId = "084CC46F-5456-4686-8067-9C919969A46E"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("6019FFB7-6E56-4AD8-8149-5B7541021C48"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.Episode));
                Assert.That(result.Data!.Title, Is.EqualTo("Episode Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("Episode Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2021, 1, 1)));
                Assert.That(result.Data!.Duration, Is.EqualTo(22));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(7.3f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(expectedLastReviewDto).Using(new LastReviewDtoComparer()));
                Assert.That(result.Data!.MyRating, Is.EqualTo(5));
                Assert.That(result.Data!.EndDate, Is.EqualTo(null));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(3));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(3));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(expectedTVSeries).Using(new TVSeriesDtoComparer()));
                Assert.That(result.Data!.Season, Is.EqualTo(1));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        [Test]
        public async Task Handle_ShouldReturnEpisodeDto_WithCorrectDataWithUserRatingsAndMyRatingAndWithReviewsAndIsMine() 
        {
            //Arrange
            SetUpReturningShow(this.episode);
            SetUpCheckingUser(true);

            IEnumerable<string> expectedGenres = ["Action", "Sci-fi", "Fantasy"];
            IEnumerable<string> expectedLanguages = ["Mandarin", "French", "Romanian"];
            IEnumerable<string> expectedCountriesOfOrigin = ["Argentina", "USA", "UK"];
            IEnumerable<string> expectedFilmingLocations = ["LA", "Hogwarts", "Dallas"];
            TVSeriesDto expectedTVSeries = new()
            {
                Id = "0EDA20D2-20A1-44A9-9BE2-C411E90A5EC6",
                Title = "TV Series Title"
            };

            LastReviewDto expectedLastReviewDto = new()
            {
                ReviewId = "BD78A7F8-E1B3-413C-8419-0E147E076341",
                Heading = "Review Heading3",
                Content = "Review Content3",
                CreatedAt = new DateTime(2022, 7, 6),
                IsMine = true
            };

            ShowDetailsQuery query = new()
            {
                ShowId = "6019FFB7-6E56-4AD8-8149-5B7541021C48",
                UserId = "94A36B0B-CBE3-4EE4-BCD4-C39F10051193"
            };

            //Act
            Result<ShowDetailsDto> result = await this.handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Multiple(() => {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.Data!.ShowId, Is.EqualTo("6019FFB7-6E56-4AD8-8149-5B7541021C48"));
                Assert.That(result.Data!.ShowType, Is.EqualTo(ShowType.Episode));
                Assert.That(result.Data!.Title, Is.EqualTo("Episode Title"));
                Assert.That(result.Data!.Description, Is.EqualTo("Episode Description"));
                Assert.That(result.Data!.ReleaseDate, Is.EqualTo(new DateTime(2021, 1, 1)));
                Assert.That(result.Data!.Duration, Is.EqualTo(22));
                Assert.That(result.Data!.PhotoUrl, Is.EqualTo("photoUrl"));
                Assert.That(result.Data!.AverageRating, Is.EqualTo(7.3f));
                Assert.That(result.Data!.LastReview, Is.EqualTo(expectedLastReviewDto).Using(new LastReviewDtoComparer()));
                Assert.That(result.Data!.MyRating, Is.EqualTo(8));
                Assert.That(result.Data!.EndDate, Is.EqualTo(null));
                Assert.That(result.Data!.EpisodeNumber, Is.EqualTo(3));
                Assert.That(result.Data!.NumberOfRatings, Is.EqualTo(3));
                Assert.That(result.Data!.TVSeries, Is.EqualTo(expectedTVSeries).Using(new TVSeriesDtoComparer()));
                Assert.That(result.Data!.Season, Is.EqualTo(1));
                CollectionAssert.AreEqual(expectedGenres, result.Data!.Genres);
                CollectionAssert.AreEqual(expectedLanguages, result.Data!.Languages);
                CollectionAssert.AreEqual(expectedCountriesOfOrigin, result.Data!.CountriesOfOrigin);
                CollectionAssert.AreEqual(expectedFilmingLocations, result.Data!.FilmingLocations);
            });
        }

        private void SetUpReturningShow(Show? show)
        {
            IQueryable<Show> shows = new List<Show> { show! }.AsQueryable();

            TestAsyncEnumerableEfCore<Show> queryable = new(shows);

            this.repositoryMock
                .Setup(r => r.All(It.IsAny<Expression<Func<Show, bool>>>()))
                .Returns(queryable);
        }

        private void SetUpCheckingUser(bool value)
        {
            this.repositoryMock
                .Setup(r => r.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(value);
        }
    }
}