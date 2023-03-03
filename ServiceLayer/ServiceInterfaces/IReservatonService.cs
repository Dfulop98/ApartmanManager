using DataModelLayer.Models;
namespace ServiceLayer.ServiceInterfaces
{
    public interface IReservatonService
    {
        public Task<IEnumerable<Reservation>> GetReservationsAsync();
        public Task<Reservation> GetReservationByIdAsync(int id);
        public Task<HttpResponseMessage> AddReservationAsync(Reservation reservation);
        public Task<HttpResponseMessage> RemoveReservationAsync(int id);
        public Task<HttpResponseMessage> UpdateReservationAsync(Reservation reservation);

    }
}
