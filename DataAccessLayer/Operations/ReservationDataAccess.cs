using DataAccessLayer.DbAccess;
using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Operations
{
    public class ReservationDataAccess : IReservationDataAccess
    {
        private readonly AMDbContext _db;

        public ReservationDataAccess(AMDbContext db)
        {
            _db = db;
        }
        public bool CheckReservationByRoomId(int roomId) 
            => _db.Reservations.Any(reservation => reservation.RoomId == roomId);
        public List<Reservation> GetReservationsByRoomId(int roomId) 
            => _db.Reservations.Where(reservation => reservation.RoomId == roomId).ToList();
        

    }
}
