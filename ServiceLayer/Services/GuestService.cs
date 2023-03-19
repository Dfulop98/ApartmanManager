using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using DTOLayer.Factories;
using DTOLayer.Models;
using ServiceLayer.Common;
using ServiceLayer.Factories.Interfaces;
using ServiceLayer.Factories.Model;
using ServiceLayer.ServiceInterfaces;

namespace ServiceLayer.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGenericDataAccess<Guest> _guestContext;
        private readonly IResponseModelFactory _responseModel;
        public GuestService(
            IGenericDataAccess<Guest> guestContext,
            IResponseModelFactory responseModel
            )
        {
            _guestContext = guestContext;
            _responseModel = responseModel;
        }

        public ResponseModel GetGuests()
        {
            if (_guestContext.CheckEntities())
            {
                var entities = _guestContext.GetEntities();
                List<UniversalDTO> guestDTOs = UniversalDtoFactory.CreateListFromObjects(entities, new List<string> { });
                return _responseModel.CreateResponseModel(Status.Ok, "The guest returned.", guestDTOs);
            }
            return _responseModel.CreateResponseModel(Status.NotFound, "The Guest doesnt exists.");

        }
        public ResponseModel GetGuest(int id)
        {
            if (_guestContext.CheckEntity(id))
            {
                var guest = _guestContext.GetEntity(id);
                var guestDTO = UniversalDtoFactory.CreateFromObject(guest, new List<string> { });
                return _responseModel.CreateResponseModel(Status.Ok, "The guest returned.", guestDTO);
            }
            return _responseModel.CreateResponseModel(Status.NotFound, "The Guest doesnt exists.");
        }

        public ResponseModel AddGuest(Guest guest)
        {

            if (!_guestContext.CheckEntity(guest.Id))
            {
                _guestContext.AddEntity(guest);
                return _responseModel.CreateResponseModel(Status.Created, "The Guest successfully added.");
            }
            return _responseModel.CreateResponseModel(Status.NotFound, "The Guest already exists.");

        }

        public ResponseModel UpdateGuest(Guest guest)
        {

            if (_guestContext.CheckEntity(guest.Id))
            {
                _guestContext.UpdateEntity(guest);
                return _responseModel.CreateResponseModel(Status.Ok, "The Guest successfully updated.");
            }
            return _responseModel.CreateResponseModel(Status.NotFound, "The guest doesnt exists.");

        }

        public ResponseModel RemoveGuest(int id)
        {

            if (_guestContext.CheckEntity(id))
            {
                _guestContext.RemoveEntity(id);
                return _responseModel.CreateResponseModel(Status.Ok, "The Guest successfully removed.");
            }
            return _responseModel.CreateResponseModel(Status.NotFound, "The guest doesnt exists.");


        }
    }
}
