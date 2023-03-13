using DataModelLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IRoomDataAccess
    {
        public bool CheckRoom(int id);
        public List<Room> GetRooms();
        public Room GetRoom(int id);
        public void AddRoom(Room room);
        public void UpdateRoom(Room room);
        public void RemoveRoom(int id);

    }
}
