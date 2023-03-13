using DataAccessLayer.DbAccess;
using DataModelLayer.Models;
using ServiceLayer.Factories;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ServiceInterfaces;
using System.Data;
using System.Net;
using DataAccessLayer.Operations;
using DataAccessLayer.Interfaces;
using System.ComponentModel;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomDataAccess _context;
        public RoomService(IRoomDataAccess roomDataAccess)
        {
            _context = roomDataAccess;
        }

        public List<Room> GetRooms() => _context.GetRooms();
        public ResponseModel GetRoom(int id)
        {
            bool roomExists = _context.CheckRoom(id);
            if (roomExists)
            {
                Room room = _context.GetRoom(id);
                return new ResponseModel("Success", "The room successfully added.", room);
            }
            else
            {
                return new ResponseModel("NotFound", "The room is doesnt exists");
            }

        }
        public ResponseModel AddRoom(Room room)
        {
            bool roomExists = _context.CheckRoom(room.Id);
            if (roomExists)
            {
                return new ResponseModel("Conflict", "The room already exists");
            }
            else
            {
                _context.AddRoom(room);
                return new ResponseModel("Created", "The room successfully added.");
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
                    (HttpStatusCode.OK, "Room successfully updated.");
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
