

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

        public bool CheckRoom(int id) => _db.Rooms.Any(r => r.Id == id);
        public bool CheckRooms() => _db.Rooms.Any();
        public List<Room> GetRooms() => _db.Rooms.Include(r => r.Images).ToList();

        public Room GetRoom(int id) => _db.Rooms.Where(x => x.Id == id).First();
        public void AddRoom(Room room)
        {
            _db.Rooms.Add(room);
            _db.SaveChanges();
        }
        public void UpdateRoom(Room room)
        {
            Room existingRoom = _db.Rooms.Where(r => r.Id == room.Id).First();
            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.Description = room.Description;
            existingRoom.IsAvailable = room.IsAvailable;
            _db.SaveChanges();
        }

        public void RemoveRoom(int id)
        {
            Room existingRoom = _db.Rooms.Where(r => r.Id == id).First();
            _db.Rooms.Remove(existingRoom);
            _db.SaveChanges();
        }

        public void AddImage(RoomImage image)
        {
            _db.RoomImages.Add(image);
            _db.SaveChanges();

        }
        public void AddImageToRoom(Room room,RoomImage image)
        {
            room.Images.Add(image);
            _db.SaveChanges();

        }
    }
}
