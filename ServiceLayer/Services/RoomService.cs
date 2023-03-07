using DataAccessLayer.DbAccess;
using DataModelLayer.Models;
using ServiceLayer.Factories;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ServiceInterfaces;
using System.Data;
using System.Net;

namespace ServiceLayer.Services
{
    public class RoomService : IRoomService
    {
        private readonly AMDbContext _db;
        public RoomService(AMDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Room>> GetAllRoomAsync() => await _db.Rooms.ToListAsync();

        public Task<Room> GetRoomByIdAsync(int id) =>  _db.Rooms.Where(x => x.Id == id).FirstAsync();

        public Task<Room> GetRoomByRoomNumberAsync(string roomNumber) => _db.Rooms.Where(x => x.RoomNumber == roomNumber).FirstAsync();
        public async Task<HttpResponseMessage> AddRoomAsync(Room room)
        {
            bool roomExists = await _db.Rooms.AnyAsync(r => r.Id == room.Id);
            if (roomExists)
            {
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.Conflict, "The room already exists.");
            }
            else
            {
                await _db.Rooms.AddAsync(room);
                await _db.SaveChangesAsync();
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.Created, "The room successfully added.");
            }
        }
        public async Task<HttpResponseMessage> UpdateRoomAsync(Room room)
        {
            bool roomExists = await _db.Rooms.AnyAsync(r => r.Id == room.Id);
            if (!roomExists)
            {
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.NotFound, "Room doesnt exists.");
            }
            else
            {
                Room existingRoom = await _db.Rooms.Where(r => r.Id == room.Id).FirstAsync();
                existingRoom.RoomNumber = room.RoomNumber;
                existingRoom.Description = room.Description;
                existingRoom.IsAvailable = room.IsAvailable;

                await _db.SaveChangesAsync();
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.OK, "Room succesfully updated.");
            }

        }

        public async Task<HttpResponseMessage> RemoveRoomByIdAsync(int id)
        {
            bool roomExists = await _db.Rooms.AnyAsync(r => r.Id == id);
            if (!roomExists)
            {
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.NotFound, "Room doesnt exists.");
            }
            else
            {
                Room existingRoom = await _db.Rooms.Where(r => r.Id == id).FirstAsync();
                _db.Rooms.Remove(existingRoom);
                await _db.SaveChangesAsync();
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.OK, "Room succesfully deleted.");
            }
        }

        

    }
}
