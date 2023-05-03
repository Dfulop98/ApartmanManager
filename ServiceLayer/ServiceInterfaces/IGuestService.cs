using DataModelLayer.Models;
using DTOLayer.Models;
using ServiceLayer.Factories.Model;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IGuestService
    {
        public Result<List<UniversalDTO>> GetGuests();
        public Result<UniversalDTO> GetGuest(int id);
        public Result<Guest> AddGuest(Guest guest);
        public Result<Guest> UpdateGuest(Guest guest);
        public Result<Guest> RemoveGuest(int id);
    }
}
