using DataAccessLayer.DbAccess;
using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Factories;
using ServiceLayer.Factories.Interfaces;
using ServiceLayer.Factories.Model;
using ServiceLayer.ServiceInterfaces;
using System.Net;

namespace ServiceLayer.Services
{
    public class ReservationService : IReservatonService
    {
        private readonly IGenericDataAccess<Reservation> _reservationContext;
        private readonly IGenericDataAccess<Room> _roomContext;
        private readonly IResponseModelFactory<Reservation> _responseModel;
        public ReservationService(
            IGenericDataAccess<Reservation> reservationContext
            , IGenericDataAccess<Room> roomContext
            , IResponseModelFactory<Reservation> responseModel
            )
        {
            _reservationContext = reservationContext;
            _roomContext = roomContext;
            _responseModel = responseModel;
        }

        public ResponseModel<Reservation> GetReservations()
        {
            if (_reservationContext.CheckEntities())
            {
                var entities = _reservationContext.GetEntities();
                return _responseModel.CreateResponseModel("Success","The reservations returned.", entities);
            }
            return _responseModel.CreateResponseModel("NotFound", "The reservations doesnt exists.");

        }
        public ResponseModel<Reservation> GetReservation(int id)
        {
            if (_reservationContext.CheckEntity(id))
            {
                var entity = _reservationContext.GetEntity(id);
                return _responseModel.CreateResponseModel("Success", "The reservation returned.", entity);
            }
            return _responseModel.CreateResponseModel("NotFound", "The reservation doesnt exists");
        }
        

        public ResponseModel<Reservation> AddReservation(Reservation reservation)
        {
            if (_reservationContext.CheckEntity(reservation.Id))
            {
                return _responseModel.CreateResponseModel("Conflict", "The reservation already exists.");
            }
            else
            {
                if (reservation.CheckInDate > reservation.CheckOutDate || reservation.CheckInDate > DateTime.Now)
                {
                    return _responseModel.CreateResponseModel("BadRequest", "The reservation date is incorrect.");
                }
                if (_roomContext.CheckEntity(reservation.RoomId))
                {
                    var relatedRoom = _roomContext.GetEntity(reservation.RoomId);
                    if (relatedRoom.IsAvailable)
                    {
                        _reservationContext.AddEntity(reservation);
                        return _responseModel.CreateResponseModel("Success", "Room succefully added.");
                    }
                    return _responseModel.CreateResponseModel("Conflict", "The Room is not available");
                }
                return _responseModel.CreateResponseModel("BadRequest", "The Room doesnt exists.");
            }
        }

        public ResponseModel<Reservation> UpdateReservation(Reservation reservation)
        {
            if (_reservationContext.CheckEntity(reservation.Id))
            {
                _reservationContext.UpdateEntity(reservation);
                return _responseModel.CreateResponseModel("Succes", "The reservation succefully updated.");
            }
            return _responseModel.CreateResponseModel("NotFound", "The reservation doesnt exists.");
        }
      
        public ResponseModel<Reservation> RemoveReservation(int id)
        {
            if (_reservationContext.CheckEntity(id))
            {
                _reservationContext.RemoveEntity(id);
                return _responseModel.CreateResponseModel("Succes", "The reservation successfully deleted.");
            }
            return _responseModel.CreateResponseModel("NotFound", "The reservation doesnt exists.");
        }

    }
}
