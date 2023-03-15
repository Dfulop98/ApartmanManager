using DataAccessLayer.DbAccess;
using DataModelLayer.Models;
using ServiceLayer.Factories;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ServiceInterfaces;
using System.Data;
using System.Net;
using DataAccessLayer.Operations;
using DataAccessLayer.Interfaces;
using System.ComponentModel;
using ServiceLayer.Factories.Model;
using ServiceLayer.Factories.Interfaces;

namespace ServiceLayer.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomDataAccess _context;
        private readonly IResponseModelFactory<Room> _responseModel;

        public RoomService(IRoomDataAccess roomDataAccess, IResponseModelFactory<Room> responseModel)
        {
            _context = roomDataAccess;
            _responseModel = responseModel;
        }

        public ResponseModel<Room> GetRooms()
        {
            bool roomExists = _context.CheckRooms();
            if (roomExists) 
            {
                List<Room> rooms = _context.GetRooms();
                return _responseModel.CreateResponseModel("Success", "The rooms successfully returned.", rooms);
            }
            else
            {
                return _responseModel.CreateResponseModel("NotFound", "Rooms is doesnt exists.");
            }
        }
        public ResponseModel<Room> GetRoom(int id)
        {
            bool roomExists = _context.CheckRoom(id);
            if (roomExists)
            {
                Room room = _context.GetRoom(id);
                return _responseModel.CreateResponseModel("Success", "The room successfully returned.", room);
            }
            else
            {
                return _responseModel.CreateResponseModel("NotFound", "The room is doesnt exists.");
            }

        }
        public ResponseModel<Room> AddRoom(Room room)
        {
            bool roomExists = _context.CheckRoom(room.Id);
            if (roomExists)
            {
                return _responseModel.CreateResponseModel("Conflict", "The room already exists");
            }
            else
            {
                _context.AddRoom(room);
                return _responseModel.CreateResponseModel("Created", "The room successfully added.");
            }
        }

        public ResponseModel<Room> UpdateRoom(Room room)
        {
            bool roomExists = _context.CheckRoom(room.Id);
            if (roomExists)
            {
                _context.UpdateRoom(room);
                return _responseModel.CreateResponseModel("Updated", "The room successfully updated.");
            }
            else
            {
                return _responseModel.CreateResponseModel("NotFound", "The room doesnt exists.");
            }

        }

        public ResponseModel<Room> RemoveRoom(int id)
        {
            bool roomExists = _context.CheckRoom(id);
            if (roomExists)
            {
                _context.RemoveRoom(id);
                return _responseModel.CreateResponseModel("OK", "Room successfully deleted.");
            }
            else
            {
                return _responseModel.CreateResponseModel("NotFound", "The room is doesnt exists.");
            }
        }

        

    }
}
