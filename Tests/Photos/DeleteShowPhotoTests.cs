namespace Tests.Photos
{
    using System.Net;
    using System.Linq.Expressions;
    
    using MediatR;
    using Moq;
    using CloudinaryDotNet.Actions;
    
    using Domain;
    using Persistence.Repositories;
    using Application.Response;
    using Application.Services.Contracts;
    
    using static Application.Photos.DeleteShowPhoto;
    using Domain.Enums;

    [TestFixture]
    public class DeleteShowPhotoTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<IPhotoService> photoServiceMock;
        private DeleteShowPhotoHandler handler;
        private Show show;
        private Photo photo;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IRepository>();
            this.photoServiceMock = new Mock<IPhotoService>();
            this.handler = new DeleteShowPhotoHandler(this.repositoryMock.Object, this.photoServiceMock.Object);
            this.show = new Show
            {
                ShowId = Guid.NewGuid(),
                ShowType = ShowType.Movie,
                Title = "Test",
                Duration = 120,
                ReleaseDate = new DateTime(2022, 3, 25),
                PhotoId = "id"
            };
            this.photo = new Photo
            {
                PhotoId = "id",
                Url = "url"
            };
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenShowDoesNotExist()
        {
            //Arrange
            SetUpReturningShow(null);

            DeleteShowPhotoCommand command = new();

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
        public async Task Handle_ShouldReturnError_WhenPhotoDoesNotExist()
        {
            //Arrange
            this.show.PhotoId = "id1";
            SetUpReturningShow(this.show);
            SetUpReturningPhoto(null);

            DeleteShowPhotoCommand command = new()
            {
                ShowId = this.show.ShowId.ToString()
            };

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("Photo does not exist"));
            });
        }

        [Test]
        public async Task Handle_ShouldReturnError_IfShowDontHaveAPhotoYet()
        {
            //Arrange
            this.show.PhotoId = null;
            SetUpReturningShow(this.show);
            SetUpReturningPhoto(this.photo);

            DeleteShowPhotoCommand command = new()
            {
                ShowId = this.show.ShowId.ToString()
            };

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo("This show doesn't have a photo yet"));
            });
        }

        [Test]
        public async Task Handle_ShouldDeleteShowPhoto_WhenGivingCorrectData()
        {
            //Arrange
            SetUpReturningShow(this.show);
            SetUpReturningPhoto(this.photo);

            this.photoServiceMock
                .Setup(ps => ps.DeletePhotoAsync(this.photo))
                .ReturnsAsync(Result<DeletionResult>.Success(new DeletionResult
                {
                    Result = "delete result",
                    StatusCode = HttpStatusCode.OK
                }));

            DeleteShowPhotoCommand command = new()
            {
                ShowId = this.show.ShowId.ToString()
            };

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(result.SuccessMessage, Is.EqualTo("Successfully deleted photo"));
            });
        }

        private void SetUpReturningPhoto(Photo? photo)
        {
            this.repositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Photo, bool>>>()))
                .ReturnsAsync(photo);
        }

        private void SetUpReturningShow(Show? show)
        {
            this.repositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Show, bool>>>()))
                .ReturnsAsync(show);
        }
    }
}
