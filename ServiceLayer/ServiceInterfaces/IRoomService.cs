using DataModelLayer.Models;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IRoomService
    {
        public List<Room> GetRooms();
        public Room GetRoom(int id);
        public Task<HttpResponseMessage> AddRoom(Room room);
        public Task<HttpResponseMessage> UpdateRoomAsync(Room room);
        public Task<HttpResponseMessage> RemoveRoomByIdAsync(int id);

    }
}
