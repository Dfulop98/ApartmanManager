using DataAccessLayer.DbAccess;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
