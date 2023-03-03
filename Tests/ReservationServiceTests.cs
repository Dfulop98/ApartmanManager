using DataAccessLayer.DbAccess;
using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;
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
                    RoomId = 20,
                };
                //Act
                HttpResponseMessage result = await service.AddReservationAsync(newReservation);
                Assert.Multiple(() =>
                {
                    //Assert
                    Assert.That(result.Content.ReadAsStringAsync, Is.EqualTo("The room doesnt exists."));
                    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
                });
            }
        }
    }
}
