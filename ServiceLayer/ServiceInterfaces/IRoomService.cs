using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IRoomService
    {
        public Task<IEnumerable<Room>> GetAllRoomAsync();
        public Task<Room> GetRoomByIdAsync(int id);
        public Task<Room> GetRoomByRoomNumberAsync(string roomNumber);
    }
}
