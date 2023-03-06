using DataAccessLayer.DbAccess;
using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Factories;
using ServiceLayer.ServiceInterfaces;
using System.Net;

namespace ServiceLayer.Services
{
    public class ReservationService : IReservatonService
    {
        private readonly AMDbContext _db;
        public ReservationService(AMDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync() => await _db.Reservations.ToListAsync();
        public async Task<Reservation> GetReservationByIdAsync(int id) => await _db.Reservations.Where(r => r.Id == id).FirstAsync();
        

        public async Task<HttpResponseMessage> AddReservationAsync(Reservation reservation)
        {
            bool reservationExists = await _db.Reservations.AnyAsync(r => r.Id == reservation.Id);
            if (reservationExists)
            {
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.Conflict, "The reservation is already exists.");
            }
            else
            {
                if (reservation.CheckInDate > reservation.CheckOutDate || reservation.CheckInDate > DateTime.Now)
                {
                    return HttpResponseMessageFactory.CreateHttpResponseMessage
                        (HttpStatusCode.BadRequest, "The reservation dates incorrect.");
                }
                if (await _db.Rooms.AnyAsync(r => r.Id == reservation.RoomId))
                {
                    bool isAvailable = await _db.Rooms
                        .Where(r => r.Id == reservation.RoomId)
                        .Select(r => r.IsAvailable).FirstOrDefaultAsync();
                    if (isAvailable)
                    {
                        await _db.Reservations.AddAsync(reservation);
                        _db.SaveChanges();
                        return HttpResponseMessageFactory.CreateHttpResponseMessage
                            (HttpStatusCode.Created, "The reservation succefully added.");
                    }
                    return HttpResponseMessageFactory.CreateHttpResponseMessage
                        (HttpStatusCode.BadRequest, "This room is not available at the moment.");
                }
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.BadRequest, "The room doesnt exists.");
            }
        }

        public async Task<HttpResponseMessage> UpdateReservationAsync(Reservation reservation)
        {
            bool reservationExists = await _db.Reservations.AnyAsync(r => r.Id == reservation.Id);
            if (!reservationExists)
            {
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.NotFound, "Reservation doesnt exists.");
            }
            else
            {
                Reservation existingReservation = await _db.Reservations.Where(r => r.Id == reservation.Id).FirstAsync();
                existingReservation.Name = reservation.Name;
                existingReservation.Email = reservation.Email;
                existingReservation.Phone = reservation.Phone;
                existingReservation.CheckInDate = reservation.CheckInDate;
                existingReservation.CheckOutDate = reservation.CheckOutDate;
                existingReservation.NumberOfGuests = reservation.NumberOfGuests;
                existingReservation.RoomId = reservation.RoomId;
                _db.SaveChanges();

                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.OK, "Reservation succesfully updated.");
            }
        }
      
        public async Task<HttpResponseMessage> RemoveReservationAsync(int id)
        {
            bool reservationExists = await _db.Reservations.AnyAsync(r => r.Id == id);
            if (!reservationExists)
            {
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.NotFound, "Reservation doesnt exists.");
            }
            else
            {
                Reservation existingReservation = await _db.Reservations.Where(r => r.Id == id).FirstAsync();
                _db.Reservations.Remove(existingReservation);
                await _db.SaveChangesAsync();
                return HttpResponseMessageFactory.CreateHttpResponseMessage
                    (HttpStatusCode.OK, "Reservation succesfully deleted.");
            }
        }

    }
}
