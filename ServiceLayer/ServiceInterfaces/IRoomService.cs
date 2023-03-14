using DataModelLayer.Models;
using ServiceLayer.Factories;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IRoomService
    {
        
        public ResponseModel<Room> GetRooms();
        public ResponseModel<Room> GetRoom(int id);
        public ResponseModel<Room> AddRoom(Room room);
        public ResponseModel<Room> UpdateRoom(Room room);
        public ResponseModel<Room> RemoveRoom(int id);

    }
}
