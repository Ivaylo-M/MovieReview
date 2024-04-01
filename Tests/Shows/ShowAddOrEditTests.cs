namespace Tests.Shows
{
    using Moq;
    using Persistence.Repositories;

    public class ShowAddOrEditShowTests
    {
        private Mock<IRepository> repositoryMock;

        [SetUp]
        public void Setup()
        {
            this.repositoryMock = new Mock<IRepository>();
        }
    }
}
