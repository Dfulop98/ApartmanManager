using DataModelLayer.Models;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IGuestService
    {
        public ResponseModel GetGuests();
        public ResponseModel GetGuest(int id);
        public ResponseModel AddGuest(Guest guest);
        public ResponseModel UpdateGuest(Guest guest);
        public ResponseModel RemoveGuest(int id);
    }
}
