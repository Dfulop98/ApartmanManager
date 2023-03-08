using DataAccessLayer.DbAccess;
using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Factories;
using ServiceLayer.ServiceInterfaces;
using System.Net;

namespace ServiceLayer.Services
{
    public class GuestService : IGuestService
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

        public async Task<HttpResponseMessage> UpdateGuestAsync(Guest guest)
        {
            bool guestExists = await _db.Guests.AnyAsync(g => g.Id == guest.Id);
            if (!guestExists)
            {
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.NotFound, "Guest doesnt exists.");
            }
            else
            {
                Guest existingGuest = await _db.Guests.Where(g => g.Id == guest.Id).FirstAsync();
                existingGuest.Address = guest.Address;
                existingGuest.Name = guest.Name;
                existingGuest.Phone= guest.Phone;
                existingGuest.Email = guest.Email;
                existingGuest.Nationatily = guest.Nationatily;
                await _db.SaveChangesAsync();
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.OK, "Guest successfully updated.");
            }
        }

        public async Task<HttpResponseMessage> RemoveGuestByIdAsync(int id)
        {
            bool guestExists = await _db.Guests.AnyAsync(g => g.Id == id);
            if (!guestExists)
            {
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.NotFound, "Guest doesnt exists.");
            }
            else
            {
                Guest existingGuest = await _db.Guests.Where(g => g.Id == id).FirstAsync();
                _db.Guests.Remove(existingGuest);
                await _db.SaveChangesAsync();
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.OK, "Guest successfully deleted.");
            }
        }
    }
}
