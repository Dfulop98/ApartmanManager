using DataAccessLayer.DbAccess;
using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ReservationServiceTests
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
        [Test]
        public async Task GetReservationsAsync_ListIsNotEmpty_ReturnNotNull()
        {
            using (_db)
            {
                DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);
                _db.Reservations.AddRange(new List<Reservation>
                {
                    new Reservation
                    {
                        Name = "test name",
                        Email = "test@email.com",
                        Phone = "+36302642038",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 1,
                        RoomId = 1,
                    },
                    new Reservation
                    {
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    },
                });
                _db.SaveChanges();
                ReservationService service = new(_db);
                //Act

                IEnumerable<Reservation> result = await service.GetReservationsAsync();
                //Assert
                Assert.That(result, Is.Not.Null);

            }
        }

        [Test]
        public async Task GetReservationsAsync_ListIsEmpty_ReturnEmpty()
        {
            using (_db)
            {
                ReservationService service = new(_db);
                //Act

                IEnumerable<Reservation> result = await service.GetReservationsAsync();
                //Assert
                Assert.That(result, Is.Empty);

            }
        }
        
        [Test]
        public async Task GetReservationsAsync_HaveOneReservation_ReturnOneReservation()
        {
            using (_db)
            {
                DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);
                _db.Reservations.AddRange(new List<Reservation>
                {
                    
                    new Reservation
                    {
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    },
                });
                _db.SaveChanges();
                ReservationService service = new(_db);
                //Act

                IEnumerable<Reservation> result = await service.GetReservationsAsync();
                //Assert
                Assert.That(result.Count(), Is.EqualTo(1));

            }
        }
        
        [Test]
        public async Task GetReservationsAsync_HaveFourReservation_ReturnFourReservation()
        {
            using (_db)
            {
                DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);
                _db.Reservations.AddRange(new List<Reservation>
                {
                    
                    new Reservation
                    {
                        Name = "test name1",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 4,
                    },
                    new Reservation
                    {
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 3,
                    },
                    
                    new Reservation
                    {
                        Name = "test name3",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    },
                    
                    new Reservation
                    {
                        Name = "test name4",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 1,
                    },
                });
                _db.SaveChanges();
                ReservationService service = new(_db);
                //Act

                IEnumerable<Reservation> result = await service.GetReservationsAsync();
                //Assert
                Assert.That(result.Count(), Is.EqualTo(4));

            }
        }

        [Test]
        public async Task GetReservationsAsync_IsCorrectReservationId_ReturnSameReservation()
        {
            using (_db)
            {
                DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);
                _db.Reservations.AddRange(new List<Reservation>
                {

                    new Reservation
                    {
                        Id = 1,
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    },
                });
                _db.SaveChanges();
                ReservationService service = new(_db);

                var expectedReservation = new List<Reservation>
                {
                    new Reservation
                    {
                        Id = 1,
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    }
                };
                //Act
                IEnumerable<Reservation> result = await service.GetReservationsAsync();
                //Assert
                Assert.That(result.First().Id, Is.EqualTo(expectedReservation.First().Id));
            }
        }

        [Test]
        public Task GetReservationByIdAsync_EmptyResevationList_ThrowsException()
        {
            //Arrage
            using (_db)
            {
                // Arrage
                ReservationService service = new(_db);
                // Act & Assert
                Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetReservationByIdAsync(1));

            }
            return Task.CompletedTask;
        }
        
        [Test]
        public Task GetReservationByIdAsync_ResevationNotExists_ThrowsException()
        {
            //Arrage
            using (_db)
            {
                // Arrage
                DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);
                _db.Reservations.AddRange(new List<Reservation>
                {

                    new Reservation
                    {
                        Id = 1,
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    },
                });
                _db.SaveChanges();
                ReservationService service = new(_db);
                // Act & Assert
                Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetReservationByIdAsync(5));

            }
            return Task.CompletedTask;
        }
        
        [Test]
        public async Task GetReservationByIdAsync_ResevationIdIsCorrect_ReturnCorrectReservationId()
        {
            //Arrage
            using (_db)
            {
                // Arrage
                DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);
                _db.Reservations.AddRange(new List<Reservation>
                {

                    new Reservation
                    {
                        Id = 1,
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    },
                });
                _db.SaveChanges();
                ReservationService service = new(_db);
                // Act
                var result = await service.GetReservationByIdAsync(1);
                var expectedReservation = new Reservation
                {
                    Id = 1,
                    Name = "test name2",
                    Email = "test2@email.com",
                    Phone = "+36302642037",
                    CheckInDate = checkinDate,
                    CheckOutDate = checkoutDate,
                    NumberOfGuests = 2,
                    RoomId = 2,
                };

                // Assert
                Assert.That(result.Id, Is.EqualTo(expectedReservation.Id));

            }
        }


        [Test]
        public async Task AddReservationAsync_WithValidReservationData_ReturnsSuccess()
        {
            using  (_db)
            {
                //Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = true },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();
                ReservationService service = new(_db);
                DateTime checkinDate = new (2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new (2008, 5, 4, 8, 30, 52);
                Reservation newReservation = new()
                {
                    Name = "test name",
                    Email = "test@email.com",
                    Phone = "+36302642038",
                    CheckInDate = checkinDate,
                    CheckOutDate = checkoutDate,
                    NumberOfGuests = 1,
                    RoomId = 1,
                };
                //Act
                HttpResponseMessage result = await service.AddReservationAsync(newReservation);
                Assert.Multiple(() =>
                {
                    //Assert
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("The reservation succefully added."));
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
                });
            }
        }
        
        
        [Test]
        public async Task AddReservationAsync_WithExistingReservation_ReturnsConflict()
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
                ReservationService service = new(_db);
                DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);
                _db.Reservations.AddRange(new List<Reservation>
                {
                    new Reservation
                    {
                        Name = "test name",
                        Email = "test@email.com",
                        Phone = "+36302642038",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 1,
                        RoomId = 1,
                    },
                    new Reservation
                    {
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    },
                });
                _db.SaveChanges();
                Reservation newReservation = new()
                {
                    Id = 1,
                    Name = "test name",
                    Email = "test@email.com",
                    Phone = "+36302642038",
                    CheckInDate = checkinDate,
                    CheckOutDate = checkoutDate,
                    NumberOfGuests = 1,
                    RoomId = 1,
                };
                //Act
                HttpResponseMessage result = await service.AddReservationAsync(newReservation);
                Assert.Multiple(() =>
                {
                    //Assert
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("The reservation is already exists."));
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
                });
            }
        }
        
        [Test]
        public async Task AddReservationAsync_WithInvalidReservationDate_ReturnsBadRequest()
        {
            using (_db)
            {
                //Arrage
                _db.Rooms.AddRange(new List<Room>
                {
                    new Room { Id = 1, RoomNumber = "101", Description="test1", IsAvailable = true },
                    new Room { Id = 2, RoomNumber = "102", Description="test2", IsAvailable = true },
                    new Room { Id = 3, RoomNumber = "103", Description="test3", IsAvailable = false }
                });
                _db.SaveChanges();
                ReservationService service = new(_db);
                DateTime checkinDate = new(2024, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);
                Reservation newReservation = new()
                {
                    Name = "123",
                    Email = "test@email.com",
                    Phone = "+36302642038",
                    CheckInDate = checkinDate,
                    CheckOutDate = checkoutDate,
                    NumberOfGuests = 1,
                    RoomId = 1,
                };
                //Act
                HttpResponseMessage result = await service.AddReservationAsync(newReservation);
                Assert.Multiple(() =>
                {
                    //Assert
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("The reservation dates incorrect."));
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                });
            }
        }
        
        [Test]
        public async Task AddReservationAsync_WithInvalidReservationRoom_ReturnsBadRequest()
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
                ReservationService service = new(_db);
                DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);
                Reservation newReservation = new()
                {
                    Name = "123",
                    Email = "test@email.com",
                    Phone = "+36302642038",
                    CheckInDate = checkinDate,
                    CheckOutDate = checkoutDate,
                    NumberOfGuests = 1,
                    RoomId = 1,
                };
                //Act
                HttpResponseMessage result = await service.AddReservationAsync(newReservation);
                Assert.Multiple(() =>
                {
                    //Assert
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("This room is not available at the moment."));
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                });
            }
        }
        
        [Test]
        public async Task AddReservationAsync_WithNonExistentRoom_ReturnsNotFound()
        {
            using (_db)
            {
                //Arrage
                DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);
                Reservation newReservation = new()
                {
                    Name = "123", 
                    Email = "test@email.com",
                    Phone = "+36302642038",
                    CheckInDate = checkinDate,
                    CheckOutDate = checkoutDate,
                    NumberOfGuests = 1,
                    RoomId = 20,
                };
                //Act
                ReservationService service = new(_db);
                HttpResponseMessage result = await service.AddReservationAsync(newReservation);
                Assert.Multiple(() =>
                {
                    //Assert
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("The room doesnt exists."));
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                });
            }
        }


        [Test]
        public async Task UpdateReservationAsync_ReservationDoesntExists_ReturnsNotFound()
        {
            // Arrage

            DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
            DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);

            ReservationService service = new(_db);
            Reservation newReservation = new()
            {
                Id = 5,
                Name = "123",
                Email = "test@email.com",
                Phone = "+36302642038",
                CheckInDate = checkinDate,
                CheckOutDate = checkoutDate,
                NumberOfGuests = 1,
                RoomId = 20,
            };
            
            // Act
            var result = await service.UpdateReservationAsync(newReservation);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("Reservation doesnt exists."));
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            });
        }

        [Test]
        public async Task UpdateReservationAsync_ReservationSuccesfullyUpdate_ReturnsOk()
        {
            //Arrage
            DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
            DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);
            _db.Reservations.AddRange(new List<Reservation>
                {
                    new Reservation
                    {
                        Id = 1,
                        Name = "test name",
                        Email = "test@email.com",
                        Phone = "+36302642038",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 1,
                        RoomId = 1,
                    },

                    new Reservation
                    {   
                        Id = 2,
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    },
                });

            _db.SaveChanges();

            ReservationService service = new(_db);

            Reservation newReservation = new()
            {   
                Id = 1,
                Name = "123",
                Email = "test@email.com",
                Phone = "+36302642038",
                CheckInDate = checkinDate,
                CheckOutDate = checkoutDate,
                NumberOfGuests = 1,
                RoomId = 20,
            };
            var result = await service.UpdateReservationAsync(newReservation);

            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("Reservation succesfully updated."));
            });
        }

        [Test]
        public async Task UpdateReservationAsync_ReservationSuccesfullyUpdate_ReturnsCorrectReservation()
        {
            //Arrage
            DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
            DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);
            _db.Reservations.AddRange(new List<Reservation>
                {
                    new Reservation
                    {
                        Id = 1,
                        Name = "test name",
                        Email = "test@email.com",
                        Phone = "+36302642038",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 1,
                        RoomId = 1,
                    },
                    new Reservation
                    {
                        Id = 2,
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    },
                });
            _db.SaveChanges();

            //Act

            ReservationService Service = new(_db);
            Reservation expectedReservation = new()
            {
                Id = 1,
                Name = "test name",
                Email = "test5@email.com",
                Phone = "+36302642038",
                CheckInDate = checkinDate,
                CheckOutDate = checkoutDate,
                NumberOfGuests = 5,
                RoomId = 2,
            };
            await Service.UpdateReservationAsync(expectedReservation);
            var result = await Service.GetReservationByIdAsync(expectedReservation.Id);

            Assert.That(result.Name, Is.EqualTo(expectedReservation.Name));
        }


        [Test]
        public async Task RemoveReservationByIdAsync_ReservationDoesntExist_ReturnNotFound()
        {
            using (_db)
            {
                // Arrage
                DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);

                _db.Reservations.AddRange(new List<Reservation>
                {
                    new Reservation
                    {
                        Id = 1,
                        Name = "test name",
                        Email = "test@email.com",
                        Phone = "+36302642038",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 1,
                        RoomId = 1,
                    },
                    new Reservation
                    {
                        Id = 2,
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    },
                });
                _db.SaveChanges();

                ReservationService service = new(_db);
                Reservation resertvationToRemove = new()
                {
                    Id = 3,
                    Name = "test name2",
                    Email = "test2@email.com",
                    Phone = "+36302642037",
                    CheckInDate = checkinDate,
                    CheckOutDate = checkoutDate,
                    NumberOfGuests = 2,
                    RoomId = 2,
                };
                // Act
                HttpResponseMessage result = await service.RemoveReservationAsync(resertvationToRemove.Id);

                Assert.Multiple(() =>
                {
                    // Assert
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("Reservation doesnt exists."));
                });
            }
        }


        [Test]
        public async Task RemoveReservationByIdAsync_CheckReservationIsRemoved_ReturnNotFound()
        {
            using (_db)
            {
                // Arrage
                DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);

                _db.Reservations.AddRange(new List<Reservation>
                {
                    new Reservation
                    {
                        Id = 1,
                        Name = "test name",
                        Email = "test@email.com",
                        Phone = "+36302642038",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 1,
                        RoomId = 1,
                    },
                    new Reservation
                    {
                        Id = 2,
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    },
                });
                _db.SaveChanges();

                ReservationService service = new(_db);
                Reservation resertvationToRemove = new()
                {
                    Id = 1,
                    Name = "test name",
                    Email = "test@email.com",
                    Phone = "+36302642038",
                    CheckInDate = checkinDate,
                    CheckOutDate = checkoutDate,
                    NumberOfGuests = 1,
                    RoomId = 1,
                };
                // Act
                await service.RemoveReservationAsync(resertvationToRemove.Id);
                _db.SaveChanges();

                HttpResponseMessage result = await service.RemoveReservationAsync(resertvationToRemove.Id);

                // Assert
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }
        [Test]
        public async Task RemoveReservationByIdAsync_ReservationRemove_ReturOK()
        {
            using (_db)
            {
                // Arrage
                DateTime checkinDate = new(2008, 5, 1, 8, 30, 52);
                DateTime checkoutDate = new(2008, 5, 4, 8, 30, 52);

                _db.Reservations.AddRange(new List<Reservation>
                {
                    new Reservation
                    {
                        Id = 1,
                        Name = "test name",
                        Email = "test@email.com",
                        Phone = "+36302642038",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 1,
                        RoomId = 1,
                    },
                    new Reservation
                    {
                        Id = 2,
                        Name = "test name2",
                        Email = "test2@email.com",
                        Phone = "+36302642037",
                        CheckInDate = checkinDate,
                        CheckOutDate = checkoutDate,
                        NumberOfGuests = 2,
                        RoomId = 2,
                    },
                });
                _db.SaveChanges();

                ReservationService service = new(_db);
                Reservation resertvationToRemove = new()
                {
                    Id = 1,
                    Name = "test name",
                    Email = "test@email.com",
                    Phone = "+36302642038",
                    CheckInDate = checkinDate,
                    CheckOutDate = checkoutDate,
                    NumberOfGuests = 1,
                    RoomId = 1,
                };
                // Act
                var result = await service.RemoveReservationAsync(resertvationToRemove.Id);

                Assert.Multiple(() =>
                {
                    // Assert
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("Reservation succesfully deleted."));
                });
            }
        }

    }
}
