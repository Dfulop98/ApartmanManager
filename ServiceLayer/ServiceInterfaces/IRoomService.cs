using DataModelLayer.Models;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IRoomService
    {

        public ResponseModel GetRooms();
        public ResponseModel GetRoom(int id);
        public ResponseModel AddRoom(Room room);
        public ResponseModel UpdateRoom(Room room);
        public ResponseModel RemoveRoom(int id);
        public ResponseModel AddImage(Stream imageStream, string imageName, int roomId);

    }
}
