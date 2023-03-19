using DataModelLayer.Models;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IReservatonService
    {
        public ResponseModel GetReservations();
        public ResponseModel GetReservation(int id);
        public ResponseModel AddReservation(Reservation reservation);
        public ResponseModel RemoveReservation(int id);
        public ResponseModel UpdateReservation(Reservation reservation);

    }
}
