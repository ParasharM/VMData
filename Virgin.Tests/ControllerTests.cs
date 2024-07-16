using Moq;
using VirginMediaData.Controllers;
using VirginMediaData.Services;

namespace Virgin.Tests
{
    public class ControllerTests
    {
        private Mock<IDataRepository> _repositoryMock;


        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IDataRepository>();
        }

        [Test]
        public void SalesConttroller_Throws_ArgumentNullException_When_Repository_Is_Null()
        {
            //arrange
            Action sut = () => {
                _ = new SalesController(null);
            };

            //assert
            Assert.Throws<ArgumentNullException>(() => sut());
        }

        [Test]
        public void SalesConttroller_Does_Not_Throw_Exception_When_Repository_Is_Not_Null()
        {
            //arrange
            Action sut = () => {
                _ = new SalesController(_repositoryMock.Object);
            };

            //assert
            Assert.DoesNotThrow(() => sut());
        }

    }
}