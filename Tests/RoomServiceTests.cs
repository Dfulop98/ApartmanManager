using DataAccessLayer.DbAccess;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services;
using System.Net;

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


                RoomService service = new(_db);

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
                _db.Rooms.AddRange(new List<Room> { });
                _db.SaveChanges();

                RoomService service = new(_db);

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

                RoomService service = new(_db);

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

                RoomService service = new(_db);

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


                RoomService service = new(_db);

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


                RoomService service = new(_db);

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


                RoomService service = new(_db);

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


                RoomService service = new(_db);

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


                RoomService service = new(_db);

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
                RoomService service = new(_db);
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
                RoomService service = new(_db);
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



                RoomService service = new(_db);
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



                RoomService service = new(_db);
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



                RoomService service = new(_db);
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



                RoomService service = new(_db);
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


                RoomService service = new(_db);

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

                RoomService service = new(_db);

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

                RoomService service = new(_db);
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

                RoomService service = new(_db);
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

                RoomService service = new(_db);
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

                RoomService service = new(_db);
                var expected = new Room { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                var result = await service.GetRoomByRoomNumberAsync("101");
                // Assert
                Assert.That(result.IsAvailable, Is.EqualTo(expected.IsAvailable));

            }

        }




        [Test]
        public async Task AddRoom_RoomAlreadyExist_ReturnsConflict()
        {
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
                RoomService service = new(_db);
                Room RoomToAdd = new() { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                var result = await service.AddRoomAsync(RoomToAdd);
                //Assert
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));

            }
        }

        [Test]
        public async Task AddRoom_RoomAlreadyExist_ReturnsCorrectConflictMessage()
        {
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
                RoomService service = new(_db);
                Room RoomToAdd = new() { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                var result = await service.AddRoomAsync(RoomToAdd);
                //Assert
                Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("The room already exists."));

            }
        }

        [Test]
        public async Task AddRoom_RoomSuccesfullyAdd_ReturnsAddedRoom()
        {
            using (_db)
            {
                //Arrage
                RoomService service = new(_db);
                Room RoomToAdd = new() { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                var result = await service.AddRoomAsync(RoomToAdd);
                var addedRoom = await service.GetRoomByIdAsync(1);
                //Assert
                Assert.That(addedRoom, Is.EqualTo(RoomToAdd));

            }
        }

        [Test]
        public async Task AddRoom_RoomSuccesfullyAdd_ReturnsCreated()
        {
            using (_db)
            {
                //Arrage
                RoomService service = new(_db);
                Room RoomToAdd = new() { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                var result = await service.AddRoomAsync(RoomToAdd);
                //Assert
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            }
        }

        [Test]
        public async Task AddRoom_RoomSuccesfullyAdd_ReturnsCorrectCreatedMessage()
        {
            using (_db)
            {
                //Arrage
                RoomService service = new(_db);
                Room RoomToAdd = new() { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                var result = await service.AddRoomAsync(RoomToAdd);
                //Assert
                Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("The room successfully added."));

            }
        }






        [Test]
        public async Task UpdateRoomAsync_RoomDoesntExists_ReturnNotFound()
        {
            using (_db)
            {
                // Arrage
                RoomService service = new(_db);
                Room newRoom = new() { Id = 1, RoomNumber = "101", Description = "testnew1", IsAvailable = true };

                // Act
                var result = await service.UpdateRoomAsync(newRoom);
                //Assert
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

            }
        }

        [Test]
        public async Task UpdateRoomAsync_RoomDoesntExists_ReturnNotFoundMessage()
        {
            using (_db)
            {
                // Arrage
                RoomService service = new(_db);
                Room newRoom = new() { Id = 1, RoomNumber = "101", Description = "testnew1", IsAvailable = true };

                // Act
                var result = await service.UpdateRoomAsync(newRoom);
                //Assert
                Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("Room doesnt exists."));

            }
        }

        [Test]
        public async Task UpdateRoomAsync_RoomSuccesfullyUpdate_ReturnOk()
        {
            using (_db)
            {

                // Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                RoomService service = new(_db);
                Room newRoom = new() { Id = 1, RoomNumber = "101", Description = "testnew1", IsAvailable = true };

                // Act
                var result = await service.UpdateRoomAsync(newRoom);
                //Assert
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            }
        }

        [Test]
        public async Task UpdateRoomAsync_RoomSuccesfullyUpdate_ReturnOkMessage()
        {
            using (_db)
            {

                // Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                RoomService service = new(_db);
                Room newRoom = new() { Id = 1, RoomNumber = "101", Description = "testnew1", IsAvailable = true };

                // Act
                var result = await service.UpdateRoomAsync(newRoom);
                //Assert
                Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("Room succesfully updated."));

            }
        }

        [Test]
        public async Task UpdateRoomAsync_RoomSuccesfullyUpdate_ReturnCorrectRoom()
        {
            using (_db)
            {

                // Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();

                RoomService service = new(_db);
                Room newRoom = new() { Id = 1, RoomNumber = "101", Description = "testnew1", IsAvailable = true };

                // Act
                await service.UpdateRoomAsync(newRoom);
                var updatedRoom = await service.GetRoomByIdAsync(1);
                //Assert
                Assert.That(newRoom.RoomNumber, Is.EqualTo(updatedRoom.RoomNumber));

            }
        }

        [Test]
        public async Task RemoveRoomByIdAsync_RoomDoesntExist_ReturnNotFound()
        {
            using (_db)
            {
                // Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();
                RoomService service = new(_db);
                Room roomToRemove = new() { Id = 4, RoomNumber = "104", Description = "test4", IsAvailable = false };
                // Act
                HttpResponseMessage result = await service.RemoveRoomByIdAsync(roomToRemove.Id);
                // Assert
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }
        [Test]
        public async Task RemoveRoomByIdAsync_RoomDoesntExist_ReturnNotFoundMessage()
        {
            using (_db)
            {
                // Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();
                RoomService service = new(_db);
                Room roomToRemove = new() { Id = 4, RoomNumber = "104", Description = "test4", IsAvailable = false };
                // Act
                HttpResponseMessage result = await service.RemoveRoomByIdAsync(roomToRemove.Id);
                // Assert
                Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("Room doesnt exists."));
            }
        }
        [Test]
        public async Task RemoveRoomByIdAsync_CheckRoomIsRemoved_ReturnNotFound()
        {
            using (_db)
            {
                // Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();
                RoomService service = new(_db);
                Room roomToRemove = new() { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                await service.RemoveRoomByIdAsync(roomToRemove.Id);
                HttpResponseMessage result = await service.RemoveRoomByIdAsync(roomToRemove.Id);
                // Assert
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }
        [Test]
        public async Task RemoveRoomByIdAsync_RoomRemove_ReturOK()
        {
            using (_db)
            {
                // Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();
                RoomService service = new(_db);
                Room roomToRemove = new() { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                HttpResponseMessage result = await service.RemoveRoomByIdAsync(roomToRemove.Id);
                // Assert
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }
        [Test]
        public async Task RemoveRoomByIdAsync_RoomRemove_ReturnOKMessage()
        {
            using (_db)
            {

                // Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = false },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();
                RoomService service = new(_db);
                Room roomToRemove = new() { Id = 1, RoomNumber = "101", Description = "test1", IsAvailable = false };
                // Act
                HttpResponseMessage result = await service.RemoveRoomByIdAsync(roomToRemove.Id);
                // Assert
                Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("Room succesfully deleted."));
            }
        }
    }
}