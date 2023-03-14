using DataModelLayer.Models;
using ServiceLayer.Models;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IRoomService
    {
        public ResponseModel GetRooms();
        public ResponseModel GetRoom(int id);
        public ResponseModel AddRoom(Room room);
        public ResponseModel UpdateRoom(Room room);
        public ResponseModel RemoveRoom(int id);

    }
}
