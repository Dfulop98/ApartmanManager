using DataModelLayer.Models;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IGuestService
    {
        public ResponseModel<Guest> GetGuests();
        public ResponseModel<Guest> GetGuest(int id);
        public ResponseModel<Guest> AddGuest(Guest guest);
        public ResponseModel<Guest> UpdateGuest(Guest guest);
        public ResponseModel<Guest> RemoveGuest(int id);
    }
}
