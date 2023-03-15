using DataAccessLayer.DbAccess;
using DataAccessLayer.Interfaces;
using DataModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Factories;
using ServiceLayer.Factories.Interfaces;
using ServiceLayer.Factories.Model;
using ServiceLayer.ServiceInterfaces;
using System.Net;

namespace ServiceLayer.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGenericDataAccess<Guest> _guestContext;
        private readonly IResponseModelFactory<Guest> _responseModel;
        public GuestService(
            IGenericDataAccess<Guest> guestContext,
            IResponseModelFactory<Guest> responseModel
            )
        {
            _guestContext= guestContext;
            _responseModel = responseModel;
        }

        public ResponseModel<Guest> GetGuests()
        {
            if (_guestContext.CheckEntities())
            {
                var entities = _guestContext.GetEntities();
                return _responseModel.CreateResponseModel("Success", "The guest returned.", entities);
            }
            return _responseModel.CreateResponseModel("NotFound", "The Guest doesnt exists.");

        }
        public ResponseModel<Guest> GetGuest(int id)
        {
            if (_guestContext.CheckEntity(id))
            {
                var entity = _guestContext.GetEntity(id);
                return _responseModel.CreateResponseModel("Success", "The guest returned.", entity);
            }
            return _responseModel.CreateResponseModel("NotFound", "The Guest doesnt exists.");
        }

        public ResponseModel<Guest> AddGuest(Guest guest)
        {

            if (!_guestContext.CheckEntity(guest.Id))
            {
                _guestContext.AddEntity(guest);
                return _responseModel.CreateResponseModel("Success", "The Guest successfully added.");
            }
            return _responseModel.CreateResponseModel("Conflict", "The Guest already exists.");
            
        }

        public ResponseModel<Guest> UpdateGuest(Guest guest)
        {
            
            if (_guestContext.CheckEntity(guest.Id))
            {
                _guestContext.UpdateEntity(guest);
                return _responseModel.CreateResponseModel("Success", "The Guest successfully updated.");
            }
            return _responseModel.CreateResponseModel("NotFound", "The guest doesnt exists.");

        }

        public ResponseModel<Guest> RemoveGuest(int id)
        {
            
            if (_guestContext.CheckEntity(id))
            {
                _guestContext.RemoveEntity(id);
                return _responseModel.CreateResponseModel("Success", "The Guest successfully removed.");
            }
            return _responseModel.CreateResponseModel("NotFound", "The guest doesnt exists.");


        }
    }
}
