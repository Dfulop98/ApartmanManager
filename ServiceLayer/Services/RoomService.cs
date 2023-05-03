using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using DTOLayer.Configurations;
using DTOLayer.Factories;
using DTOLayer.Models;
using ServiceLayer.Common;
using ServiceLayer.Factories;
using ServiceLayer.Factories.Interfaces;
using ServiceLayer.Factories.Model;
using ServiceLayer.ServiceInterfaces;

namespace ServiceLayer.Services
{
    public class RoomService : IRoomService
    {
        private readonly IResponseModelFactory _responseModel;
        private readonly IGenericDataAccess<Room> _context;
        private readonly IRoomDataAccess _roomContext;
        public RoomService(
            IGenericDataAccess<Room> context,
            IResponseModelFactory responseModel,
            IRoomDataAccess roomContext
            )
        {
            _context = context;
            _responseModel = responseModel;
            _roomContext = roomContext;
        }

        public ResponseModel GetRooms()
        {
            bool roomExists = _context.CheckEntities();
            if (roomExists)
            {
                List<Room> rooms = _context.GetEntities("Images");
                List<UniversalDTO> roomDTOs = UniversalDtoFactory.CreateListFromObjects(
                    rooms,
                    DTOConfig.RoomProperties,
                    DTOConfig.RoomIncludedProperties);

                return _responseModel.CreateResponseModel(Status.Ok, Messages.RoomsGetOk, roomDTOs);
            }
            else
            {
                return _responseModel.CreateResponseModel(Status.NotFound, Messages.RoomNotFound);
            }
        }


        public ResponseModel GetRoom(int id)
        {

            if (_context.CheckEntity(id))
            {
                Room room = _context.GetEntity(id);
                var roomDTO = UniversalDtoFactory.CreateFromObject(
                    room,
                    DTOConfig.RoomProperties,
                    DTOConfig.RoomIncludedProperties);
                return _responseModel.CreateResponseModel(Status.Ok, Messages.RoomGetOk, roomDTO);
            }

            return _responseModel.CreateResponseModel(Status.NotFound, Messages.RoomNotFound);


        }
        public ResponseModel AddRoom(Room room)
        {
            if (_context.CheckEntity(room.Id))
            {
                return _responseModel.CreateResponseModel(Status.Conflict, Messages.RoomConflict);
            }

            _context.AddEntity(room);
            return _responseModel.CreateResponseModel(Status.Created, Messages.RoomCreated);

        }

        public ResponseModel UpdateRoom(Room room)
        {
            if (_context.CheckEntity(room.Id))
            {
                _context.UpdateEntity(room);
                return _responseModel.CreateResponseModel(Status.Ok, Messages.RoomUpdated);
            }

            return _responseModel.CreateResponseModel(Status.NotFound, Messages.RoomNotFound);


        }

        public ResponseModel RemoveRoom(int id)
        {

            if (_context.CheckEntity(id))
            {
                _context.RemoveEntity(id);
                return _responseModel.CreateResponseModel(Status.Ok, Messages.RoomDeleted);
            }
            return _responseModel.CreateResponseModel(Status.NotFound, Messages.RoomNotFound);

        }

        public ResponseModel AddImage(Stream imageStream, string imageName, int roomId)
        {
            if (_context.CheckEntity(roomId))
            {
                string ImageUrl = GoogleCloudStorageService.UploadImage(imageStream, imageName);
                Room room = _context.GetEntity(roomId);
                RoomImage image = new()
                {
                    Url = ImageUrl,
                    Room = room,
                };
                room.Images ??= new List<RoomImage>();
                _roomContext.AddImage(image);
                _roomContext.AddImageToRoom(room, image);
                return _responseModel.CreateResponseModel(Status.Created, Messages.ImagesCreated);

            }
            return _responseModel.CreateResponseModel(Status.NotFound, Messages.RoomNotFound);
        }

    }
}
