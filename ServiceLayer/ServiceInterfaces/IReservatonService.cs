using DataModelLayer.Models;
using ServiceLayer.Factories;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IReservatonService
    {
        public ResponseModel<Reservation> GetReservations();
        public ResponseModel<Reservation> GetReservation(int id);
        public ResponseModel<Reservation> AddReservation(Reservation reservation);
        public ResponseModel<Reservation> RemoveReservation(int id);
        public ResponseModel<Reservation> UpdateReservation(Reservation reservation);

    }
}
