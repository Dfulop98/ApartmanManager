using Moq;
using NUnit.Framework;
using ServiceLayer.Factories;
using ServiceLayer.Factories.Interfaces;
using ServiceLayer.Services;
using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using System.Collections.Generic;

namespace ServiceLayer.Tests
{
    public class RoomServiceTests
    {
        private Mock<IRoomDataAccess> _mockRoomDataAccess;
        private Mock<IResponseModelFactory<Room>> _mockResponseModel;
        private RoomService _roomService;

        [SetUp]
        public void Setup()
        {
            _mockRoomDataAccess = new Mock<IRoomDataAccess>();
            _mockResponseModel = new Mock<IResponseModelFactory<Room>>();
            _roomService = new RoomService(_mockRoomDataAccess.Object, _mockResponseModel.Object);
        }

        // GetRooms tests
        [Test]
        public void GetRooms_RoomsExist_ReturnsSuccess()
        {
            // Arrange
            _mockRoomDataAccess.Setup(x => x.CheckRooms()).Returns(true);
            _mockRoomDataAccess.Setup(x => x.GetRooms()).Returns(new List<Room> { new Room() });

            // Act
            var result = _roomService.GetRooms();

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("Success", "The rooms successfully returned.", It.IsAny<List<Room>>()), Times.Once);
        }

        [Test]
        public void GetRooms_RoomsDoNotExist_ReturnsNotFound()
        {
            // Arrange
            _mockRoomDataAccess.Setup(x => x.CheckRooms()).Returns(false);

            // Act
            var result = _roomService.GetRooms();

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("NotFound", "Rooms is doesnt exists."), Times.Once);
        }

        // GetRoom tests
        [Test]
        public void GetRoom_RoomExists_ReturnsSuccess()
        {
            // Arrange
            int roomId = 1;
            _mockRoomDataAccess.Setup(x => x.CheckRoom(roomId)).Returns(true);
            _mockRoomDataAccess.Setup(x => x.GetRoom(roomId)).Returns(new Room { Id = roomId });

            // Act
            var result = _roomService.GetRoom(roomId);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("Success", "The room successfully returned.", It.IsAny<Room>()), Times.Once);
        }

        [Test]
        public void GetRoom_RoomDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int roomId = 1;
            _mockRoomDataAccess.Setup(x => x.CheckRoom(roomId)).Returns(false);

            // Act
            var result = _roomService.GetRoom(roomId);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("NotFound", "The room is doesnt exists."), Times.Once);
        }

        // AddRoom tests
        [Test]
        public void AddRoom_RoomExists_ReturnsConflict()
        {
            // Arrange
            Room room = new Room { Id = 1 };
            _mockRoomDataAccess.Setup(x => x.CheckRoom(room.Id)).Returns(true);

            // Act
            var result = _roomService.AddRoom(room);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("Conflict", "The room already exists"), Times.Once);
        }

        [Test]
        public void AddRoom_RoomDoesNotExist_ReturnsCreated()
        {
            // Arrange
            Room room = new Room { Id = 1 };
            _mockRoomDataAccess.Setup(x => x.CheckRoom(room.Id)).Returns(false);

            // Act
            var result = _roomService.AddRoom(room);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("Created", "The room successfully added."), Times.Once);
        }

        // UpdateRoom tests
        [Test]
        public void UpdateRoom_RoomExists_ReturnsUpdated()
        {
            // Arrange
            Room room = new Room { Id = 1 };
            _mockRoomDataAccess.Setup(x => x.CheckRoom(room.Id)).Returns(true);

            // Act
            var result = _roomService.UpdateRoom(room);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("Updated", "The room successfully updated."), Times.Once);
        }

        [Test]
        public void UpdateRoom_RoomDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            Room room = new Room { Id = 1 };
            _mockRoomDataAccess.Setup(x => x.CheckRoom(room.Id)).Returns(false);

            // Act
            var result = _roomService.UpdateRoom(room);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("NotFound", "The room doesnt exists."), Times.Once);
        }

        // RemoveRoom tests
        [Test]
        public void RemoveRoom_RoomExists_ReturnsOK()
        {
            // Arrange
            int roomId = 1;
            _mockRoomDataAccess.Setup(x => x.CheckRoom(roomId)).Returns(true);

            // Act
            var result = _roomService.RemoveRoom(roomId);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("OK", "Room successfully deleted."), Times.Once);
        }

        [Test]
        public void RemoveRoom_RoomDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int roomId = 1;
            _mockRoomDataAccess.Setup(x => x.CheckRoom(roomId)).Returns(false);

            // Act
            var result = _roomService.RemoveRoom(roomId);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("NotFound", "The room is doesnt exists."), Times.Once);
        }

    }
}