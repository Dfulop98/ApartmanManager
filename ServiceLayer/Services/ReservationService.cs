using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using DTOLayer.Factories;
using ServiceLayer.Common;
using ServiceLayer.Factories.Interfaces;
using ServiceLayer.Factories.Model;
using ServiceLayer.ServiceInterfaces;

namespace ServiceLayer.Services
{
    public class ReservationService : IReservatonService
    {
        private readonly IGenericDataAccess<Reservation> _reservationContext;
        private readonly IGenericDataAccess<Room> _roomContext;
        private readonly IResponseModelFactory _responseModel;
        public ReservationService(
            IGenericDataAccess<Reservation> reservationContext
            , IGenericDataAccess<Room> roomContext
            , IResponseModelFactory responseModel
            )
        {
            _reservationContext = reservationContext;
            _roomContext = roomContext;
            _responseModel = responseModel;
        }

        public ResponseModel GetReservations()
        {
            if (_reservationContext.CheckEntities())
            {
                var entities = _reservationContext.GetEntities();
                var entityDTOs = UniversalDtoFactory.CreateListFromObjects(
                    entities,
                    new List<string> { "Id", "CheckInDate", "CheckOutDate", "RoomId" }
                    );
                return _responseModel.CreateResponseModel(Status.Ok, Messages.ReservationsGetOk, entityDTOs);
            }
            return _responseModel.CreateResponseModel(Status.NotFound, Messages.ReservationNotFound);

        }
        public ResponseModel GetReservation(int id)
        {
            if (_reservationContext.CheckEntity(id))
            {
                var entity = _reservationContext.GetEntity(id);
                var entityDTO = UniversalDtoFactory.CreateFromObject(entity, new List<string> { });
                return _responseModel.CreateResponseModel(Status.Ok, Messages.ReservationGetOk, entityDTO);
            }
            return _responseModel.CreateResponseModel(Status.NotFound, Messages.ReservationNotFound);
        }


        public ResponseModel AddReservation(Reservation reservation)
        {
            if (_reservationContext.CheckEntity(reservation.Id))
            {
                return _responseModel.CreateResponseModel(Status.Conflict, Messages.ReservationConflict);
            }
            else
            {
                if (reservation.CheckInDate > reservation.CheckOutDate || reservation.CheckInDate < DateTime.Now)
                {
                    return _responseModel.CreateResponseModel(Status.BadRequest, Messages.ReservationDateBadRequest);
                }
                if (_roomContext.CheckEntity(reservation.RoomId))
                {
                    var relatedRoom = _roomContext.GetEntity(reservation.RoomId);
                    if (relatedRoom.IsAvailable)
                    {
                        _reservationContext.AddEntity(reservation);
                        return _responseModel.CreateResponseModel(Status.Created, Messages.ReservationCreated);
                    }
                    return _responseModel.CreateResponseModel(Status.Conflict, Messages.ReservationRoomConflict);
                }
                return _responseModel.CreateResponseModel(Status.BadRequest, Messages.ReservationRoomBadRequest);
            }
        }

        public ResponseModel UpdateReservation(Reservation reservation)
        {
            if (_reservationContext.CheckEntity(reservation.Id))
            {
                _reservationContext.UpdateEntity(reservation);
                return _responseModel.CreateResponseModel(Status.Ok, Messages.ReservationUpdated);
            }
            return _responseModel.CreateResponseModel(Status.NotFound, Messages.ReservationNotFound);
        }

        public ResponseModel RemoveReservation(int id)
        {
            if (_reservationContext.CheckEntity(id))
            {
                _reservationContext.RemoveEntity(id);
                return _responseModel.CreateResponseModel(Status.Ok, Messages.ReservationDeleted);
            }
            return _responseModel.CreateResponseModel(Status.NotFound, Messages.ReservationNotFound);
        }

    }
}
