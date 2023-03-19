using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using DTOLayer.Models;
using Moq;
using ServiceLayer.Factories.Interfaces;
using ServiceLayer.ServiceInterfaces;
using ServiceLayer.Services;

namespace ServiceLayer.Tests
{
    [TestFixture]
    public class GuestServiceTests
    {
        private Mock<IGenericDataAccess<Guest>> _mockGuestContext;
        private Mock<IResponseModelFactory> _mockResponseModel;
        private IGuestService _guestService;

        [SetUp]
        public void Setup()
        {
            _mockGuestContext = new Mock<IGenericDataAccess<Guest>>();
            _mockResponseModel = new Mock<IResponseModelFactory>();
            _guestService = new GuestService(_mockGuestContext.Object, _mockResponseModel.Object);
        }

        [Test]
        public void GetGuest_GuestExists_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            _mockGuestContext.Setup(x => x.CheckEntity(id)).Returns(true);
            _mockGuestContext.Setup(x => x.GetEntity(id)).Returns(new Guest { Id = id });

            // Act
            var result = _guestService.GetGuest(id);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("Ok", "The guest returned.", It.IsAny<UniversalDTO>()), Times.Once);
        }

        [Test]
        public void GetGuest_GuestDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int id = 1;
            _mockGuestContext.Setup(x => x.CheckEntity(id)).Returns(false);

            // Act
            var result = _guestService.GetGuest(id);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("NotFound", "The Guest doesnt exists."), Times.Once);
        }

        [Test]
        public void AddGuest_GuestDoesNotExist_ReturnsSuccess()
        {
            // Arrange
            Guest guest = new Guest { Id = 1 };
            _mockGuestContext.Setup(x => x.CheckEntity(guest.Id)).Returns(false);

            // Act
            var result = _guestService.AddGuest(guest);

            // Assert
            _mockGuestContext.Verify(x => x.AddEntity(guest), Times.Once);
            _mockResponseModel.Verify(x => x.CreateResponseModel("Created", "The Guest successfully added."), Times.Once);
        }

        [Test]
        public void AddGuest_GuestExists_ReturnsConflict()
        {
            // Arrange
            Guest guest = new Guest { Id = 1 };
            _mockGuestContext.Setup(x => x.CheckEntity(guest.Id)).Returns(true);

            // Act
            var result = _guestService.AddGuest(guest);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("NotFound", "The Guest already exists."), Times.Once);
        }

        [Test]
        public void UpdateGuest_GuestExists_ReturnsSuccess()
        {
            // Arrange
            Guest guest = new Guest { Id = 1 };
            _mockGuestContext.Setup(x => x.CheckEntity(guest.Id)).Returns(true);

            // Act
            var result = _guestService.UpdateGuest(guest);

            // Assert
            _mockGuestContext.Verify(x => x.UpdateEntity(guest), Times.Once);
            _mockResponseModel.Verify(x => x.CreateResponseModel("Ok", "The Guest successfully updated."), Times.Once);
        }

        [Test]
        public void UpdateGuest_GuestDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            Guest guest = new Guest { Id = 1 };
            _mockGuestContext.Setup(x => x.CheckEntity(guest.Id)).Returns(false);

            // Act
            var result = _guestService.UpdateGuest(guest);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("NotFound", "The guest doesnt exists."), Times.Once);
        }

        [Test]
        public void RemoveGuest_GuestExists_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            _mockGuestContext.Setup(x => x.CheckEntity(id)).Returns(true);

            // Act
            var result = _guestService.RemoveGuest(id);

            // Assert
            _mockGuestContext.Verify(x => x.RemoveEntity(id), Times.Once);
            _mockResponseModel.Verify(x => x.CreateResponseModel("Ok", "The Guest successfully removed."), Times.Once);
        }

        [Test]
        public void RemoveGuest_GuestDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int id = 1;
            _mockGuestContext.Setup(x => x.CheckEntity(id)).Returns(false);

            // Act
            var result = _guestService.RemoveGuest(id);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("NotFound", "The guest doesnt exists."), Times.Once);
        }
    }
}
