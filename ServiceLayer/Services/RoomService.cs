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
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomDataAccess _context;
        public RoomService(IRoomDataAccess roomDataAccess)
        {
            _context = roomDataAccess;
        }

        public ResponseModel GetRooms()
        {
            bool roomExists = _context.CheckRooms();
            if (roomExists) 
            {
                List<Room> rooms = _context.GetRooms();
                return new ResponseModel("Success", "The rooms successfully returned.", rooms);
            }
            else
            {
                return new ResponseModel("NotFound", "Rooms is doesnt exists.");
            }
        }
        public ResponseModel GetRoom(int id)
        {
            bool roomExists = _context.CheckRoom(id);
            if (roomExists)
            {
                Room room = _context.GetRoom(id);
                return new ResponseModel("Success", "The room successfully returned.", room);
            }
            else
            {
                return new ResponseModel("NotFound", "The room is doesnt exists.");
            }

        }
        public ResponseModel AddRoom(Room room)
        {
            bool roomExists = _context.CheckRoom(room.Id);
            if (roomExists)
            {
                return new ResponseModel("Conflict", "The room already exists");
            }
            else
            {
                _context.AddRoom(room);
                return new ResponseModel("Created", "The room successfully added.");
            }
        }

        public ResponseModel UpdateRoom(Room room)
        {
            bool roomExists = _context.CheckRoom(room.Id);
            if (roomExists)
            {
                _context.UpdateRoom(room);
                return new ResponseModel("Updated", "The room successfully updated.");
            }
            else
            {
                return new ResponseModel("NotFound", "The room doesnt exists.");
            }

        }

        public ResponseModel RemoveRoom(int id)
        {
            bool roomExists = _context.CheckRoom(id);
            if (roomExists)
            {
                _context.RemoveRoom(id);
                return new ResponseModel ("OK", "Room successfully deleted.");
            }
            else
            {
                return new ResponseModel("NotFound", "The room is doesnt exists.");
            }
        }

        

    }
}
