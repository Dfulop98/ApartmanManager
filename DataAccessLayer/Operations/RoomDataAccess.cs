

using DataAccessLayer.DbAccess;
using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DataAccessLayer.Operations
{
    public class RoomDataAccess : IRoomDataAccess
    {
        private readonly AMDbContext _db;
        public RoomDataAccess(AMDbContext db)
        {
            _db = db;
        }

        public List<Room> GetRooms() => _db.Rooms.ToList();

        public Room GetRoom(int id) => _db.Rooms.Where(x => x.Id == id).First();
        public void AddRoom(Room room)
        {
            _db.Rooms.Add(room);
            _db.SaveChangesAsync();
        }
        public void UpdateRoom(Room room)
        {
            Room existingRoom = _db.Rooms.Where(r => r.Id == room.Id).First();
            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.Description = room.Description;
            existingRoom.IsAvailable = room.IsAvailable;
            _db.SaveChangesAsync();
        }

        public void RemoveRoom(int id)
        {
            Room existingRoom = _db.Rooms.Where(r => r.Id == id).First();
            _db.Rooms.Remove(existingRoom);
            _db.SaveChangesAsync();
        }


    }
}
