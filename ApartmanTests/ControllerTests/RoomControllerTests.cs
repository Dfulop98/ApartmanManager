using ApartmanManagerApi.Controllers;
using DataAccessLayer.DbAccess;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ServiceLayer.ServiceInterfaces;
using ServiceLayer.Services;
using Xunit;

namespace ApartmanTests.ControllerTests
{
    public class RoomControllerTests
    {
        private readonly Mock<IRoomService> _roomServiceMock;
        private readonly RoomController _controller;

        private readonly List<Room> _expectedRooms = new()
        { 
            new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = true },
            new Room { Id = 2, RoomNumber = "102", Description="test1", IsAvailable = false },
            new Room { Id = 3, RoomNumber = "103", Description="test1", IsAvailable = true }};

        public RoomControllerTests()
        {
            _roomServiceMock = new Mock<IRoomService>();
            _controller = new RoomController(_roomServiceMock.Object);
        }


        //GetAllRooms

        [Fact]
        public async Task GetAllRooms_ReturnType_OkObjectResult()
        {
            // Arrange
            _roomServiceMock.Setup(x => x.GetAllRoomAsync()).ReturnsAsync(_expectedRooms);
            // Act
            var result = await _controller.GetAllRooms();
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);

        }
        [Fact]
        public async Task GetAllRooms_ReturnAssignableFrom_IEnumerableRoom()
        {
            // Arrange
            _roomServiceMock.Setup(x => x.GetAllRoomAsync()).ReturnsAsync(_expectedRooms);
            // Act
            var result = await _controller.GetAllRooms();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<Room>>(okResult.Value);
        }

        [Fact]
        public async Task GetAllRooms_Returns_ReturnAllRooms()
        {
            // Arrange
            _roomServiceMock.Setup(x => x.GetAllRoomAsync()).ReturnsAsync(_expectedRooms);
            // Act
            var result = await _controller.GetAllRooms();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var rooms = Assert.IsAssignableFrom<IEnumerable<Room>>(okResult.Value);
            Assert.Equal(3, rooms.Count());

        }
        
        [Fact]
        public async Task GetAllRooms_Returns_ReturnCorrectDatas()
        {
            // Arrange
            _roomServiceMock.Setup(x => x.GetAllRoomAsync()).ReturnsAsync(_expectedRooms);
            // Act
            var result = await _controller.GetAllRooms();
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var rooms = Assert.IsAssignableFrom<IEnumerable<Room>>(okResult.Value);
            Assert.Equal(_expectedRooms, rooms);
        }


        //GetRoomById

        [Fact]
        public async Task GetRoomById_ReturnType_OkObjectResult()
        {
            //Arrage
            _roomServiceMock.Setup(x => x.GetRoomByIdAsync(1)).ReturnsAsync(_expectedRooms.Where(x => x.Id == 1).First());
            //Act
            var result = await _controller.GetRoomById(1);
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
            
        }
        
        [Fact]
        public async Task GetRoomById_ReturnAssignableFrom_Room()
        {
            //Arrage
            _roomServiceMock.Setup(x => x.GetRoomByIdAsync(1)).ReturnsAsync(_expectedRooms.Where(x => x.Id == 1).First());
            //Act
            var result = await _controller.GetRoomById(1);
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<Room>(okResult.Value);
        }
        
        [Fact]
        public async Task GetRoomById_Returns_ReturnCorrectRoom()
        {
            //Arrage
            _roomServiceMock.Setup(x => x.GetRoomByIdAsync(1)).ReturnsAsync(_expectedRooms.Where(x => x.Id == 1).First());
            //Act
            var result = await _controller.GetRoomById(1);
            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var room = Assert.IsAssignableFrom<Room>(okResult.Value);
            Assert.Equal(_expectedRooms.Where(x => x.Id == 1).First(), room);
        }
    }
}
