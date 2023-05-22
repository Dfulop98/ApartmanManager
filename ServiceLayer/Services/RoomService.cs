using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using DTOLayer.Configurations;
using DTOLayer.Factories;
using DTOLayer.Models;
using ServiceLayer.Factories.Model;
using ServiceLayer.ServiceInterfaces;

namespace ServiceLayer.Services
{
    public class RoomService : IRoomService
    {
        private readonly IGenericDataAccess<Room> _context;
        public RoomService(
            IGenericDataAccess<Room> context
            )
        {
            _context = context;
        }

        public Result<List<UniversalDTO>> GetRooms()
        {
            bool roomExists = _context.CheckEntities();

            if (roomExists)
            {
                List<Room> rooms = _context.GetEntities("Images");
                List<UniversalDTO> roomDTOs = UniversalDtoFactory.CreateListFromObjects(
                    rooms,
                    DTOConfig.RoomProperties,
                    DTOConfig.RoomIncludedProperties);

                return Result<List<UniversalDTO>>.Success(roomDTOs);
            }
            
            return Result<List<UniversalDTO>>.Failure("Rooms not found");
            
        }


        public Result<UniversalDTO> GetRoom(int id)
        {

            if (_context.CheckEntity(id))
            {
                Room room = _context.GetEntity(id);
                var roomDTO = UniversalDtoFactory.CreateFromObject(
                    room,
                    DTOConfig.RoomProperties,
                    DTOConfig.RoomIncludedProperties);
                return Result<UniversalDTO>.Success(roomDTO);
            }

            return Result<UniversalDTO>.Failure("Room not found.");


        }
        public Result<Room> AddRoom(Room room)
        {
            if(room.RoomNumber == null)
            {
                return Result<Room>.Failure("The RoomNumber cannot be null");
            }
            if (!_context.CheckEntity(room.Id))
            {
                _context.AddEntity(room);
                return Result<Room>.Success(room);
            }
            return Result<Room>.Failure("The Room already exists.");
        }

        public Result<Room> UpdateRoom(Room room)
        {
            if (_context.CheckEntity(room.Id))
            {
                _context.UpdateEntity(room);
                return Result<Room>.Success(room);

            }
            return Result<Room>.Failure("Room not found.");
        }

        public Result<Room> RemoveRoom(int id)
        {

            if (_context.CheckEntity(id))
            {
                var removedEntity = _context.GetEntity(id);

                _context.RemoveEntity(id);
                return Result<Room>.Success(removedEntity);
            }
            return Result<Room>.Failure("The Room doesn't exists.");

        }

        public Result<Room> LinkImageToRoom(Images images, int roomId)
        {
            if (images == null)
                return Result<Room>.Failure("Images param is null");

            if (roomId < 0)
                return Result<Room>.Failure("invalid roomid param");

            try
            {
                var room = _context.GetEntity(roomId);
                room.Images.Add(images);
                _context.UpdateEntity(room);

                return Result<Room>.Success(room);
            }
            catch (Exception ex)
            {
                return Result<Room>.Failure($"error during linking images to room: {ex.Message}");
            }
                 
        }

    }
}
