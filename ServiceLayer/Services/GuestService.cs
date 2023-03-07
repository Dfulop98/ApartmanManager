using DataAccessLayer.DbAccess;
using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Factories;
using System.Net;

namespace ServiceLayer.Services
{
    public class GuestService
    {
        private readonly AMDbContext _db;

        public GuestService(AMDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Guest>> GetAllGuestsAsync() => await _db.Guests.ToListAsync();
        public async Task<Guest> GetGuestByIdAsync(int id) => await _db.Guests.Where(x => x.Id == id).FirstAsync();

        public async Task<HttpResponseMessage> AddGuestAsync(Guest guest)
        {
            bool guestExists = await _db.Guests.AnyAsync(r => r.Id == guest.Id);
            if (guestExists)
            {
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.Conflict, "The guest already exists.");
            }
            else
            {
                await _db.Guests.AddAsync(guest);
                await _db.SaveChangesAsync();
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.Created, "The guest successfully added.");
            }
        }
    }
}
