using DataModelLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IRoomDataAccess
    {
        public bool CheckRoom(int id);
        public bool CheckRooms();
        public List<Room> GetRooms();
        public Room GetRoom(int id);
        public void AddRoom(Room room);
        public void UpdateRoom(Room room);
        public void RemoveRoom(int id);
        public void AddImage(RoomImage image);
        public void AddImageToRoom(Room room ,RoomImage image);

    }
}
