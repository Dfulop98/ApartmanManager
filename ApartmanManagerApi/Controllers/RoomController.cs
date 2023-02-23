using DataAccessLayer.DbAccess;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ServiceInterfaces;

namespace ApartmanManagerApi.Controllers
{
    public  class RoomController : ControllerBase

    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("/api/room")]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
        {
            IEnumerable<Room> allRooms = await _roomService.GetAllRoomAsync();
            return Ok(allRooms);
        }

        [HttpGet("/api/room/{id}")]
        public async Task<ActionResult<Room>> GetRoomById(int id)
        {
            return Ok(await _roomService.GetRoomByIdAsync(id));
        } 
        
        [HttpGet("/api/room/{id}")]
        public async Task<ActionResult<Room>> GetRoomByRoomNumber(int id)
        {
            return Ok(await _roomService.GetRoomByIdAsync(id));
        } 


    }
}