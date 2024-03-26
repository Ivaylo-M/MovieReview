namespace Tests.Photos
{
    using System.Linq.Expressions;
    
    using Moq;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using CloudinaryDotNet.Actions;
    
    using Domain;
    using Persistence.Repositories;
    using Application.Response;
    using Application.Services.Contracts;
    
    using static Application.Photos.AddShowPhoto;
    using Domain.Enums;

    [TestFixture]
    public class AddShowPhotoTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<IPhotoService> photoServiceMock;
        private AddShowPhotoHandler handler;
        private Show show;

        [SetUp]
        public void Setup()
        {
            this.photoServiceMock = new Mock<IPhotoService>();
            this.repositoryMock = new Mock<IRepository>();
            this.handler = new AddShowPhotoHandler(this.photoServiceMock.Object, this.repositoryMock.Object);
            this.show = new Show
            {
                ShowType = ShowType.Movie,
                Title = "Test",
                Duration = 120,
                ReleaseDate = new DateTime(2022, 3, 25)
            };
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenShowPhotoIsNull()
        {
            //Arrange and Act
            Result<Unit> result = await this.handler
                .Handle(new AddShowPhotoCommand(), CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("File is not selected or empty"));
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenFileLengthIsZero()
        {
            //Arrange
            Mock<IFormFile> fileMock = new Mock<IFormFile>();

            AddShowPhotoCommand command = new AddShowPhotoCommand
            {
                File = fileMock.Object,
                ShowId = "test id"
            };

            //Act
            Result<Unit> result = await this.handler
                .Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("File is not selected or empty"));
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenShowDoesNotExist()
        {
            //Arrange
            AddShowPhotoCommand command = SetUpReturningPhoto();
            SetUpReturningShow(null!);

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This show does not exist! Please select an existing one"));
        }

        [Test]
        public async Task Handle_ShouldReturnError_WhenShowAlreadyHavePhoto()
        {
            //Arrange
            this.show.PhotoId = "id";

            AddShowPhotoCommand command = SetUpReturningPhoto();
            SetUpReturningShow(this.show);

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("This show already have photo"));
        }

        [Test]
        public async Task Handle_ShouldAddPhoto_WhenGivingCorrectData()
        { 
            //Arrange
            AddShowPhotoCommand command = SetUpReturningPhoto();
            SetUpReturningShow(this.show);

            this.photoServiceMock
                .Setup(ps => ps.AddPhotoAsync(command.File))
                .ReturnsAsync(Result<ImageUploadResult>.Success(new ImageUploadResult
                {
                    PublicId = "public-id",
                    Url = new Uri("https://example.com/image.jpg")
                }));

            //Act
            Result<Unit> result = await this.handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.That(result.SuccessMessage, Is.EqualTo("Successfully uploaded photo"));
        } 

        private void SetUpReturningShow(Show show)
        {
            this.repositoryMock
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<Show, bool>>>()))
                .ReturnsAsync(show);
        }

        private AddShowPhotoCommand SetUpReturningPhoto()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();

            fileMock.Setup(f => f.Length).Returns(2);

            AddShowPhotoCommand command = new AddShowPhotoCommand
            {
                File = fileMock.Object,
                ShowId = Guid.NewGuid().ToString()
            };

            return command;
        }
    }
}
