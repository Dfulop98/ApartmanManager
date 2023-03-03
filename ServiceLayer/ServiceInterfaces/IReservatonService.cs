using DataModelLayer.Models;
namespace ServiceLayer.ServiceInterfaces
{
    public interface IReservatonService
    {
        public void AddReservation();
        public void RemoveReservation();
        public void UpdateReservation();

        public List<Reservation> GetReservations();
        public Reservation GetReservationById(int id);
    }
}
