
using DataModelLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IReservationDataAccess
    {
        public bool CheckReservationByRoomId(int roomId);
        public List<Reservation> GetReservationsByRoomId(int roomId);
    }
}
