using ApartmanTests.ServiceTests;
using DataAccessLayer.DbAccess;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApartmanTests.ControllerTests
{
    public class RoomControllerTests
    {
        [Fact]
        public void GetAllRooms_ReturnOK()
        {
            //Arrage
            var dbContextOptions = new DbContextOptionsBuilder<AMDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllRooms_ReturnsOk")
                .Options;
            using (var dbContext = new AMDbContext(dbContextOptions))
            {
                dbContext.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "1", IsAvailable= true },
                    new Room { Id = 2, RoomNumber = "2", IsAvailable= false }
                });
                dbContext.SaveChanges();
            }
            var controller = new RoomController(new RoomService(dbContextOptions));
            //act
            var result = controller.GetAllRooms();
            //assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var rooms = Assert.IsAssignableFrom<IEnumerable<Room>>(okResult.Value);
            Assert.Equal(2, rooms.Count());
        }
    }
}
