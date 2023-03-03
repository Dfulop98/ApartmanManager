using DataModelLayer.Models;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IRoomService
    {
        public Task<IEnumerable<Room>> GetAllRoomAsync();
        public Task<Room> GetRoomByIdAsync(int id);
        public Task<Room> GetRoomByRoomNumberAsync(string roomNumber);
        public Task<HttpResponseMessage> AddRoomAsync(Room room);
        public Task<HttpResponseMessage> UpdateRoomAsync(Room room);
        public Task<HttpResponseMessage> RemoveRoomByIdAsync(int id);

    }
}
