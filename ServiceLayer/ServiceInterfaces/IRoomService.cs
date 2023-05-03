using DataModelLayer.Models;
using DTOLayer.Models;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IRoomService
    {

        public Result<List<UniversalDTO>> GetRooms();
        public Result<UniversalDTO> GetRoom(int id);
        public Result<Room> AddRoom(Room room);
        public Result<Room> UpdateRoom(Room room);
        public Result<Room> RemoveRoom(int id);
        public Result<Room> LinkImageToRoom(Images images, int roomId);

    }
}
