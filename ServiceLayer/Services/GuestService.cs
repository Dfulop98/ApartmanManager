using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using DTOLayer.Configurations;
using DTOLayer.Factories;
using DTOLayer.Models;
using ServiceLayer.Factories.Model;
using ServiceLayer.ServiceInterfaces;

namespace ServiceLayer.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGenericDataAccess<Guest> _context;
        public GuestService(IGenericDataAccess<Guest> guestContext)
        {
            _context = guestContext;
        }

        public Result<List<UniversalDTO>> GetGuests()
        {

            bool GuestCheck = _context.CheckEntities();
            if (GuestCheck)
            {
                var entities = _context.GetEntities();
                List<UniversalDTO> guestDTOs = UniversalDtoFactory.CreateListFromObjects(entities, new List<string> { });
                return Result<List<UniversalDTO>>.Success(guestDTOs);
            }
            return Result<List<UniversalDTO>>.Failure("Guests not found");
        }
        public Result<UniversalDTO> GetGuest(int id)
        {
            bool GuestCheck = _context.CheckEntities();
            if (GuestCheck)
            {
                var entity = _context.GetEntity(id);
                UniversalDTO guestDTO = UniversalDtoFactory.CreateFromObject(entity, new List<string> { });
                return Result<UniversalDTO>.Success(guestDTO);
            }
            return Result<UniversalDTO>.Failure("Guest not found");
        }

        public Result<Guest> AddGuest(Guest guest)
        {
            if (!_context.CheckEntity(guest.Id))
            {
                _context.AddEntity(guest);
                return Result<Guest>.Success(guest);
            }
            return Result<Guest>.Failure("The Guest already exists.");
        }


        public Result<Guest> UpdateGuest(Guest guest)
        {
            if (_context.CheckEntity(guest.Id))
            {
                _context.UpdateEntity(guest);
                return Result<Guest>.Success(guest);
            }
            return Result<Guest>.Failure("The guest doesn't exist.");
        }


        public Result<Guest> RemoveGuest(int id)
        {
            if (_context.CheckEntity(id))
            {
                var removedEntity = _context.GetEntity(id);
                _context.RemoveEntity(id);
                return Result<Guest>.Success(removedEntity);
            }
            return Result<Guest>.Failure("The guest doesn't exist.");
        }

    }
}
