namespace Tests.Shows
{
    using Moq;
    using Persistence.Repositories;
    using static Application.Shows.AllShows;

    [TestFixture]
    public class AllShowsTests
    {
        private Mock<IRepository> repositoryMock;
        private AllShowsHandler handler;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();
            handler = new AllShowsHandler(repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturn_AllShows()
        {

        }
    }
}
