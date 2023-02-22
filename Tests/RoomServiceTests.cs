using DataAccessLayer.DbAccess;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services;

namespace Tests
{
    public class Tests
    {
        private AMDbContext _db; 
        
        [SetUp]

        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AMDbContext>()
                .UseInMemoryDatabase(databaseName: "TestBd_" + DateTime.Now.ToFileTimeUtc())
                .Options;
            _db = new AMDbContext(options);
        }


        //GetAllRooms Tests
        [Test]
        public async Task GetAllRoomsAsync_IsNotEmpty_ReturnNotNull()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = true },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = false },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = true }
                });
                _db.SaveChanges();
            

                var service = new RoomService(_db);

                // Act
                var result = await service.GetAllRoomAsync();

                // Assert
                Assert.That(result, Is.Not.Null);
            }
        }
        
        [Test]
        public async Task GetAllRoomsAsync_IsEmpty_ReturnEmpty()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>{});
                _db.SaveChanges();
            
                var service = new RoomService(_db);

                // Act
                var result = await service.GetAllRoomAsync();

                // Assert
                Assert.That(result, Is.Empty);
            }
        }

        [Test]
        public async Task GetAllRoomsAsync_HaveOneRoom_ReturnOneRoom()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 4, RoomNumber = "104", Description="test4", IsAvailable = true },
                });
                _db.SaveChanges();
            
                var service = new RoomService(_db);

                // Act
                var result = await service.GetAllRoomAsync();

                // Assert
                Assert.That(result.Count(), Is.EqualTo(1));
            }
        }
        
        [Test]
        public async Task GetAllRoomsAsync_HaveFourRoom_ReturnFourRoom()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false },
                    new Room { Id = 4, RoomNumber = "104", Description="test4", IsAvailable = true },
                });
                _db.SaveChanges();
            
                var service = new RoomService(_db);

                // Act
                var result = await service.GetAllRoomAsync();

                // Assert
                Assert.That(result.Count(), Is.EqualTo(4));
            }
        }
        
        [Test]
        public async Task GetAllRoomsAsync_IsCorrectRoomId_ReturnSameRoomId()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false }
                });
                _db.SaveChanges();
                
                var testRoom = new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false }
                };


                var service = new RoomService(_db);

                // Act
                var result = await service.GetAllRoomAsync();

                // Assert
                Assert.That(result.First().Id, Is.EqualTo(testRoom.First().Id));
            }
        }
        
        [Test]
        public async Task GetAllRoomsAsync_IsCorrectRoomNumber_ReturnSameRoomNumber()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false }
                });
                _db.SaveChanges();
                
                var testRoom = new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false }
                };


                var service = new RoomService(_db);

                // Act
                var result = await service.GetAllRoomAsync();

                // Assert
                Assert.That(result.First().RoomNumber, Is.EqualTo(testRoom.First().RoomNumber));
            }
        }

        [Test]
        public async Task GetAllRoomsAsync_IsCorrectDescription_ReturnSameDescription()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false }
                });
                _db.SaveChanges();

                var testRoom = new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false }
                };


                var service = new RoomService(_db);

                // Act
                var result = await service.GetAllRoomAsync();

                // Assert
                Assert.That(result.First().Description, Is.EqualTo(testRoom.First().Description));
            }
        }

        [Test]
        public async Task GetAllRoomsAsync_IsCorrectStatus_ReturnSameStatus()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false }
                });
                _db.SaveChanges();

                var testRoom = new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false }
                };


                var service = new RoomService(_db);

                // Act
                var result = await service.GetAllRoomAsync();

                // Assert
                Assert.That(result.First().IsAvailable, Is.EqualTo(testRoom.First().IsAvailable));
            }
        }

        [Test]
        public async Task GetAllRoomsAsync_IsFirstRoomCorrect_ReturnCorrectRoom()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();
                
                var testRoom = new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                };


                var service = new RoomService(_db);

                // Act
                var result = await service.GetAllRoomAsync();

                Assert.Multiple(() =>
                {
                    // Assert
                    Assert.That(result.First().Id, Is.EqualTo(testRoom.First().Id));
                    Assert.That(result.First().RoomNumber, Is.EqualTo(testRoom.First().RoomNumber));
                    Assert.That(result.First().Description, Is.EqualTo(testRoom.First().Description));
                    Assert.That(result.First().IsAvailable, Is.EqualTo(testRoom.First().IsAvailable));
                });
            }
        }








        [Test]
        public Task GetRoomByIdAsync_EmptyRoomList_ThrowsException()
        {
            //Arrage
            using (_db)
            {
                // Arrage
                var service = new RoomService(_db);
                // Act & Assert
                Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetRoomByIdAsync(1));


            }
            return Task.CompletedTask;
        }
        
        [Test]
        public Task GetRoomByIdAsync_RoomNotExist_ThrowsException()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                // Arrage
                var service = new RoomService(_db);
                // Act & Assert
                Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetRoomByIdAsync(4));


            }
            return Task.CompletedTask;
        }

        [Test]
        public async Task GetRoomByIdAsync_RoomIdIsCorrect_ReturnCorrectRoomId()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                var expected = new Room { Id = 2, RoomNumber = "102", Description = "test2", IsAvailable = true };



                var service = new RoomService(_db);
                // Act
                var result = await service.GetRoomByIdAsync(2);
                    // Assert
                Assert.That(result.Id, Is.EqualTo(expected.Id));
                
            }
        }
        
        [Test]
        public async Task GetRoomByIdAsync_RoomNumberIsCorrect_ReturnCorrectRoomNumber()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                var expected = new Room { Id = 3, RoomNumber = "103", Description = "test3", IsAvailable = false };



                var service = new RoomService(_db);
                // Act
                var result = await service.GetRoomByIdAsync(3);
                    // Assert
                Assert.That(result.RoomNumber, Is.EqualTo(expected.RoomNumber));
                
            }
        }
        
        [Test]
        public async Task GetRoomByIdAsync_DescriptionIsCorrect_ReturnCorrectDescription()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                var expected = new Room { Id = 3, RoomNumber = "103", Description = "test3", IsAvailable = false };



                var service = new RoomService(_db);
                // Act
                var result = await service.GetRoomByIdAsync(3);
                    // Assert
                Assert.That(result.Description, Is.EqualTo(expected.Description));
                
            }
        }
        
        [Test]
        public async Task GetRoomByIdAsync_StatusIsCorrect_ReturnCorrectStatus()
        {
            //Arrage
            using (_db)
            {
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                var expected = new Room { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };



                var service = new RoomService(_db);
                // Act
                var result = await service.GetRoomByIdAsync(3);
                    // Assert
                Assert.That(result.IsAvailable, Is.EqualTo(expected.IsAvailable));
                
            }
        }
        





        [Test]
        public Task GetRoomByRoomNumberAsync_RoomListIsEmpty_ThrowsException()
        {
            //Arrage
            using (_db)
            {
                

                var service = new RoomService(_db);
                
                // Act & Assert
                Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetRoomByRoomNumberAsync("101"));
            }
            return Task.CompletedTask;
        }
        
        [Test]
        public Task GetRoomByRoomNumberAsync_RoomDoesntExist_ThrowsException()
        {
            //Arrage
            using (_db)
            {
                //Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                var service = new RoomService(_db);
                
                // Act & Assert
                Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetRoomByRoomNumberAsync("104"));
            }
            return Task.CompletedTask;
        }
        
        [Test]
        public async Task GetRoomByRoomNumberAsync_RoomIdIsCorrect_ReturnCorrectRoomId()
        {
            //Arrage
            using (_db)
            {
                //Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                var service = new RoomService(_db);
                var expected = new Room { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                var result = await service.GetRoomByRoomNumberAsync("101");
                // Assert
                Assert.That(result.Id, Is.EqualTo(expected.Id));
            }
            
        }
        
        [Test]
        public async Task GetRoomByRoomNumberAsync_RoomNumberIsCorrect_ReturnsCorrectRoomNumber()
        {
            //Arrage
            using (_db)
            {
                //Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                var service = new RoomService(_db);
                var expected = new Room { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                var result = await service.GetRoomByRoomNumberAsync("101");
                // Assert
                Assert.That(result.RoomNumber, Is.EqualTo(expected.RoomNumber));
            }
            
        }
        
        [Test]
        public async Task GetRoomByRoomNumberAsync_DescriptionIsCorrect_ReturnCorrectDescription()
        {
            //Arrage
            using (_db)
            {
                //Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                var service = new RoomService(_db);
                var expected = new Room { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                var result = await service.GetRoomByRoomNumberAsync("101");
                // Assert
                Assert.That(result.Description, Is.EqualTo(expected.Description));
            }
            
        }
        
        [Test]
        public async Task GetRoomByRoomNumberAsync_StatusIsCorrect_ReturnCorrectStatus()
        {
            //Arrage
            using (_db)
            {
                //Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                var service = new RoomService(_db);
                var expected = new Room { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                var result = await service.GetRoomByRoomNumberAsync("101");
                // Assert
                Assert.That(result.IsAvailable, Is.EqualTo(expected.IsAvailable));

            }
            
        }


    }
}