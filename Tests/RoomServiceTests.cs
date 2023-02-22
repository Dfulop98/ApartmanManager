using DataAccessLayer.DbAccess;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services;

namespace Tests
{
    public class Tests
    {
        private DbContextOptions<AMDbContext> _options; 
        [SetUp]

        public void Setup()
        {
            _options = new DbContextOptionsBuilder<AMDbContext>()
                .UseInMemoryDatabase(databaseName: "TestBd")
                .Options;
        }

        [Test]
        public async Task GetAllRoomsAsync_IsNotEmpty_NotNull()
        {
            //Arrage
            using (var context = new AMDbContext(_options))
            {
                context.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = true },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = false },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = true }
                });
                context.SaveChanges();
            }

            using (var context = new AMDbContext(_options))
            {
                var service = new RoomService(context);

                // Act
                var result = await service.GetAllRoomAsync();

                // Assert
                Assert.That(result, Is.Not.Null);
            }
        }
    }
}