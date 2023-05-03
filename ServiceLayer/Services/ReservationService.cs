using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using DTOLayer.Factories;
using DTOLayer.Models;
using ServiceLayer.Factories.Model;
using ServiceLayer.ServiceInterfaces;

namespace ServiceLayer.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IGenericDataAccess<Reservation> _context;
        public ReservationService(
            IGenericDataAccess<Reservation> reservationContext
            )
        {
            _context = reservationContext;
        }

        public Result<List<UniversalDTO>> GetReservations()
        {
            try
            {
                var reservations = _context.GetEntities();
                var reservationsDTOs = UniversalDtoFactory.CreateListFromObjects(
                    reservations,
                    new List<string> { "Id", "CheckInDate", "CheckOutDate", "RoomId" });

                return Result<List<UniversalDTO>>.Success(reservationsDTOs);
            }
            catch ( Exception ex )
            {
                return Result<List<UniversalDTO>>.Failure($"error during get reservations {ex.Message}");
            }
            

        }
        public Result<UniversalDTO> GetReservation(int id)
        {

            if (id < 0)
                return Result<UniversalDTO>.Failure("id param is invalid");

            try
            {
                Reservation reservation = _context.GetEntity(id);
                UniversalDTO reservationDTO = UniversalDtoFactory.CreateFromObject(
                    reservation,
                    new List<string> { });

                return Result<UniversalDTO>.Success(reservationDTO);

            }
            catch( Exception ex )
            {
                return Result<UniversalDTO>.Failure($"error during get reservation {ex.Message}");
            }
        }


        public Result<Reservation> AddReservation(Reservation reservation)
        {
            if (_context.CheckEntity(reservation.Id))
                return Result<Reservation>.Failure("reservation already exists!");

            if (reservation.CheckInDate > reservation.CheckOutDate || reservation.CheckInDate < DateTime.Now)
                return Result<Reservation>.Failure("reservation date invalid");

            // Ask Kornel , how to check connected room is exits without make dependencies with other service
            try
            {
                _context.AddEntity(reservation);
                return Result<Reservation>.Success(reservation);
            }
            catch(Exception ex)
            {
                return Result<Reservation>.Failure($"error during adding new reservation :{ex.Message}");
            }
            
            
        }

        public Result<Reservation> UpdateReservation(Reservation reservation)
        {
            if (!_context.CheckEntity(reservation.Id))
                return Result<Reservation>.Failure("reservation doesn't exist!");

            if (reservation.CheckInDate > reservation.CheckOutDate || reservation.CheckInDate < DateTime.Now)
                return Result<Reservation>.Failure("reservation date invalid");

            try
            {
                _context.UpdateEntity(reservation);
                return Result<Reservation>.Success(reservation);

            }
            catch
            {
                return Result<Reservation>.Failure("error during updating reservation");
            }
        }

        public Result<Reservation> RemoveReservation(int id)
        {
            if (_context.CheckEntity(id))
                return Result<Reservation>.Failure("reservation doesn't exists");

            try
            {
                Reservation removedReservation = _context.GetEntity(id);
                _context.RemoveEntity(id);
                return Result<Reservation>.Success(removedReservation);
            }
            catch(Exception ex)
            {
                return Result<Reservation>.Failure($"error during remove reservation {ex.Message}");
            }
        }

    }
}
