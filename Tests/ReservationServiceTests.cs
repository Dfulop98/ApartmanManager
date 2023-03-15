using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using Moq;
using NUnit.Framework;
using ServiceLayer.Factories;
using ServiceLayer.Factories.Interfaces;
using ServiceLayer.ServiceInterfaces;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;

namespace ServiceLayer.Tests
{
    [TestFixture]
    public class ReservationServiceTests
    {
        private Mock<IGenericDataAccess<Reservation>> _mockReservationContext;
        private Mock<IGenericDataAccess<Room>> _mockRoomContext;
        private Mock<IResponseModelFactory<Reservation>> _mockResponseModel;
        private IReservatonService _reservationService;

        [SetUp]
        public void Setup()
        {
            _mockReservationContext = new Mock<IGenericDataAccess<Reservation>>();
            _mockRoomContext = new Mock<IGenericDataAccess<Room>>();
            _mockResponseModel = new Mock<IResponseModelFactory<Reservation>>();
            _reservationService = new ReservationService(_mockReservationContext.Object, _mockRoomContext.Object, _mockResponseModel.Object);
        }

        // ... már megírt GetReservations és GetReservation tesztek ...
        [Test]
        public void GetReservations_ReservationsExist_ReturnsSuccess()
        {
            // Arrange
            _mockReservationContext.Setup(x => x.CheckEntities()).Returns(true);
            _mockReservationContext.Setup(x => x.GetEntities()).Returns(new List<Reservation>());

            // Act
            var result = _reservationService.GetReservations();

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("Success", "The reservations returned.", It.IsAny<IEnumerable<Reservation>>()), Times.Once);
        }

        [Test]
        public void GetReservations_ReservationsDoNotExist_ReturnsNotFound()
        {
            // Arrange
            _mockReservationContext.Setup(x => x.CheckEntities()).Returns(false);

            // Act
            var result = _reservationService.GetReservations();

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("NotFound", "The reservations doesnt exists."), Times.Once);
        }

        [Test]
        public void GetReservation_ReservationExists_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            _mockReservationContext.Setup(x => x.CheckEntity(id)).Returns(true);
            _mockReservationContext.Setup(x => x.GetEntity(id)).Returns(new Reservation { Id = id });

            // Act
            var result = _reservationService.GetReservation(id);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("Success", "The reservation returned.", It.IsAny<Reservation>()), Times.Once);
        }

        [Test]
        public void GetReservation_ReservationDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int id = 1;
            _mockReservationContext.Setup(x => x.CheckEntity(id)).Returns(false);

            // Act
            var result = _reservationService.GetReservation(id);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("NotFound", "The reservation doesnt exists"), Times.Once);
        }

        [Test]
        public void AddReservation_ReservationDoesNotExistAndDatesAreCorrectAndRoomIsAvailable_ReturnsSuccess()
        {
            // Arrange
            Reservation reservation = new() { Id = 1, CheckInDate = DateTime.Now.AddDays(1), CheckOutDate = DateTime.Now.AddDays(3), RoomId = 1 };
            _mockReservationContext.Setup(x => x.CheckEntity(reservation.Id)).Returns(false);
            _mockRoomContext.Setup(x => x.CheckEntity(reservation.RoomId)).Returns(true);
            Room room = new Room { Id = 1, IsAvailable = true };
            _mockRoomContext.Setup(x => x.GetEntity(reservation.RoomId)).Returns(room);

            // Act
            var result = _reservationService.AddReservation(reservation);

            // Assert
            _mockReservationContext.Verify(x => x.AddEntity(reservation), Times.Once);
            _mockResponseModel.Verify(x => x.CreateResponseModel("Success", "Room succefully added."), Times.Once);
        }

        [Test]
        public void AddReservation_ReservationExists_ReturnsConflict()
        {
            // Arrange
            Reservation reservation = new() { Id = 1 };
            _mockReservationContext.Setup(x => x.CheckEntity(reservation.Id)).Returns(true);

            // Act
            var result = _reservationService.AddReservation(reservation);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("Conflict", "The reservation already exists."), Times.Once);
        }

        [Test]
        public void AddReservation_DatesAreIncorrect_ReturnsBadRequest()
        {
            // Arrange
            Reservation reservation = new() { Id = 1, CheckInDate = DateTime.Now.AddDays(3), CheckOutDate = DateTime.Now.AddDays(1), RoomId = 1 };
            _mockReservationContext.Setup(x => x.CheckEntity(reservation.Id)).Returns(false);

            // Act
            var result = _reservationService.AddReservation(reservation);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("BadRequest", "The reservation date is incorrect."), Times.Once);
        }
        [Test]
        public void AddReservation_RoomDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            Reservation reservation = new() { Id = 1, CheckInDate = DateTime.Now.AddDays(1), CheckOutDate = DateTime.Now.AddDays(3), RoomId = 1 };
            _mockReservationContext.Setup(x => x.CheckEntity(reservation.Id)).Returns(false);
            _mockRoomContext.Setup(x => x.CheckEntity(reservation.RoomId)).Returns(false);

            // Act
            var result = _reservationService.AddReservation(reservation);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("BadRequest", "The Room doesnt exists."), Times.Once);
        }

        [Test]
        public void AddReservation_RoomIsNotAvailable_ReturnsConflict()
        {
            // Arrange
            Reservation reservation = new() { Id = 1, CheckInDate = DateTime.Now.AddDays(1), CheckOutDate = DateTime.Now.AddDays(3), RoomId = 1 };
            _mockReservationContext.Setup(x => x.CheckEntity(reservation.Id)).Returns(false);
            _mockRoomContext.Setup(x => x.CheckEntity(reservation.RoomId)).Returns(true);
            Room room = new Room { Id = 1, IsAvailable = false };
            _mockRoomContext.Setup(x => x.GetEntity(reservation.RoomId)).Returns(room);

            // Act
            var result = _reservationService.AddReservation(reservation);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("Conflict", "The Room is not available"), Times.Once);
        }

        [Test]
        public void UpdateReservation_ReservationExists_ReturnsSuccess()
        {
            // Arrange
            Reservation reservation = new() { Id = 1 };
            _mockReservationContext.Setup(x => x.CheckEntity(reservation.Id)).Returns(true);

            // Act
            var result = _reservationService.UpdateReservation(reservation);

            // Assert
            _mockReservationContext.Verify(x => x.UpdateEntity(reservation), Times.Once);
            _mockResponseModel.Verify(x => x.CreateResponseModel("Succes", "The reservation succefully updated."), Times.Once);
        }

        [Test]
        public void UpdateReservation_ReservationDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            Reservation reservation = new() { Id = 1 };
            _mockReservationContext.Setup(x => x.CheckEntity(reservation.Id)).Returns(false);

            // Act
            var result = _reservationService.UpdateReservation(reservation);

            // Assert
            _mockResponseModel.Verify(x => x.CreateResponseModel("NotFound", "The reservation doesnt exists."), Times.Once);
        }

        [Test]
        public void RemoveReservation_ReservationExists_ReturnsSuccess()
        {
            // Arrange
            int id = 1;
            _mockReservationContext.Setup(x => x.CheckEntity(id)).Returns(true);

            // Act
            var result = _reservationService.RemoveReservation(id);

            // Assert
            _mockReservationContext.Verify(x => x.RemoveEntity(id), Times.Once);
            _mockResponseModel.Verify(x => x.CreateResponseModel("Succes", "The reservation successfully deleted."), Times.Once);
        }
    }
}