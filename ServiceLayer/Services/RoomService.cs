using DataAccessLayer.DbAccess;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ServiceLayer.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Room> GetRoomByIdAsync(int id) => await _db.Rooms.Where(x => x.Id == id).FirstAsync();

        public async Task<Room> GetRoomByRoomNumberAsync(string roomNumber) => await _db.Rooms.Where(x => x.RoomNumber == roomNumber).FirstAsync();
        public async Task<HttpResponseMessage> AddRoomAsync(Room room)
        {
            bool roomExists = await _db.Rooms.AnyAsync(r => r.Id == room.Id);
            if (roomExists)
            {
                return CreateHttpResponseMessage(HttpStatusCode.Conflict, "The room already exists.");
            }
            else
            {
                await _db.Rooms.AddAsync(room);
                await _db.SaveChangesAsync();
                return CreateHttpResponseMessage(HttpStatusCode.Created, "The room successfully added.");
            }
        }

        private static HttpResponseMessage CreateHttpResponseMessage(HttpStatusCode statusCode, string message)
        {
            HttpResponseMessage response = new(statusCode)
            {
                Content = new StringContent(message)
            };
            return response;
        }

    }
}
