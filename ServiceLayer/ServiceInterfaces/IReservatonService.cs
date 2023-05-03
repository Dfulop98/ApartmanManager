using DataModelLayer.Models;
using DTOLayer.Models;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IReservationService
    {
        public Result<List<UniversalDTO>> GetReservations();
        public Result<UniversalDTO> GetReservation(int id);
        public Result<Reservation> AddReservation(Reservation reservation);
        public Result<Reservation> RemoveReservation(int id);
        public Result<Reservation> UpdateReservation(Reservation reservation);

    }
}
