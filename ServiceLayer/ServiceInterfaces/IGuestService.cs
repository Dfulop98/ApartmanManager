using DataModelLayer.Models;

namespace ServiceLayer.ServiceInterfaces
{
    public interface IGuestService
    {
        public Task<IEnumerable<Guest>> GetAllGuestsAsync();
        public Task<Guest> GetGuestByIdAsync(int id);
        public Task<HttpResponseMessage> AddGuestAsync(Guest guest);
    }
}
